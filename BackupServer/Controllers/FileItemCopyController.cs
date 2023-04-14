using BackupServer.Data.Interfaces;
using BackupServer.Data.Models;
using BackupServer.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackupServer.Controllers
{
    [ApiController]
    [Route("api/itemCopyes")]
    public class FileItemCopyController : ControllerBase
    {
        private IItemCopyRepository _itemCopyRepository;

        public FileItemCopyController(IItemCopyRepository itemCopyRepository)
        {
            this._itemCopyRepository = itemCopyRepository;
        }

        [HttpPost("create")]
        public async Task<string> CreateFileItem([FromBody] ItemCopy fileItemCopy)
        {
             await _itemCopyRepository.CreateAsync(fileItemCopy);
            return "Ok";
        }

        [HttpGet]
        public async Task<List<ItemCopy>> GetFileItems()
        {
            var result = await _itemCopyRepository.GetAllItemCopies();
            return result;
        }

    }
}
