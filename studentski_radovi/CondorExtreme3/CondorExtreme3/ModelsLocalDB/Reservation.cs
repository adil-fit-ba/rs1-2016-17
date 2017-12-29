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
    [Table("Reservations")]
    public class Reservation
    {
        [Key] 
        public int ReservationID { get; set; }

        [ForeignKey("Visitor")]
        public int VisitorID { get; set; }
        [ForeignKey("Projection")]
        public int ProjectionID { get; set; }

        public virtual Visitor Visitor { get; set; }
        public virtual Projection Projection { get; set; }
      
        public DateTime ReservationDate { get; set; }

        public DateTime ExpiryDate { get; set; }
       
        public bool ReservationCompleted { get; set; }

        public virtual ReservationOnlineTransaction ReservationOnlineTransaction { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
       
        public bool IsDeleted { get; set; }

        

    }
}
