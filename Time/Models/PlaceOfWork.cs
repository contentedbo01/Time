using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Time.Models
{
    public class PlaceOfWork
    {
        [Key]
        [Required]
        [Column("Id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual List<Employee> Employees { get; set; } = new List<Employee>();


        [Column("OwnerId"), ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
