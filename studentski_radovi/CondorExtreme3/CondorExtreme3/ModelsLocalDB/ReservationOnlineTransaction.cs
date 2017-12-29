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
    [Table("ReservationOnlineTransactions")]
    public class ReservationOnlineTransaction
    {
        [Key]
        [ForeignKey("Reservation")]       
        public int ReservationID { get; set; }

        [ForeignKey("PaymentMethod")]
        public int PaymentMethodID { get; set; }


        [ForeignKey("RegisteredVisitor")]
        public int VisitorID { get; set; }

      
        public int VirtualPointsAmount { get; set; }


        public virtual Reservation Reservation { get; set; }


        public virtual PaymentMethod PaymentMethod { get; set; }


        public virtual RegisteredVisitor RegisteredVisitor { get; set; }

        public bool IsDeleted { get; set; }


    }
}
