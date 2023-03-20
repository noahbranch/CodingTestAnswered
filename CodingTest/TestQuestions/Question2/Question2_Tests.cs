using Xunit;

namespace CodingTest.TestQuestions.Question2
{
    public class Question2_Tests
    {
        private readonly MatchingAlgorithm _testAlgorithm;
        public Question2_Tests()
        {
            _testAlgorithm = new MatchingAlgorithm();
        }

        // In this example, we want to be able to match users with their active directory logins
        // We don't care about the "domain" that they are logging into, so we can throw that away.
        // We only care about the username that is being used.
        [Theory]
        // we should be able to match first initial, last name
        [InlineData("Taylor", "Hall", "DATABLY\\THALL", true)]

        // we sould be able to match first.last
        [InlineData("Taylor", "Hall", "datably\\taylor.hall", true)]
        [InlineData("Elizabeth", "Peters", "ORM\\ELIZABETH.PETERS", true)]

        // we should be able to match with middle initial
        [InlineData("John", "Smith", "WORKGROUP\\jtsmith", true)]

        // we do not want to match ones that clearly don't work:
        [InlineData("Taylor", "Hall", "ORM\\epeters", false)]
        [InlineData("Taylor", "Hall", "ORM\\tsmith", false)]
        [InlineData("Taylor", "Hall", "DATABLY\\taylor.smith", false)]
        [InlineData("John", "Smith", "datably\\thall", false)]
        public void TryMatching(string firstName, string lastName, string adLogin, bool shouldMatch)
        {
            Assert.True(shouldMatch == _testAlgorithm.IsMatch(firstName, lastName, adLogin));
        }


        // some users are using their own machines, and so the login is not joined to the domain, meaning that
        // the active directory name does not include the domain name in it.  Handle these cases gracefully.
        [Theory]
        [InlineData("Taylor", "Hall", "thall", true)]
        [InlineData("Taylor", "Hall", "taylor.hall", true)]
        public void BonusMatching(string firstName, string lastName, string adLogin, bool shouldMatch)
        {
            Assert.True(shouldMatch == _testAlgorithm.IsMatch(firstName, lastName, adLogin));
        }
    }
}
