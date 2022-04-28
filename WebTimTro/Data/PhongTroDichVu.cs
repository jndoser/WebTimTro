namespace WebTimTro.Data
{
    public class PhongTroDichVu
    {
        public int PhongTroId;
        public int DichVuId;
        public PhongTro PhongTro { get; set; }
        public DichVu DichVu { get; set; }
    }
}
