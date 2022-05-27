using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Keep.Areas.Identity.Data;

namespace Keep.Model
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }  
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string KeepUserId { get; set; }
    }
}
