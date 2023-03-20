using System;

namespace CodingTest.TestQuestions.Question3.Models
{
    public class SiteEntity
    {
        public string SiteName { get; private set; }
        public Address Address { get; private set; }
        public decimal TaxRate { get; private set; }
        public DateTime CreatedDateTime { get; private set; }

        public SiteEntity(string siteName, Address address, decimal taxRate)
        {
            SiteName = siteName;
            Address = address;
            TaxRate = taxRate;
        }
    }
}
