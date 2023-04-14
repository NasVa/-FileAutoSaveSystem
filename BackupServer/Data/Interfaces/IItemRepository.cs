using BackupServer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupServer.Data.Interfaces
{
    public interface IItemRepository
    {
        Task<string> CreateAsync(Item item);

        Task DeleteAsync(Item item, bool IsDeleteItemCopies);

        Task<List<Item>> GetAllAsync();

        Task EditAsync(Item item, bool IsSaveByIsSaveByChange = false, TimeSpan? period = null, int MaxnUmCopy = 0);

    }
}
