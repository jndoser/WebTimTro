using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface INoteRepository: IGenericRepository<Note>
    {
        public int GetAddedNoteId();
        public int GetNoteIdByPhongTroId(int phongTroId);
        public void DeleteNotesByPhongTroId(int phongTroId);
    }
}
