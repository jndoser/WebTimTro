using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class NoteRepository: GenericRepository<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext contex): base(contex)
        {
            
        }

        public int GetAddedNoteId()
        {
            return _context.Notes.Select(x => x.Id).Max();
        }

        public int GetNoteIdByPhongTroId(int phongTroId)
        {
            return _context.PhongTroNotes
                .FirstOrDefault(x => x.PhongTroId == phongTroId).NoteId;
        }

        public void DeleteNotesByPhongTroId(int phongTroId)
        {
            var phongTroNote =_context.PhongTroNotes
                .FirstOrDefault(x => x.PhongTroId == phongTroId);
            var noteId = _context.PhongTroNotes
                .FirstOrDefault(x => x.PhongTroId == phongTroId)
                .NoteId;

            _context.PhongTroNotes.Remove(phongTroNote);

            var note = _context.Notes
                .FirstOrDefault(x => x.Id == noteId);
            _context.Notes.Remove(note);
        }

        // Lấy nội dung ghi chú của chủ trọng với phòng trọ 
        // tương ứng
        public string GetNoteByPhongTroId(int phongTroId)
        {
            int noteId = _context.PhongTroNotes
                .FirstOrDefault(x => x.PhongTroId == phongTroId).NoteId;

            return _context.Notes.FirstOrDefault(x => x.Id == noteId).Ten;
        }
    }
}
