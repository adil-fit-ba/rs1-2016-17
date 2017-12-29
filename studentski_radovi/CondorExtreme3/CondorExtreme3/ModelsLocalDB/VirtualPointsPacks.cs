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
    [Table("VirtualPointsPacks")]
    public class VirtualPointsPack
    {
        [Key]
        public int VirtualPointsPackID { get; set; }
       
        public int Amount { get; set; }

        public virtual ICollection<SalesOrderVirtualPoints> SalesOrderVirtualPoints { get; set; }

        public bool IsDeleted { get; set; }

    }
}
