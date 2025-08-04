using AutoMapper;
using Scheduling.API.Dto;
using Scheduling.API.Models.Materialized;


namespace Scheduling.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

    

            // Instances
            CreateMap<MonthlyScheduleInstance, MonthlyScheduleInstanceDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ReverseMap();

            CreateMap<MonthlyScheduleItem, MonthlyScheduleItemDto>()
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Meal.Name));

            

            CreateMap<ScheduleTemplateDetail, ScheduleTemplateDetailDto>().ReverseMap();
            CreateMap<ScheduleTemplate, ScheduleTemplateDto>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.TemplateDetails))
                .ReverseMap();

            CreateMap<MonthlyScheduleItem, CalendarMealDto>()
                .ForMember(dest => dest.MealId, opt => opt.MapFrom(src => src.MealId))
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Meal.Name));

            CreateMap<ScheduleCollection, ScheduleCollectionBriefDto>();


        }
    }



}
