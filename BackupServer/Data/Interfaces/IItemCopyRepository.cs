using BackupServer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupServer.Data.Interfaces
{
    public interface IItemCopyRepository
    {
        Task CreateAsync(ItemCopy itemCopy);

        Task<List<ItemCopy>> GetAllCopies();
        Task<List<ItemCopy>> GetAllItemCopies(Item item);

        Task DeleteAllItemCopiesAsync(Item item);
    }
}
