using System.Linq;
using Keep.Data;
using Keep.Model;
using Keep.Services;
using Microsoft.EntityFrameworkCore;

namespace Keep.Repositories
{
    public class NotesRepository
    {
        private readonly KeepContext _context;
        private readonly UserService _userService;
        public NotesRepository(KeepContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public List<Note> GetNotes()
        {
           return  _context.Notes.ToList();
        }
        public List<Note> GetNotesByUserId(string id, string title = null, string categoryName = null)
        {
            var notes = _context.Notes.Where(note => note.KeepUserId == id).Include(note => note.Category).Select(note => note);
            if (!string.IsNullOrEmpty(title))
            {
                notes = notes.Where(note => note.Title.Contains(title));
            }
            if (!string.IsNullOrEmpty(categoryName))
            {
                notes = notes.Where(note => note.Category.Name.Contains(categoryName));
            }

            return notes.ToList();
        }
        public List<Category> GetCategoriesByUserId(string id)
        {
            return _context.Categories.Where(c => c.KeepUserId == id).OrderBy(c => c.Name).ToList();
        }
        public Note GetNote(int id)
        {
            return _context.Notes.FirstOrDefault(x => x.Id == id);
        }
        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public void CreateNote(Note note)
        {
            var userId = _userService.GetUserId();
            note.KeepUserId = userId;
            _context.Add(note);
            _context.SaveChanges();
        }
        public void UpdateNote(Note note)
        {
            _context.Entry(note).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeteteNote(int id)
        {
            var note = _context.Notes.FirstOrDefault(b => b.Id == id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
        }
        public void CreateCategory(Category category)
        {
            var userId = _userService.GetUserId();
            category.KeepUserId = userId;
            _context.Add(category);
            _context.SaveChanges();
        }
        public void UpdateGategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeteteGategoryAndNotes(int id)
        {
            
            var note= _context.Notes.FirstOrDefault(b => b.CategoryId == id);
            var category = _context.Categories.FirstOrDefault(b => b.Id == id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
                _context.Categories.Remove(category);
                _context.SaveChanges();

        }
        public List<Note> GetNotesByTitle(string title)
        {
            return _context.Notes.Where(note => note.Title.Contains(title)).Include(note => note.Category).ToList();
        }
        public List<string> GetNotesCategories(string id)
        {
            return _context.Notes.Where(c => c.KeepUserId == id).Include(b => b.Category).Select(b => b.Category.Name).Distinct().ToList();
        }
    }
}
