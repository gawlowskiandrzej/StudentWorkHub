using AutoMapper;
using offer_manager.Models.Offers.dtoObjects;
using UnifiedOfferSchema;

namespace offer_manager.Models.Others.AutoMapper
{

    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<UOS, OfferDTO>();

            CreateMap<Company, CompanyDto>();
            CreateMap<Employment, EmploymentDto>();
            CreateMap<Salary, SalaryDto>();
            CreateMap<Dates, DatesDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Requirements, RequirementsDto>();
            CreateMap<Location, LocationDto>();

            CreateMap<Skills, SkillDto>();
            CreateMap<Languages, LanguageDto>();
        }
    }
}
