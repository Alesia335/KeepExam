using Keep.Model;
using Keep.Repositories;
using Keep.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Keep.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly NotesRepository _repository;
        private readonly UserService _userService;
        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(NotesRepository repository, UserService userService)
        {
            _repository = repository;
            _userService = userService;
        }
        public void OnGet()
        {
            var userId = _userService.GetUserId();
        }
        public async Task<IActionResult> OnPost()

        {
            _repository.CreateCategory(Category);
            return RedirectToPage("Index");
        }
    }
}
