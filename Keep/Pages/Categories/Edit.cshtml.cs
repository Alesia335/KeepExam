using Keep.Data;
using Keep.Model;
using Keep.Repositories;
using Keep.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Keep.Pages.Categories
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly NotesRepository _repository;
        public EditModel(NotesRepository repository)
        {
            _repository = repository;         
        }
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            
            Category = _repository.GetCategory(id);
        }
        public async Task<IActionResult> OnPost()
        {         
            var categoryFromDb = _repository.GetCategory(Category.Id);
            categoryFromDb.Name = Category.Name;
            _repository.UpdateGategory(categoryFromDb);
            return RedirectToPage("Index");
        }
    }
}
