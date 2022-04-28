using System.Collections.Generic;

namespace WebTimTro.Data
{
    public class Note
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string NoiDung { get; set; }

        public ICollection<PhongTroNote> PhongTroNotes { get; set; }
    }
}
