using AutoMapper;
using CarRentalApp.Domain.Entities;
using CarRentalApp.DTOs.Car;

namespace CarRentalApp.Mapping;

public class MappingProfile : Profile 
{
    public MappingProfile()
    {
        // ── Car Mappings ──────────────────────────────────────────
        CarMappings();

        // ── Booking Mappings ──────────────────────────────────────
        BookingMappings();

        // ── Customer Mappings ─────────────────────────────────────
        CustomerMappings();

        // ── Payment Mappings ──────────────────────────────────────
        PaymentMappings();

        // ── Branch Mappings ───────────────────────────────────────
        BranchMappings();

        // ── Category Mappings ─────────────────────────────────────
        CategoryMappings();
    }

    private void CarMappings()
    {
        
        // Rule 1 , Id , Make , Model , Year ,Color ,Seats
        // ,DailyRate ,ImageUrl ,IsActive ,CreatedAt 
        // (Same name and same type so no changes !)
        
        
        // Role 2 the car status needs to be converted to be string instead of Enum  
        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.Status
                , opt =>
                    opt.MapFrom(src => src.Status.ToString()))
            // Rule 3 — nested properties need flattening
            .ForMember(dest => dest.CategoryName,
                opt  => opt.MapFrom(src => src.Category != null
                    ? src.Category.Name
                    : string.Empty))
            .ForMember(dest => dest.BranchName,
                opt  => opt.MapFrom(src => src.Branch != null
                    ? src.Branch.Name
                    : string.Empty))
            .ForMember(dest => dest.BranchCity,
                opt  => opt.MapFrom(src => src.Branch != null
                    ? src.Branch.City
                    : string.Empty));
    }
}