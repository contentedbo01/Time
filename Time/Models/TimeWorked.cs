using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Time.Models
{
    public class TimeWorked
    {
        [Key]
        [Required]
        [Column("Id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        [Column("EmployeeId"), ForeignKey(nameof(Employee))]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
