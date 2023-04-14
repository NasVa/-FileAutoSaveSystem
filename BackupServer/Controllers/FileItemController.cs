using BackupServer.Data.Interfaces;
using BackupServer.Data.Models;
using BackupServer.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackupServer.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class FileItemController : ControllerBase
    {
        private IItemRepository _itemRepository;

        public FileItemController(IItemRepository itemRepository)
        {
            this._itemRepository = itemRepository;
        }

        [HttpPost("create")]
        public async Task<string> CreateFileItem([FromBody] Item fileItem)
        {
            var result = await _itemRepository.CreateAsync(fileItem);
            return result;
        }

        [HttpGet]
        public async Task<List<Item>> GetFileItems()
        {
            var result = await _itemRepository.GetAllAsync();
            return result;
        }

    }
}
