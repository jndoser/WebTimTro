using System.Collections.Generic;

namespace WebTimTro.Data
{
    public class HinhAnh
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public ICollection<PhongTroHinhAnh> PhongTroHinhAnhs { get; set; }
    }
}
