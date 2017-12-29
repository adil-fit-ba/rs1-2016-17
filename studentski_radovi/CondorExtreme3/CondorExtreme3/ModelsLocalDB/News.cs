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
    [Table("News")]
    public class News
    {
        [Key]
        public int NewsID { get; set; }
        
        public string NewsTitle { get; set; }
       
        public string NewsContent { get; set; }


        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }


        public virtual ICollection<NewsTag> NewsTags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public bool IsDeleted { get; set; }


    }
}
