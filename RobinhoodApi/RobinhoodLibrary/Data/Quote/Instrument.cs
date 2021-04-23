using System;

namespace RobinhoodLibrary.Data.Quote
{
    public class Instrument
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Quote { get; set; }

        public string Fundamentals { get; set; }

        public string Splits { get; set; }

        public string State { get; set; }

        public string Market { get; set; }

        public string SimpleName { get; set; }

        public string Name { get; set; }

        public bool Tradeable { get; set; }

        public string Tradability { get; set; }

        public string Symbol { get; set; }

        public string BloombergUnique { get; set; }

        public string MarginInitialRatio { get; set; }

        public string MaintenanceRatio { get; set; }

        public string Country { get; set; }

        public string DayTradeRatio { get; set; }

        public DateTime ListDate { get; set; }

        public string MinTickSize { get; set; }

        public string Type { get; set; }

        public string TradableChainId { get; set; }

        public string RhsTradability { get; set; }

        public string FractionalTradability { get; set; }

        public string DefaultCollarFraction { get; set; }

        public string IpoAccessStatus { get; set; }

        public string IpoAccessCobDeadline { get; set; }

        public string IpoAllocatedPrice { get; set; }

        public string IpoCustomersReceived { get; set; }

        public string IpoCustomersRequested { get; set; }

        public string IpoDate { get; set; }

        public string IpoS1Url { get; set; }

        public bool IsSpac { get; set; }

        public bool IsTest { get; set; }
    }
}
