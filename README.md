# CarRentalApp

CarRentalApp is an ASP.NET Core MVC project for a car-rental domain.  
The codebase currently contains a full domain model, EF Core persistence layer, and repository abstractions/implementations, with an MVC shell UI.

## Tech Stack

- .NET `10.0` (`Microsoft.NET.Sdk.Web`)
- ASP.NET Core MVC
- Entity Framework Core `10.0.1`
- ASP.NET Core Identity EF Core `10.0.1`
- SQL Server provider (`Microsoft.EntityFrameworkCore.SqlServer`)
- Stripe SDK (`Stripe.net`)

## Current Project Structure

```text
CarRentalApp/
├── CarRentalApp.csproj
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── Controllers/
│   └── HomeController.cs
├── Domain/
│   ├── Entities/
│   │   ├── BaseEntity.cs
│   │   ├── Branch.cs
│   │   ├── Category.cs
│   │   ├── Car.cs
│   │   ├── Customer.cs
│   │   ├── Booking.cs
│   │   ├── BookingExtra.cs          (includes BookingExtraLine)
│   │   ├── Payment.cs
│   │   └── ErrorViewModel.cs
│   ├── Enums/
│   │   ├── BookingStatus.cs
│   │   ├── Carstatus.cs
│   │   ├── PaymentMethod.cs
│   │   └── paymentStatus.cs
│   ├── Exceptions/
│   │   ├── DomainException.cs
│   │   ├── BookingExceptions.cs
│   │   ├── CarExceptions.cs
│   │   ├── CustomerExceptions.cs
│   │   ├── PaymentExceptions.cs
│   │   ├── CategoryExceptions.cs
│   │   ├── BranchExceptions.cs
│   │   └── BookingExtraExceptions.cs
│   └── Interfaces/
│       ├── Repositories/
│       │   ├── IBaseRepository.cs
│       │   ├── IBookingRepository.cs
│       │   ├── IBookingExtraRepository.cs
│       │   ├── IBranchRepository.cs
│       │   ├── ICarRepository.cs
│       │   ├── ICategoryRepository.cs
│       │   ├── ICustomerRepository.cs
│       │   ├── IPaymentRepository.cs
│       │   └── IUnitOfWork.cs
│       └── Services/
│           ├── IAvailabilityService.cs
│           ├── IPricingService.cs
│           └── IPaymentGateway.cs
├── Infrastructure/
│   ├── Persistence/
│   │   ├── AppDbContext.cs
│   │   └── Configuration/
│   │       ├── BranchConfiguration.cs
│   │       ├── CategoryConfiguration.cs
│   │       ├── CarConfiguration.cs
│   │       ├── CustomerConfiguration.cs
│   │       ├── BookingConfiguration.cs
│   │       ├── BookingExtraConfiguration.cs
│   │       ├── BookingExtraLineConfiguration.cs
│   │       └── PaymentConfiguration.cs
│   └── Repositories/
│       ├── BaseRepository.cs
│       ├── BookingRepository.cs
│       ├── BookingExtraRepository.cs
│       ├── CarRepository.cs
│       ├── CategoryRepository.cs
│       ├── CustomerRepository.cs
│       └── PaymentRepository.cs
├── Views/
│   ├── Home/
│   ├── Shared/
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
└── wwwroot/
    ├── css/
    ├── js/
    └── lib/
```

## Domain Model Summary

Main entities:

- `Branch`: physical rental branch.
- `Category`: car grouping with base pricing.
- `Car`: fleet car (status, pricing, mileage, branch/category links).
- `Customer`: renter profile with eligibility/blacklist logic.
- `Booking`: rental lifecycle (pending -> confirmed -> active -> completed/cancelled).
- `BookingExtra`: add-ons (GPS, child seat, etc.).
- `BookingExtraLine`: junction between booking and extra with locked booking price.
- `Payment`: one-to-one payment per booking.

Enums used by the domain:

- `CarStatus`: `Available`, `Rented`, `UnderMaintenance`, `Retired`
- `BookingStatus`: `Pending`, `Confirmed`, `Active`, `Completed`, `Cancelled`
- `PaymentStatus`: `Pending`, `Paid`, `Refunded`, `Failed`
- `PaymentMethod`: `Cash`, `CreditCard`, `DebitCard`, `OnlineTransfer`

## Persistence and Data Access

`AppDbContext` defines DbSets for all domain entities and applies:

- Fluent entity configurations from `Infrastructure/Persistence/Configuration`
- Global query filters for soft delete (`IsDeleted`)
- Audit field handling (`CreatedAt`, `UpdatedAt`) inside `SaveChanges` overrides

Repository implementations currently available:

- Generic base CRUD/query repository (`BaseRepository<T>`)
- Booking, car, customer, payment, category, and booking-extra repositories
- Specialized queries (availability, overlap checks, status/date range filtering, etc.)

## Current Application Wiring

`Program.cs` currently wires:

- MVC (`AddControllersWithViews`)
- static assets mapping
- default route (`Home/Index`)

`AppDbContext`, Identity, and repository DI registrations are not yet added in `Program.cs`.

## Run Locally

From this folder (`CarRentalApp/CarRentalApp`):

```bash
dotnet restore
dotnet build
dotnet run
```

Then open the URL printed in the terminal.

## Suggested Next Steps

- Add connection string(s) in `appsettings*.json`
- Register `AppDbContext` and repositories in DI
- Add EF Core migrations and update the database
- Add controllers/use-cases for cars, bookings, customers, and payments
