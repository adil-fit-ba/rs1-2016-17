using MVC.Helper;

namespace MVC.Models
{
    public class Smjer:IEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Naziv { get; set; }


        public Fakultet Fakultet { get; set; }
        public int FakultetId { get; set; }
    }
}
