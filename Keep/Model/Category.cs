using System.ComponentModel.DataAnnotations;
using Keep.Areas.Identity.Data;

namespace Keep.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public List<Note> Notes { get; set; }
        public string KeepUserId { get; set; }
       
    }
}
