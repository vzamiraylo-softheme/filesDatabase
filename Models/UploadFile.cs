using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace filesDatabase.Models
{
    public partial class UploadedFile
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string fileDescription { get; set; }
        public string filePath { get; set; }
    }
}