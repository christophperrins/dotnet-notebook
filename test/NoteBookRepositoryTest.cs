using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using src.Data;
using src.Persistence.Model;
using src.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace test
{
    class NoteBookRepositoryTest
    {
        Logger<Repository<NoteBook>> logger;
        ApplicationContext applicationContext;
        [SetUp]
        public void SetUp()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            logger = new Logger<Repository<NoteBook>>(loggerFactory);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseInMemoryDatabase("in-memory-notebook-db");

            applicationContext = new ApplicationContext(optionsBuilder.Options);

            NoteBook noteBookOne = new NoteBook() { Id = 1, Title = "First Notebook", Notes = new List<Note>() };
            NoteBook noteBookTwo = new NoteBook() { Id = 2, Title = "Second Notebook", Notes = new List<Note>() };

            applicationContext.NoteBook.AddRange(
                noteBookOne,
                noteBookTwo
            );

            applicationContext.Note.AddRange(
                new Note() { Id = 1, Text = "Note One", Notebook = noteBookOne, NoteBookId = 1},
                new Note() { Id = 2, Text = "Note Two", Notebook = noteBookOne, NoteBookId = 1},
                new Note() { Id = 3, Text = "Note Third", Notebook = noteBookTwo, NoteBookId = 2 },
                new Note() { Id = 4, Text = "Note Four", Notebook = noteBookTwo, NoteBookId = 2 }
            );

            applicationContext.SaveChanges();


        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void GetAllTest()
        {
            // Arrange
            List<NoteBook> expected = new List<NoteBook>();

            List<Note> notebookOneNotes = new List<Note>();
            NoteBook noteBookOne = new NoteBook() { Id = 1, Title = "First Notebook", Notes = notebookOneNotes };
            notebookOneNotes.Add(new Note { Id = 1, Text = "Note One", Notebook = noteBookOne, NoteBookId = 1 });
            notebookOneNotes.Add(new Note { Id = 2, Text = "Note Two", Notebook = noteBookOne, NoteBookId = 1 });

            List<Note> notebookTwoNotes = new List<Note>();
            NoteBook noteBookTwo = new NoteBook() { Id = 2, Title = "Second Notebook", Notes = notebookTwoNotes };
            notebookTwoNotes.Add(new Note { Id = 3, Text = "Note Three", Notebook = noteBookTwo, NoteBookId = 2 });
            notebookTwoNotes.Add(new Note { Id = 4, Text = "Note Four", Notebook = noteBookTwo, NoteBookId = 2 });

            expected.Add(noteBookOne);
            expected.Add(noteBookTwo);

            Repository<NoteBook> repository = new Repository<NoteBook>(logger, applicationContext);
            
            //Act
            List<NoteBook> actual = repository.GetAllAsync().Result;

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected[0].Notes, actual[0].Notes);
        }
    }
}
