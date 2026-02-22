using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Domain.Interfaces.Services;

public interface IPricingService
{
    /// <summary>
    /// Calculates the Base price of a booking
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    decimal CalculatingBaseAmount(decimal dailyRate,int days);

    /// <summary>
    /// Calculates the total amount of a booking
    /// </summary>

    decimal CalculatingTotalAmountWithTaxes(decimal baseAmount, IEnumerable<BookingExtraLine> extraLines,int days);
    
    /// <summary>
    /// Calculates the late return fee
    /// </summary>
    decimal CalculatingLateFee(DateTime dailyRate,int extraDays);
    
}