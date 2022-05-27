using System.Security.Claims;
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
    public class IndexModel : PageModel
    {
        private readonly NotesRepository _repository;
        private readonly UserService _userService;
        public List<Note> Notes { get; set; }
        public SelectList Categories { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchInputTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCategoryName { get; set; }

        public IndexModel(NotesRepository repository, UserService userService)
        {
            _repository = repository;
            _userService = userService;
        }
        public IActionResult OnGet()
        {
            var userId = _userService.GetUserId();
            var categories = _repository.GetNotesCategories(userId);
            Categories = new SelectList(categories);            
            Notes = _repository.GetNotesByUserId(userId, SearchInputTitle, SelectedCategoryName);
            return Page();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var note = _repository.GetNote(id);
            if (note == null)
            {
                return NotFound();
            }

            _repository.DeteteNote(id);
            return RedirectToPage("Index");
        }
    }
}
