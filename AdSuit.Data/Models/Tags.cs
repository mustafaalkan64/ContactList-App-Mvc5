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
    [Table("Tags")]
    public class Tags
    {
        public Tags()
        {
            EmployeeTags = new Collection<EmployeeTags>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tag Name Is Required")]
        [StringLength(50, ErrorMessage = "Tag Name cannot be longer than 50 characters.")]
        [Display(Name ="Tag Name")]
        public string TagName { get; set; }
        public virtual ICollection<EmployeeTags> EmployeeTags { get; set; }
    }
}
