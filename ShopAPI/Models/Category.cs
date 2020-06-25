using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class Category
    {
        [DisplayName("Category ID")]
        public int CatID { get; set; }
        [DisplayName("Category Name")]
        public string CatName { get; set; }
        [DisplayName("Category Description")]
        public string Description { get; set; }
    }
}