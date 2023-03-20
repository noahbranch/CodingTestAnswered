using CodingTest.TestQuestions.Question3.Models;

namespace CodingTest.TestQuestions.Question3.Application
{
    /// <summary>
    /// Site Creation will take in a site view model, which is from a form on our application and will
    /// insert it into the database.
    /// </summary>
    public interface ISiteCreationService
    {
        SiteEntity CreateSite(SiteViewModel svm);
    }
}
