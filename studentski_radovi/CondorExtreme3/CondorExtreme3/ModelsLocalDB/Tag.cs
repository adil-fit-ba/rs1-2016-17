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
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public int TagID { get; set; }
       
        public string TagName { get; set; }

        public virtual ICollection<NewsTag> NewsTags { get; set; }

        public bool IsDeleted { get; set; }

    }
}
