using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Time.Models
{
    public class Owner
    {
        [Key]
        [Required]
        [Column("Id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column("EmployeeId"), ForeignKey(nameof(Employee))]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public virtual List<PlaceOfWork> PlaceOfWorks { get; set; } = new List<PlaceOfWork>();
    }
}
