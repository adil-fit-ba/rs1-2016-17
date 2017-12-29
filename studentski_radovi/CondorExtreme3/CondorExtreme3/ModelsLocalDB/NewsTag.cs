using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondorExtreme3.ModelsLocalDB
{
    [Table("NewsTags")]
    public class NewsTag
    {
        
        [Column(Order = 1), Key,ForeignKey("News"),]
        public int NewsID { get; set; }
        [Column(Order = 2), Key, ForeignKey("Tag"),]
        public int TagID { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual News News { get; set; }

        public bool IsDeleted { get; set; }


    }
}
