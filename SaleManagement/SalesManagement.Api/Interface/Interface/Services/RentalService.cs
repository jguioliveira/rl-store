using System;
using System.Collections.Generic;
using System.Text;
using Interface.Services;
using Interface.Entities;

namespace Interface.Services
{
    class RentalService
    {
        public double PricePerHour { get; set; }
        public double PricePerDay { get; set; }

        private ITaxService _taxService;

        public RentalService(double pricePerHour, double pricePerDay, ITaxService taxService)
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
            _taxService = taxService;
        }

        public void ProcessInvoice(CarRental carRental)
        {
            TimeSpan duration = carRental.Finish.Subtract(carRental.Start);

            double BasicPayment = 0.0;
            if(duration.TotalHours <= 12.0)
            {
                BasicPayment = PricePerHour * Math.Ceiling(duration.TotalHours);
            }
            else
            {
                BasicPayment = PricePerDay * Math.Ceiling(duration.TotalDays);
            }
            double tax = _taxService.Tax(BasicPayment);

            carRental.Invoice = new Invoice(BasicPayment, tax);
        }
    }
}
