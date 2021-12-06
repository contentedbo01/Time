using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Time.Models
{
    public class Position
    {
        [Key]
        [Required]
        [Column("Id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual List<Employee> Employees { get; set; } = new List<Employee>();

    }
}
