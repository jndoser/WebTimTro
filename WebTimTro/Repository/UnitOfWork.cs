using Microsoft.AspNetCore.Http;
using System;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IBinhLuanRepository BinhLuan { get; }

        public IDichVuRepository DichVu { get; }

        public INguoiDungRepository NguoiDung { get; }

        public INoteRepository Note { get; }

        public IPhongTroDichVuRepository PhongTroDichVu { get; }

        public IPhongTroNoteRepository PhongTroNote { get; }

        public IPhongTroRepository PhongTro { get; }
        public IHinhAnhRepository HinhAnh { get; }
        public IPhongTroHinhAnhRepository PhongTroHinhAnh { get; }
        public IPhongTroLuuTruRepository PhongTroLuuTru { get; }

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            IHttpContextAccessor httpContext)
        {
            _context = context;

            BinhLuan = new BinhLuanRepository(_context);
            DichVu = new DichVuRepository(_context);
            NguoiDung = new NguoiDungRepository(_context, httpContext);
            Note = new NoteRepository(_context);
            PhongTro = new PhongTroRepository(_context);
            PhongTroDichVu = new PhongTroDichVuRepository(_context);
            PhongTroNote = new PhongTroNoteRepository(_context);
            HinhAnh = new HinhAnhRepository(_context);
            PhongTroHinhAnh = new PhongTroHinhAnhRepository(_context);
            PhongTroLuuTru = new PhongTroLuuTruRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool Save()
        {
            var changes = _context.SaveChanges();
            return changes > 0;
        }
    }
}
