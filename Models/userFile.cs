using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace filesDatabase.Models
{
    public class userFile
    {
        public filesTable file { get; set; }
        public List<Category> categoriesForFile { get; set; } 

        public userFile(filesTable f, List<Category> list )
        {
            file = f;
            categoriesForFile = list;
        }
    }
}