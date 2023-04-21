using BackupServer.Data.Interfaces;
using BackupServer.Data.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Cryptography;

namespace BackupServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileItemController : ControllerBase
    {
        private string path = "D:/copies/";
        private IItemCopyRepository _itemCopyRepository;
        private IItemRepository _itemRepository;

        public FileItemController(IItemCopyRepository itemCopyRepository, IItemRepository itemRepository)
        {
            this._itemCopyRepository = itemCopyRepository;
            this._itemRepository = itemRepository;
        }

        public async Task CreatingAsync(Item item)
        {
            ItemCopy newItemCopy = new ItemCopy();
            Item parentItem = _itemRepository.FindByPath(item.Path, item.Name);

            newItemCopy.ParentItem = parentItem;

            var copies = await _itemCopyRepository.GetAllItemCopies(parentItem);
            if (copies.Count() > 0)
            {
                newItemCopy.PrevCopy = copies.OrderByDescending(i => i.CreationTime).FirstOrDefault();
            }
            else
            {
                newItemCopy.PrevCopy = null;
            }

            newItemCopy.Path = path;

            using (var stream = new FileStream(Path.Combine(path, "/", newItemCopy.ParentItem.Name), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(stream);
                newItemCopy.Hash = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            newItemCopy.CreationTime = DateTime.Now;
            Console.WriteLine(newItemCopy);
            if (!System.IO.File.Exists(path + newItemCopy.Hash + item.Name))
            {
                System.IO.File.Copy(Path.Combine(item.Path, "/", item.Name), path + newItemCopy.Hash + item.Name);
                Console.WriteLine(path + newItemCopy.Hash + item.Name);
            }
            else
            {
                return;
            }
            await _itemCopyRepository.CreateAsync(newItemCopy);
        }


        [HttpGet]
        public async Task<List<Item>> GetFileItems()
        {
            var result = await _itemRepository.GetAllAsync();
            return result;
        }

        //RecurringJob.AddOrUpdate((Item item) => Creating(item), Cron.Minutely);
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> FileItemCreate([FromBody] Item item)
        {
            Item newitem = new Item()
            {
                
                Name = "base.txt",
                Path = "D:",
                IsSaveByChange = false,
                Period = new TimeSpan(0, 0, 1, 0),
                MaxNumCopy = 5
            };
            var result = await _itemRepository.CreateAsync(newitem);
            if (result == null) { 
                return StatusCode(500, "Failed to create item"); 
            }

            string cronExpression = string.Format("{0} {1} * * * *", newitem.Period.Seconds, string.Join(",", Enumerable.Range(0, 60 / newitem.Period.Minutes).Select(x => x * newitem.Period.Minutes)));
            RecurringJob.AddOrUpdate( () => CreatingAsync(newitem), cronExpression);

            return StatusCode(200, "Success"); ;
           
        }
    }
}