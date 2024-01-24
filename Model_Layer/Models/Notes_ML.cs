using System;
using System.Collections.Generic;
using System.Text;

namespace Model_Layer.Models
{
    public class Notes_ML
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime Remainder { get; set; }
        public string color { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool Istrash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
