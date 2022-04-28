using System;
using System.Collections.Generic;

namespace WebTimTro.Data
{
    public class DichVu: IEquatable<DichVu>
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Icon { get; set; }

        // 1 dịch vụ có thể thuộc nhiều phòng trọ 
        // khác nhau
        public ICollection<PhongTroDichVu> PhongTroDichVus { get; set; }

        public bool Equals(DichVu other)
        {
            if (other == null) return false;
            return (Id == other.Id && Ten == other.Ten);
        }
    }
}
