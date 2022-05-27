using Keep.Data;
using Keep.Model;
using Keep.Repositories;
using Keep.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Keep.Pages.Notes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly NotesRepository _repository;
        private readonly UserService _userService;

        [BindProperty]
        public Note Note { get; set; }
        public SelectList Categories { get; set; }
        public List<Category> CategoriesUser { get; set; }
        public CreateModel(NotesRepository repository, UserService userService)
        {
            _repository = repository;
            _userService = userService;
        }
        public void OnGet()
        {        
            var userId = _userService.GetUserId();
            var categories = _repository.GetCategoriesByUserId(userId);
            Categories = new SelectList(categories, "Id", "Name");
            CategoriesUser = _repository.GetCategoriesByUserId(userId);

        }
        public async Task<IActionResult> OnPost()

        {
          
            if (Note.Title == null && Note.CategoryId == 0)
            {
                return Page();
            }
            _repository.CreateNote(Note);
            return RedirectToPage("Index");
  
        }
    }
}
