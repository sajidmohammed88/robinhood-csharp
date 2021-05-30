using RobinhoodLibrary.Data.Base;
using RobinhoodLibrary.Data.Quote;
using System;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Crypto
{
    public class CryptoHistoricalData : BaseHistoricalData
    {
        public IList<Historical> DataPoints { get; set; }

        public Guid Id { get; set; }
    }
}
