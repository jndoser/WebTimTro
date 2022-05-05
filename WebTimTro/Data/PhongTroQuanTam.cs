namespace WebTimTro.Data
{
    public class PhongTroQuanTam
    {
        public string NguoiDungId { get; set; }
        public NguoiDung NguoiDung { get; set; }
        public int PhongTroId { get; set; }
        public PhongTro PhongTro { get; set; }
    }
}
