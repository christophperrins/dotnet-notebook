using AutoMapper;
using src.Dto;
using src.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Configuation
{
    public class ApplicationConfiguration
    {
        public IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NoteTextDto, Note>()
                    .ForMember(note => note.Id, opt => opt.Ignore())
                    .ForMember(note => note.NoteBookId, opt => opt.Ignore())
                    .ForMember(note => note.Notebook, opt => opt.Ignore());

                cfg.CreateMap<NoteWithoutNotebookDto, Note>()
                    .ForMember(note => note.NoteBookId, opt => opt.Ignore())
                    .ForMember(note => note.Notebook, opt => opt.Ignore());

                cfg.CreateMap<Note, NoteWithoutNotebookDto>();

                cfg.CreateMap<NoteBookWithoutNotesDto, NoteBook>()
                    .ForMember(notebook => notebook.Notes, opt => opt.Ignore());

                cfg.CreateMap<NoteBookTitleDto, NoteBook>()
                    .ForMember(notebook => notebook.Id, opt => opt.Ignore())
                    .ForMember(notebook => notebook.Notes, opt => opt.Ignore());

                cfg.CreateMap<NoteBook, NoteBookWithoutNotesDto>();
                cfg.CreateMap<NoteBook, NoteBookDto>();

            });

            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}
