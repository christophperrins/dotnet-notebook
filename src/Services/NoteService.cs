using src.Persistence.Model;
using src.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    public class NoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IRepository<NoteBook> _noteBookRepository;

        public NoteService(IRepository<Note> repository, IRepository<NoteBook> noteBookRepository)
        {
            _noteRepository = repository;
            _noteBookRepository = noteBookRepository;
        }

        public async Task<List<Note>> GetAllNotesInNoteBookAsync(int notebookId)
        {
            NoteBook notebook = await _noteBookRepository.GetSingleAsync(notebook => notebook.Id == notebookId, 
                    notebook => notebook.Notes);
            return notebook.Notes;
        }

        public async Task<Note> AddNoteToNoteBookAsync(int notebookId, Note note)
        {
            note.NoteBookId = notebookId;
            _noteRepository.Add(note);
            await _noteRepository.Save();
            return note;
        }

        public async Task<Note> ChangeTextInsideNoteAsync(Note note)
        {
            Note found = await _noteRepository.GetSingleAsync(note.Id);
            found.Text = note.Text;
            await _noteRepository.Save();
            return found;
        }

        public async Task<int> DeleteNoteAsync(int id)
        {
            Note note = await _noteRepository.GetSingleAsync(id);
            _noteRepository.Delete(note);
            return await _noteRepository.Save();
        }
    }
}
