using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using src.Configuation;
using src.Controllers;
using src.Dto;
using src.Persistence.Model;
using src.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace test
{
    public class NoteBookControllerTest
    {
        private ILogger<NoteBookController> logger;
        private IMapper imapper;

        [SetUp]
        public void Setup()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            logger = new Logger<NoteBookController>(loggerFactory);
            imapper = new ApplicationConfiguration().CreateMapper();
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void TestGetAllNotebooks()
        {
            // Arrange
            var service = new Mock<NoteBookService>();

            List<Note> notebookOneNotes = new List<Note>();
            NoteBook noteBookOne = new NoteBook() { Id = 1, Title = "First Notebook", Notes = notebookOneNotes };
            notebookOneNotes.Add(new Note { Id = 1, Text = "Note One", Notebook = noteBookOne, NoteBookId = 1 });
            notebookOneNotes.Add(new Note { Id = 2, Text = "Note Two", Notebook = noteBookOne, NoteBookId = 1 });

            List<Note> notebookTwoNotes = new List<Note>();
            NoteBook noteBookTwo = new NoteBook() { Id = 2, Title = "Second Notebook", Notes = notebookTwoNotes };
            notebookTwoNotes.Add(new Note { Id = 3, Text = "Note Three", Notebook = noteBookTwo, NoteBookId = 2 });
            notebookTwoNotes.Add(new Note { Id = 4, Text = "Note Four", Notebook = noteBookTwo, NoteBookId = 2 });

            List<NoteBook> notebooks = new List<NoteBook>();
            notebooks.Add(noteBookOne);
            notebooks.Add(noteBookTwo);

            Task<List<NoteBook>> taskNoteBookList = Task<List<NoteBook>>.Factory.StartNew(() => notebooks);
            service.Setup(service => service.GetAllNoteBooks()).Returns(taskNoteBookList);


            List<NoteBookWithoutNotesDto> expected = new List<NoteBookWithoutNotesDto>();
            expected.Add(new NoteBookWithoutNotesDto() { Id = 1, Title = "First Notebook" });
            expected.Add(new NoteBookWithoutNotesDto() { Id = 2, Title = "Second Notebook" });

            // Act
            NoteBookController noteBookController = new NoteBookController(logger, service.Object, imapper);
            var actual = noteBookController.GetAllNoteBooksAsync();

            // Assert
            Assert.AreEqual(expected, actual.Result);
        }
    }
}