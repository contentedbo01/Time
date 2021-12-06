using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time.Models;

namespace Time.VIewModels
{
    public class EmployeeView
    {
        public string UserName { get; set; }
        public string PlaceName { get; set; }
        public string PositionName { get; set; }
        public string Id { get; set; }
        public virtual List<PlaceOfWork> PlaceOfWorks { get; set; } = new List<PlaceOfWork>();

        public virtual List<TimeWorked> TimeWorked { get; set; } = new List<TimeWorked>();
        public virtual List<User> User { get; set; } = new List<User>();
    }
}
