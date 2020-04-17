using src.Persistence.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace src.Persistence.Model
{
    /// <summary>
    ///  Dependent Entity
    /// </summary>
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        /// <summary>
        /// Foreign Key
        /// </summary>
        public int NoteBookId { get; set; }

        /// <summary>
        /// reference navigation property
        /// </summary>
        [ForeignKey("NoteBookId")]
        public NoteBook Notebook { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Note note &&
                   Id == note.Id &&
                   Text == note.Text;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Text);
        }
    }
}
