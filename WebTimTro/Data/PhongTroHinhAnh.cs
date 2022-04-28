namespace WebTimTro.Data
{
    public class PhongTroHinhAnh
    {
        public int PhongTroId { get; set; }
        public int HinhAnhId { get; set; }
        public PhongTro PhongTro { get; set; }
        public HinhAnh HinhAnh { get; set; }
    }
}
