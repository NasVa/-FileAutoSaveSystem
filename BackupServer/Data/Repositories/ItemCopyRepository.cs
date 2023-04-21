using Microsoft.EntityFrameworkCore;
using BackupServer.Data.Interfaces;
using BackupServer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BackupServer.Data.Repositories
{
    public class ItemCopyRepository : IItemCopyRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemCopyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ItemCopy createNewItemCopy()
        {
            return new ItemCopy();
        }


        public async Task CreateAsync(ItemCopy itemCopy)
        {
            if (_context.ItemsCopies.Where(i => i.ParentItem == itemCopy.ParentItem).ToList<ItemCopy>().Count < itemCopy.ParentItem.MaxNumCopy)
            {
                List<ItemCopy> identicalCopies = _context.ItemsCopies.Where(i => i.Hash == itemCopy.Hash).ToList<ItemCopy>();
                if (identicalCopies.Count > 0)
                {
                    itemCopy.Path = identicalCopies.First().Path;
                    _context.ItemsCopies.Add(itemCopy);
                }

            }
            _context.ItemsCopies.Add(itemCopy);
            await _context.SaveChangesAsync();           // error
        }

        public async Task<List<ItemCopy>> GetAllCopies()
        {
            return await _context.ItemsCopies
                .ToListAsync();
        }

        public async Task<List<ItemCopy>> GetAllItemCopies(Item item)
        {
            return await _context.ItemsCopies.Where(i => i.ParentItem == item)
                .ToListAsync();
       }

        public async Task DeleteAllItemCopiesAsync(Item item)
        {

            /*await _context.itemsCopies.Remove()
                .Where(itemCopy => itemCopy.ParentItem == item);*/
            await _context.ItemsCopies
                .ToListAsync();
        }
    }

        
}
