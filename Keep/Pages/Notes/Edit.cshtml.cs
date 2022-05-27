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
    public class EditModel : PageModel
    {
        private readonly NotesRepository _repository;
        private readonly UserService _userService;
        [BindProperty]
        public Note Note { get; set; }
        public SelectList Categories { get; set; }
        
        public EditModel(NotesRepository repository, UserService userService)
        {
            _repository = repository;
            _userService = userService;
        }
        public void OnGet(int id)
        {           
            var userId = _userService.GetUserId();
            Note = _repository.GetNote(id);
            var categories = _repository.GetCategoriesByUserId(userId);
            Categories = new SelectList(categories, "Id", "Name");

        }
        public async Task<IActionResult> OnPost()
        {
       
            if (Note.Title == null || Note.CategoryId == 0)
            {
                return Page();
            }
            var noteFromDb = _repository.GetNote(Note.Id);
            noteFromDb.Title = Note.Title;
            noteFromDb.Text = Note.Text;
            noteFromDb.CategoryId = Note.CategoryId;
            _repository.UpdateNote(noteFromDb);
            return RedirectToPage("Index");
        }

    }
}
