using System;
using System.Collections.Generic;

#nullable disable

namespace StoreMarket.Models
{
    public partial class Group
    {
        public Group()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
