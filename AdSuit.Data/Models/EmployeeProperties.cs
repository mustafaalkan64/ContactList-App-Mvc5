using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.DAL.Models
{
    [Table("EmployeeProperties")]
    public class EmployeeProperties
    {
        [Key]
        public int Id { get; set; }
        public string ContactType { get; set; }
        public string Value { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> EmployeeHistories_Id { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
