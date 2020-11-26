using System;
using System.Collections.Generic;

#nullable disable

namespace StoreMarket.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal? ItemPrice { get; set; }
        public string ItemImage { get; set; }
        public int? GroupsId { get; set; }

        public virtual Group Groups { get; set; }
    }
}
