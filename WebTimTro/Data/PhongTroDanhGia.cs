namespace WebTimTro.Data
{
    public class PhongTroDanhGia
    {
        public string NguoiDungId { get; set; }
        public NguoiDung NguoiDung { get; set; }
        public int PhongTroId { get; set; }
        public PhongTro PhongTro { get; set; }
        public int Rating { get; set; }
    }
}
