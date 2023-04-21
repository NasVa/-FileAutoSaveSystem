using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupServer.Data.Models;
using BackupServer.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackupServer.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public readonly ApplicationDbContext _context;
        private readonly IItemCopyRepository _itemCopyRepository;

        public ItemRepository(ApplicationDbContext context, IItemCopyRepository itemCopyRepository)
        {
            _context = context;
            _itemCopyRepository = itemCopyRepository;
        }

        public Item FindByPath(string path, string name)
        {
            return _context.Items.FirstOrDefault(i => i.Path == path && i.Name == name);
        }

        public async Task<string> CreateAsync(Item item)
        {
            if (_context.Items.Where(i => i.Path == item.Path && i.Name == item.Name).ToList<Item>().Count > 0)
            {
                return "This file already added to system.";
            }

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return "Ok";
        }

        public async Task DeleteAsync (Item item, bool IsDeleteItemCopies)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            if (IsDeleteItemCopies)
            {
                await _itemCopyRepository.DeleteAllItemCopiesAsync(item);
            }
        }
        public async Task<List<Item>> GetAllAsync()
        {
            return await _context.Items
                .ToListAsync();
        }

        public async Task EditAsync(Item item, bool IsSaveByIsSaveByChange = false, TimeSpan? period = null, int MaxnUmCopy = 0)
        {
            item.IsSaveByChange = IsSaveByIsSaveByChange;
            

            if (period.HasValue)
            {
                item.Period = period.Value;
            }

            if (MaxnUmCopy > 0)
            {
                item.MaxNumCopy = MaxnUmCopy;
            }
            await _context.SaveChangesAsync();
        }
    }
}
