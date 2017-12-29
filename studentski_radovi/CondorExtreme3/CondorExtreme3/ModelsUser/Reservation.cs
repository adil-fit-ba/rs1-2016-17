using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondorExtreme3.ModelsUser
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key] 
        public int ReservationID { get; set; }

        [ForeignKey("RegisteredVisitor")]
        public int VisitorID { get; set; }
        public virtual RegisteredVisitor RegisteredVisitor { get; set; }
        public int ProjectionId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ExpiryDate { get; set; }       
        public bool ReservationCompleted { get; set; }
        public string Guid { get; set; }
        public string ConnectionString { get; set; }
        public bool IsDeleted { get; set; }
    }
}
