using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class CategoryViewModel
    {
        public int CatID { get; set; }
        public string CatName { get; set; }
        public string Description { get; set; }
    }
}