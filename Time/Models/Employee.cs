using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [Column("Id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }


        [Column("UserId"), ForeignKey(nameof(User))]
         public string UserId { get; set; }
         public User User { get; set; }


         public virtual List<TimeWorked> Employees { get; set; } = new List<TimeWorked>();

        [Column("PlaceOfWorkId"), ForeignKey(nameof(PlaceOfWork))] 
         public string PlaceOfWorkId { get; set; }
         public virtual PlaceOfWork PlaceOfWork { get; set; }


        [Column("PositionId"), ForeignKey(nameof(Position))]
         public string PositionId { get; set; }
         public  virtual  Position Position { get; set; }


    }
}
