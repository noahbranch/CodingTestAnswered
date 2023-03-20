using System.Collections.Generic;
using CodingTest.TestQuestions.Question3.Taxes;

namespace CodingTest.TestQuestions.Question3.Repositories
{
    /// <summary>
    /// The Taxes Repository will return a list of tax rates for a given zip code.
    /// </summary>
    public interface ITaxesRepository
    {
        IEnumerable<TaxRate> GetRatesByZip(string zipCode);
    }
}
