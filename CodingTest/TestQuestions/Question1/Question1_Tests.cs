using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodingTest.TestQuestions.Question1
{
    public class Question1_Tests
    {
        // example data
        private readonly List<BatchEntity> _testBatches;

        // repository to test
        private readonly BatchRepository _testRepository;

        public Question1_Tests()
        {
            // example data
            _testBatches = new List<BatchEntity>()
            {
                new BatchEntity()
                {
                    BatchId = 1,
                    CreatedByUser = new User(1, "Adam"),
                    CreatedDateTime = DateTime.Now,
                    QuantityInBatch = 10,
                    Status = BatchStatus.Uncommitted
                },
                new BatchEntity()
                {
                    BatchId = 2,
                    CreatedByUser = new User(2, "Graham"),
                    CreatedDateTime = DateTime.Now,
                    QuantityInBatch = 2,
                    Status = BatchStatus.Committed
                },
                new BatchEntity()
                {
                    BatchId = 3,
                    CreatedByUser = new User(1, "Adam"),
                    CreatedDateTime = DateTime.Now,
                    QuantityInBatch = 4,
                    Status = BatchStatus.Uncommitted
                },
                new BatchEntity()
                {
                    BatchId = 4,
                    CreatedByUser = new User(3, "Mitch"),
                    CreatedDateTime = DateTime.Now,
                    QuantityInBatch = 10,
                    Status = BatchStatus.Error
                }
            };

            // creates the test repository with our example data
            _testRepository = new BatchRepository(_testBatches);
        }
        
        [Fact]
        public void GetBatchViewModelReturnsTheCorrectNumberOfBatches()
        {
            var result = _testRepository.GetUncommittedBatches();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetUncommittedBatchesReturnsOnlyCorrectStatus()
        {
            var result = _testRepository.GetUncommittedBatches();
            Assert.All(result, x=> Assert.Equal("Uncommitted", x.Status));
        }

        [Fact]
        public void GetUncommittedBatchesReturnsCorrectNamesOfUsers()
        {
            var result = _testRepository.GetUncommittedBatches();
            foreach (var i in result)
            {
                var expectedUserName = _testBatches.FirstOrDefault(x => i.BatchId == x.BatchId)?.CreatedByUser.Name;
                Assert.Equal(expectedUserName, i.CreatedBy);
            }
        }
    }
}
