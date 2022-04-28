using System;

namespace WebTimTro.Models
{
    public class DichVuVM: IEquatable<DichVuVM>
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Icon { get; set; }

        public bool Equals(DichVuVM other)
        {
            if(other == null) return false;
            return (Id == other.Id && Ten == other.Ten);
        }
    }
}
