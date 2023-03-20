using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingTest.TestQuestions.Question1
{
    public class BatchRepository
    {
        private readonly IEnumerable<BatchEntity> _batchEntities;

        public BatchRepository(IEnumerable<BatchEntity> batchEntities)
        {
            _batchEntities = batchEntities;
        }

        /// <summary>
        /// In this example, we want to write a linq statement that will select our domain model, which is a BatchEntity
        /// into a BatchViewModel, with a where clause that will select only the uncommitted batches.
        /// You will find the Unit Tests in Question1_Tests, which will allow you to verify if your code is functioning properly.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BatchesViewModel> GetUncommittedBatches()
        {
            return _batchEntities.Where(x => x.Status == BatchStatus.Uncommitted).Select(x => new BatchesViewModel { 
                BatchId = x.BatchId, 
                CreatedDateTime = x.CreatedDateTime, 
                CreatedBy = x.CreatedByUser.Name,
                Status = x.Status.ToString(),
            }).ToList();

            //TODO: Replace this with linq code that will pass the unit tests.
            // Hint:
            // return _batchEntities.Where(...).Select(...);
        }
    }




    //// ASSOCIATED CLASSES BELOW ///////




    /// <summary>
    /// Domain Model of a Batch Entity
    /// </summary>
    public class BatchEntity
    {
        public int BatchId { get; set; }
        public BatchStatus Status { get; set; }
        public int QuantityInBatch { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public virtual User CreatedByUser { get; set; }
    }

    public class User
    {
        public User(int id, string name)
        {
            UserId = id;
            Name = name;
            CreatedDateTime = DateTime.Now;
        }
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
    }

    public enum BatchStatus
    {
        Uncommitted,
        Committed,
        Error
    }

    /// <summary>
    /// View Model of a Batch Entity.  This will be passed to our front end, and so we want to be able to
    /// "hydrate" the model with the "Name" of the person in CreatedBy and the "Name" of the Status in the status field.
    /// </summary>
    public class BatchesViewModel
    {
        public int BatchId { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
