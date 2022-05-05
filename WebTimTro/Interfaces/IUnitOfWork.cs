namespace WebTimTro.Interfaces
{
    public interface IUnitOfWork
    {
        IBinhLuanRepository BinhLuan { get; }
        IDichVuRepository DichVu { get; }
        INguoiDungRepository NguoiDung { get; }
        INoteRepository Note { get; }
        IPhongTroDichVuRepository PhongTroDichVu { get; }
        IPhongTroNoteRepository PhongTroNote { get; }
        IPhongTroRepository PhongTro { get; }
        IHinhAnhRepository HinhAnh { get; }
        IPhongTroHinhAnhRepository PhongTroHinhAnh { get; }
        IPhongTroLuuTruRepository PhongTroLuuTru { get; }
        IPhongTroQuanTamRepository PhongTroQuanTam { get; }

        bool Save();
        void Dispose();
    }
}
