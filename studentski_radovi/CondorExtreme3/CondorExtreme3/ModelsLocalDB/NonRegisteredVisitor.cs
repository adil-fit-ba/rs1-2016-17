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
    [Table("NonRegisteredVisitors")]
    public class NonRegisteredVisitor:Visitor
    {
      
        public string ActivatonCode { get; set; }

    }
}
