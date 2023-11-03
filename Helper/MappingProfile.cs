using AutoMapper;
using LAB.Models;
using LAB.ViewModel;

namespace LAB.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Vendor, VendorViewModel>();
            CreateMap<VendorViewModel, Vendor>();
        }
    }
}
