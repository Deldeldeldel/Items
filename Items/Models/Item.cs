using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Items.Models
{
    public partial class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemLocation { get; set; }
        public string ItemBox { get; set; }
        public string ItemDescription { get; set; }
        public string ItemOwner { get; set; }
        public string ItemClass { get; set; }
    }
}
