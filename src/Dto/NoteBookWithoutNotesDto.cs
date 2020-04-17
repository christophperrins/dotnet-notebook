using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dto
{
    public class NoteBookWithoutNotesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            return obj is NoteBookWithoutNotesDto dto &&
                   Id == dto.Id &&
                   Title == dto.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title);
        }
    }
}
