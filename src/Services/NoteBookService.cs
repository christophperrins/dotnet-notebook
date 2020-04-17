using Microsoft.AspNetCore.Mvc;
using src.Persistence.Model;
using src.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    public class NoteBookService
    {
        private readonly IRepository<NoteBook> _repository;

        public NoteBookService()
        {

        }

        public NoteBookService(IRepository<NoteBook> repository)
        {
            _repository = repository;
        }

        public virtual async Task<List<NoteBook>> GetAllNoteBooks()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<NoteBook> GetNoteBook(int id)
        {
            return await _repository.GetSingleAsync(notebook => notebook.Id == id, notebook => notebook.Notes);
        }

        public async Task<NoteBook> CreateNoteBook(NoteBook noteBook)
        {
            _repository.Add(noteBook);
            await _repository.Save();
            return noteBook;
        }

        public async Task<NoteBook> PatchNotebookTitle(NoteBook noteBook)
        {
            NoteBook foundNotebook = await GetNoteBook(noteBook.Id);
            foundNotebook.Title = noteBook.Title;
            await _repository.Save();
            return foundNotebook;
        }

        public async Task<int> DeleteNoteBook(int id)
        {
            NoteBook noteBook = await GetNoteBook(id);
            _repository.Delete(noteBook);
            return await _repository.Save();
        }




        
    }
}
