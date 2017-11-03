using AdSuit.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.DAL.Models
{
    [Table("Employees")]
    public class Employee
    {
        public Employee()
        {
            EmployeeProperties = new Collection<EmployeeProperties>();
            EmployeeTags = new Collection<EmployeeTags>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name Is Required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }
        public string Tags { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Deleted { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public virtual ICollection<EmployeeProperties> EmployeeProperties { get; set; }
        public virtual ICollection<EmployeeTags> EmployeeTags { get; set; }

        //public virtual ICollection<Tags> Tags { get; set; }
    }
}
