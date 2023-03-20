using System;
using System.Collections.Generic;
using System.Linq;
using CodingTest.TestQuestions.Question3.Models;
using CodingTest.TestQuestions.Question3.Repositories;
using CodingTest.TestQuestions.Question3.Taxes;
using Moq;
using Xunit;

namespace CodingTest.TestQuestions.Question3
{
    public class Question3_Tests
    {
        private readonly SiteCreationService _testService;
        private readonly Mock<ITaxesRepository> _mockTaxesRepository;
        private readonly Mock<ISiteRepository> _mockSiteRepository;

        private readonly SiteViewModel _testModel;
        private readonly List<TaxRate> _taxRateList;

        public Question3_Tests()
        {
            _mockTaxesRepository = new Mock<ITaxesRepository>();
            _mockSiteRepository = new Mock<ISiteRepository>();

            _testService = new SiteCreationService(_mockSiteRepository.Object, _mockTaxesRepository.Object);

            _testModel = new SiteViewModel()
            {
                SiteName = "A Test Site",
                City = "Chattanooga",
                State = "Tennessee",
                ZipCode = "37405"
            };

            _taxRateList = new List<TaxRate>()
            {
                new TaxRate(){Name="City", Rate = 2.5m, Type = "City"},
                new TaxRate(){Name="State", Rate = 5.0m, Type="State"}
            };
        }

        [Fact]
        public void IfTheSiteAlreadyExists_ThrowInvalidOperationException()
        {
            _mockSiteRepository.Setup(x => x.SiteExists(It.IsAny<string>())).Returns(true);

            Assert.Throws<InvalidOperationException>(() => _testService.CreateSite(_testModel));
            
            // do not call create site
            _mockSiteRepository.Verify(x=>x.CreateSite(It.IsAny<SiteEntity>()), Times.Never);
        }

        [Fact]
        public void IfTheZipCodeCannotBeFoundInTheTaxDatabase_ThrowInvalidOperationException()
        {
            _mockTaxesRepository.Setup(x => x.GetRatesByZip(It.IsAny<string>())).Returns((List<TaxRate>) null);
            Assert.Throws<InvalidOperationException>(() => _testService.CreateSite(_testModel));

            // do not call create site
            _mockSiteRepository.Verify(x => x.CreateSite(It.IsAny<SiteEntity>()), Times.Never);
        }

        [Fact]
        public void IfZipCodeReturnsValidRatesAndSiteDoesNotExist_CreateSite()
        {
            _mockSiteRepository.Setup(x => x.SiteExists(It.IsAny<string>())).Returns(false);
            _mockTaxesRepository.Setup(x => x.GetRatesByZip(It.IsAny<string>())).Returns(_taxRateList);

            var result = _testService.CreateSite(_testModel);

            // check values of Site Entity Created
            Assert.Equal(_taxRateList.Sum(x => x.Rate), result.TaxRate);
            Assert.Equal(_testModel.SiteName, result.SiteName);
            Assert.Equal(_testModel.City, result.Address.City);
            Assert.Equal(_testModel.State, result.Address.State);

            // call create site
            _mockSiteRepository.Verify(x => x.CreateSite(It.IsAny<SiteEntity>()), Times.Once);
        }
    }
}
