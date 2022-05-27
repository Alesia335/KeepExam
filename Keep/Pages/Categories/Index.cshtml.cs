using Keep.Model;
using Keep.Repositories;
using Keep.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Keep.Pages.Categories
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly NotesRepository _repository;
        private readonly UserService _userService;
        public List<Category> Categories { get; set; }
        public IndexModel(NotesRepository repository, UserService userService)
        {
            _repository = repository;
            _userService = userService;
        }
        public void OnGet()
        {
            var userId = _userService.GetUserId();
            Categories = _repository.GetCategoriesByUserId(userId);
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            
            var category = _repository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            _repository.DeteteGategoryAndNotes(id);
            return RedirectToPage("Index");
        }
    }
}
