using AutoMapper;
using CarRentalApp.Domain.Entities;
using CarRentalApp.DTOs.Booking;
using CarRentalApp.DTOs.Branch;
using CarRentalApp.DTOs.Car;
using CarRentalApp.DTOs.Category;
using CarRentalApp.DTOs.Customer;
using CarRentalApp.DTOs.payment;

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

    private void BookingMappings()
    {
        CreateMap<Booking, BookingDto>()
            // Rule 1 , Id , CustomerId , CarId , StartDate , EndDate , TotalAmount , Status , CreatedAt 
            
            // Rule 2 Status Enum >> string 
            .ForMember(dest=>dest.Status,
                src=>
                    src.MapFrom(dest=>dest.Status.ToString()))
            
            
            .ForMember(dest =>dest.RentalDays,
                src => 
                    src.MapFrom(dest => dest.RentalDays))
            .ForMember(dest=>dest.ExtrasTotal,
                src=>
                    src.MapFrom(dest=>dest.ExtrasTotal))
            .ForMember(dest=>dest.GrandTotal,
                src=>
                src.MapFrom(dest=>dest.GrandTotal))
               
            
            // Rule 3 Flatten Car
            
            .ForMember(dest => dest.CarMake,
                opt => opt.MapFrom(
                    src => src.Car != null ? src.Car.Make : string.Empty))
            
            .ForMember(dest => dest.CarModel,
                opt => opt.MapFrom(
                    src => src.Car != null ? src.Car.Model : string.Empty))
        
            .ForMember(dest => dest.CarImageUrl,
                opt => opt.MapFrom(
                    src => src.Car != null ? src.Car.ImageUrl : string.Empty))
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(
                    src => src.Car != null && src.Car.Category != null ? src.Car.Category.Name : string.Empty))
            
            // Rule 3 — flatten Customer
            .ForMember(dest => dest.CustomerFullName,
                opt  => opt.MapFrom(src => src.Customer != null
                    ? src.Customer.FullName
                    : string.Empty))
            .ForMember(dest => dest.CustomerEmailAddress,
                opt  => opt.MapFrom(src => src.Customer != null
                    ? src.Customer.Email
                    : string.Empty))
            .ForMember(dest => dest.CustomerPhoneNumber,
                opt  => opt.MapFrom(src => src.Customer != null
                    ? src.Customer.PhoneNumber
                    : string.Empty))

            // Rule 3 — flatten Payment
            .ForMember(dest => dest.PaymentStatus,
                opt  => opt.MapFrom(src => src.Payment != null
                    ? src.Payment.Status.ToString()
                    : null))
            .ForMember(dest => dest.PaymentMethod,
                opt  => opt.MapFrom(src => src.Payment != null
                    ? src.Payment.Method.ToString()
                    : null));


        CreateMap<Booking, BookingSummaryDto>()
            .ForMember(dest => dest.Status,
                src =>
                    src.MapFrom(dest => dest.Status.ToString()))
            .ForMember(dest => dest.GrandTotal,
                src =>
                    src.MapFrom(dest => dest.GrandTotal))
            .ForMember(dest => dest.CustomerName,
                opt =>
                    opt.MapFrom(src => src.Customer != null ? src.Customer.FullName : string.Empty))
            .ForMember(dest => dest.CarName
                , opt => 
                    opt.MapFrom(src => src.Car != null ? $"{src.Car.Make} {src.Car.Model}" : string.Empty));
            
        // BookingExtraLine → BookingExtraLineDto
        CreateMap<BookingExtraLine, BookingExtraDto>()
                
            .ForMember(dest => dest.ExtraName,
                opt  => opt.MapFrom(src => src.BookingExtra != null
                    ? src.BookingExtra.Name
                    : string.Empty))
            .ForMember(dest => dest.TotalPrice,
                opt  => opt.MapFrom(src => src.TotalPrice));
            

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
    
    // ── Customer ──────────────────────────────────────────────────
    private void CustomerMappings()
    {
        CreateMap<Customer, CustomerDto>()
            // Rule 1 — Id, FirstName, LastName,
            //          Email, PhoneNumber, Address → auto mapped

            // Rule 2 — computed domain properties
            .ForMember(dest => dest.FullName,
                opt  => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Age,
                opt  => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.HasLicense,
                opt  => opt.MapFrom(src => !string.IsNullOrEmpty(
                    src.DriverLicense)))
            .ForMember(dest => dest.TotalBookings,
                opt  => opt.MapFrom(src => src.Bookings != null
                    ? src.Bookings.Count
                    : 0));

        // RegisterCustomerDto → Customer
        CreateMap<RegisterCustomerDto, Customer>()
            .ConstructUsing(src => new Customer(
                src.FirstName,
                src.LastName,
                src.Email,
                src.PhoneNumber,
                src.Address ?? string.Empty,
                src.NationalId,
                src.DateOfBirth));
    }
    
    // ── Payment ───────────────────────────────────────────────────
    private void PaymentMappings()
    {
        CreateMap<Payment,PaymentDto>()
            // Rule 1 — Id, Amount, TransactionId, PaidAt → auto mapped

            // Rule 2 — enums → strings
            .ForMember(dest => dest.Status,
                opt  => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Method,
                opt  => opt.MapFrom(src => src.Method.ToString()))

            // Rule 3 — flatten Booking
            .ForMember(dest => dest.BookingNumber,
                opt  => opt.MapFrom(src => src.Booking != null
                    ? src.Booking.BookingNumber
                    : string.Empty));
    }
    
    // ── Branch ────────────────────────────────────────────────────
    private void BranchMappings()
    {
        // Rule 1 only — all properties match by name
        // Id, Name, PhoneNumber, Address, City, IsActive → auto mapped
        CreateMap<Branch,BranchDto>()
            .ForMember(dest => dest.TotalCars,
                opt  => opt.MapFrom(src => src.Cars != null
                    ? src.Cars.Count
                    : 0));

        // BranchDto → Branch (reverse for create/update)
        CreateMap<BranchDto, Branch>()
            .ConstructUsing(src => new Branch(
                src.Name,
                src.PhoneNumber,
                src.Address,
                src.City));
    }
    // ── Category ──────────────────────────────────────────────────
    private void CategoryMappings()
    {
        // Rule 1 — Id, Name, Description, BaseDailyRate → auto mapped
        CreateMap<Category,CategoryDto>()
            .ForMember(dest => dest.TotalCars,
                opt  => opt.MapFrom(src => src.Cars != null
                    ? src.Cars.Count
                    : 0));

        // CategoryDto → Category (reverse for create/update)
        CreateMap<CategoryDto, Category>()
            .ConstructUsing(src => new Category(
                src.Name,
                src.Description,
                src.BaseDailyRate));
    }
}
