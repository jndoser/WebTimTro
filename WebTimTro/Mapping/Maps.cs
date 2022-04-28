using AutoMapper;
using WebTimTro.Data;
using WebTimTro.Models;

namespace WebTimTro.Mapping
{
    public class Maps: Profile
    {
        public Maps()
        {
            CreateMap<NguoiDung, NguoiDungVM>()
                .ForMember(dest => dest.UploadFile, act => act.Ignore())
                .ReverseMap();

            CreateMap<PhongTro, PhongTroVM>().ReverseMap();

            CreateMap<Note, NoteVM>().ReverseMap();

            CreateMap<DichVu, DichVuCheckBoxVM>()
                .ForMember(dest => dest.IsChecked, act => act.Ignore())
                .ReverseMap();

            CreateMap<DichVu, DichVuVM>().ReverseMap();

            CreateMap<HinhAnh, HinhAnhVM>().ReverseMap();
        }
    }
}
