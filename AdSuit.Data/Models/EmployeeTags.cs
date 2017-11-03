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
    [Table("EmployeeTags")]
    public class EmployeeTags
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TagId { get; set; }
        public Nullable<int> EmployeeHistories_Id { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
