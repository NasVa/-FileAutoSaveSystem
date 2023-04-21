using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupServer.Data.Models
{
    public class ItemCopy
    {
        public int Id { get; set; }
        public string Hash { get; set; }

        public Item ParentItem { get; set; }

        public ItemCopy? PrevCopy { get; set; }
        public string Path { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
