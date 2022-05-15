using System;

namespace WebTimTro.Models
{
    public class SearchPhongTroOptionsVM: IEquatable<SearchPhongTroOptionsVM>
    {
        public string KhuVuc { get; set; }
        public long Gia { get; set; }
        public int SoNguoiO { get; set; }
        public string TienIchLanCan { get; set; }

        public bool Equals(SearchPhongTroOptionsVM other)
        {
            if (other == null)
                return false;

            if (this.KhuVuc.Equals(other.KhuVuc) 
                && this.Gia == other.Gia
                && this.SoNguoiO == other.SoNguoiO
                && this.TienIchLanCan.Equals(other.TienIchLanCan))
                return true;
            else
                return false;
        }
    }
}
