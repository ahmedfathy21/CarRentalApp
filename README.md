# 🚗 CarRentalApp

ASP.NET Core MVC project for a car rental system built with **Clean Architecture** principles.

---

## Structure

- `CarRentalApp.sln` — solution file
- `CarRentalApp/` — web application
- `CarRentalApp/Domain/` — domain entities and enums

---

## Run Locally

```bash
dotnet restore
dotnet build
dotnet run --project CarRentalApp/CarRentalApp.csproj
```

Then open `https://localhost:5001` (or the URL shown in terminal).

---

## Domain Layer Architecture

### Entity Relationship Overview

```
                          ┌─────────────────────┐
                          │      BaseEntity      │  ← BASE
                          │─────────────────────│
                          │ 🔑 Id         int   │
                          │  CreatedAt  DateTime │
                          │  UpdatedAt  DateTime?│
                          │  IsDeleted  bool     │
                          └──────────┬──────────┘
                                     │ (all entities inherit)
              ┌──────────────────────┼─────────────────────────┐
              │                      │                         │
   ┌──────────▼──────┐   ┌──────────▼──────┐    ┌────────────▼────────┐
   │     Branch      │   │    Category     │    │      Customer       │  ← ENTITY
   │─────────────────│   │─────────────────│    │────────────────────-│
   │ 🔑 Id           │   │ 🔑 Id           │    │ 🔑 Id               │
   │  Name           │   │  Name           │    │  FirstName/LastName │
   │  PhoneNumber    │   │  Description    │    │  Email              │
   │  Address        │   │  BaseDailyRate  │    │  PhoneNumber        │
   │  City           │   │─────────────────│    │  NationalId         │
   │  IsActive       │   │  ↳ Cars         │    │  DriverLicense      │
   │─────────────────│   └────────┬────────┘    │  DateOfBirth        │
   │  ↳ Cars         │            │             │  IsBlacklisted      │
   └────────┬────────┘            │             │  🔗 UserId          │
            │                     │             │─────────────────────│
            │         ┌───────────▼─────────────│  ↳ Bookings         │
            │         │                         └──────────┬──────────┘
            │    ┌────▼────────────────────┐              │
            └───►│          Car            │  ← ENTITY    │
                 │─────────────────────────│              │
                 │ 🔑 Id                   │              │
                 │  Make / Model           │              │
                 │  Year                   │              │
                 │  LicensePlate           │              │
                 │  Color / Seats          │              │
                 │  DailyRate              │              │
                 │  Mileage                │              │
                 │  🔗 CategoryId          │              │
                 │  🔗 BranchId            │              │
                 │  Status → [CarStatus]   │              │
                 │─────────────────────────│              │
                 │  ↳ Bookings             │              │
                 └────────────┬────────────┘              │
                              │                           │
                              └──────────┬────────────────┘
                                         │
                           ┌─────────────▼───────────────┐
                           │         Booking              │  ← AGGREGATE ROOT
                           │─────────────────────────────│
                           │ 🔑 Id                        │
                           │  BookingNumber               │
                           │  StartDate / EndDate         │
                           │  ActualReturnDate            │
                           │  CarDailyRate                │
                           │  TotalAmount                 │
                           │  LateFee                     │
                           │  Status → [BookingStatus]    │
                           │  🔗 CarId                    │
                           │  🔗 CustomerId               │
                           │─────────────────────────────│
                           │  ↳ ExtraLines                │
                           │  ↳ Payment                   │
                           └──────────┬──────────────────┘
                                      │
                    ┌─────────────────┼──────────────────┐
                    │                                     │
       ┌────────────▼────────────┐         ┌─────────────▼──────────┐
       │    BookingExtraLine     │         │        Payment          │  ← ENTITY
       │  ── JUNCTION ──────────│         │────────────────────────-│
       │  🔗 BookingId           │         │ 🔑 Id                   │
       │  🔗 BookingExtraId      │         │  Amount                 │
       │  PriceAtBooking         │         │  Status → [PaymentStatus│
       │  Quantity               │         │  Method → [PaymentMethod│
       │  TotalPrice (computed)  │         │  TransactionId          │
       └────────────┬────────────┘         │  PaidAt                 │
                    │                      │  🔗 BookingId           │
       ┌────────────▼────────────┐         └─────────────────────────┘
       │      BookingExtra       │  ← ENTITY
       │─────────────────────────│
       │ 🔑 Id                   │
       │  Name                   │
       │  Description            │
       │  DailyPrice             │
       │  IsAvailable            │
       │─────────────────────────│
       │  ↳ ExtraLines           │
       └─────────────────────────┘
```

---

### Enums

```
CarStatus                   BookingStatus
──────────────────          ──────────────────────
1  Available                1  Pending
2  Rented                   2  Confirmed
3  UnderMaintenance         3  Active
4  Retired                  4  Completed
                            5  Cancelled

PaymentStatus               PaymentMethod
──────────────────          ──────────────────────
1  Pending                  1  Cash
2  Paid                     2  CreditCard
3  Refunded                 3  DebitCard
4  Failed                   4  OnlineTransfer
```

---

### Relationships

| From | Relationship | To |
|---|---|---|
| `Branch` | 1 → N | `Car` |
| `Category` | 1 → N | `Car` |
| `Car` | 1 → N | `Booking` |
| `Customer` | 1 → N | `Booking` |
| `Booking` | 1 → 1 | `Payment` |
| `Booking` ↔ `BookingExtra` | M → N | via `BookingExtraLine` |
| All entities | inherit | `BaseEntity` |

---

### Key Business Rules

| Rule | Detail |
|---|---|
| Car availability | Car must have `CarStatus.Available` to be booked |
| Customer eligibility | Must be 21+ with a valid driver's license |
| Blacklist check | Blacklisted customers cannot create bookings |
| Max booking duration | 90 days maximum per booking |
| Late return fee | Extra days charged at **1.5×** the daily rate |
| Price snapshot | `PriceAtBooking` on `BookingExtraLine` locks the price at booking time |
| Soft delete | All entities support soft delete via `IsDeleted` on `BaseEntity` |

---

### Aggregate Roots

`Booking` is the main **Aggregate Root** in this domain. You never modify `BookingExtraLine` or `Payment` directly — all changes go through `Booking`:

```csharp
// ✅ Correct — through the aggregate root
booking.AddExtra(extraLine);
booking.Confirm();
booking.Complete(returnDate);

// ❌ Wrong — bypassing the root
dbContext.BookingExtraLines.Add(line);
```

---

## Architecture Layers

```
CarRentalApp.Web           ← Controllers, Views, ViewModels
       ↓
CarRentalApp.Application   ← Services, DTOs, Validators
       ↓
CarRentalApp.Domain        ← Entities, Enums, Interfaces (no dependencies)
       ↑
CarRentalApp.Infrastructure ← EF Core, Repositories, External Services
```