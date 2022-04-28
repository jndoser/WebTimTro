namespace WebTimTro.Data
{
    public class PhongTroNote
    {
        public int PhongTroId { get; set; }
        public int NoteId { get; set; }
        // 1 phòng trọ có thể có nhiều ghi chú
        // và một ghi chú có thể thuộc nhiều 
        // phòng trọ khác nhau
        public PhongTro PhongTro { get; set; }
        public Note Note { get; set; }
    }
}
