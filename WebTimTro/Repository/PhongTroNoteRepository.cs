using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class PhongTroNoteRepository: GenericRepository<PhongTroNote>,
        IPhongTroNoteRepository
    {
        public PhongTroNoteRepository(ApplicationDbContext context): base(context)
        {

        }
    }
}
