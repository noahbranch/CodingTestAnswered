using System;
using CodingTest.TestQuestions.Question3.Application;
using CodingTest.TestQuestions.Question3.Models;
using CodingTest.TestQuestions.Question3.Repositories;

namespace CodingTest.TestQuestions.Question3
{
    public class SiteCreationService : ISiteCreationService
    {
        private readonly ISiteRepository _repository;
        private readonly ITaxesRepository _taxesRepository;

        public SiteCreationService(ISiteRepository repository, ITaxesRepository taxesRepository)
        {
            _repository = repository;
            _taxesRepository = taxesRepository;
        }

        /// <summary>
        /// This function takes in the SiteViewModel from our application and looks up the zip code to find the applicable tax rates, then
        /// creates the site with the tax rates added.  If the tax rate cannot be found, throw an InvalidOperationException; if the
        /// site already exists, throw an InvalidOperationException.  Save the site to the database, and return the newly created site.
        /// </summary>
        /// <param name="svm"></param>
        public SiteEntity CreateSite(SiteViewModel svm)
        {
            var zipCode = svm.ZipCode;

            var taxRateByZip = _taxesRepository.GetRatesByZip(zipCode);

            if (taxRateByZip == null) {
                throw new InvalidOperationException("Cannot find tax rate");
            }

            if (_repository.SiteExists(svm.SiteName)) {
                throw new InvalidOperationException("Site already exists!");
            }

            throw new NotImplementedException();
        }
    }
}
