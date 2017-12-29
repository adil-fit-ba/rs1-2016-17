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
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [ForeignKey("News")]
        public int NewsID { get; set; }
        [ForeignKey("RegisteredVisitor")]
        public int VisitorID { get; set; }

        public virtual News News { get; set; }
        public virtual RegisteredVisitor RegisteredVisitor { get; set; }


        public string Content { get; set; }

        public bool IsDeleted { get; set; }

    }
}
