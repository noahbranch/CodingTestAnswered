using CodingTest.TestQuestions.Question3.Models;

namespace CodingTest.TestQuestions.Question3.Repositories
{
    /// <summary>
    /// Our Site Repository is the "Database" that we will use to put our site into, and also to verify if the site that is being added
    /// already exists.
    /// </summary>
    public interface ISiteRepository
    {
        void CreateSite(SiteEntity newSite);
        bool SiteExists(string siteName);
    }
}
