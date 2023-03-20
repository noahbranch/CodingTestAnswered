using System;

namespace CodingTest.TestQuestions.Question2
{
    public class MatchingAlgorithm
    {
        /// <summary>
        /// A Customer is looking to match their contact list to their users.  We have a list of contacts and a listing of their
        /// active directory logins for each computer.  Unfortunately, over the years, they have had different models of how they create
        /// their active directory logins, and so our algorithm should be able to determine whether a username matches based on a few different
        /// patterns
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="activeDirectoryLogin"></param>
        /// <returns></returns>
        public bool IsMatch(string FirstName, string LastName, string activeDirectoryLogin)
        {
            var domainlessLogin = activeDirectoryLogin.Substring(activeDirectoryLogin.IndexOf("\\")+1).ToLowerInvariant(); //Removes the domain from the login string, if no domain it trims nothing
            var firstNameLower = FirstName.ToLowerInvariant();
            var lastNameLower = LastName.ToLowerInvariant();

            var isLoginMatch =
                (domainlessLogin.Contains(firstNameLower) && domainlessLogin.Contains(lastNameLower)) ||
                (domainlessLogin.Contains(firstNameLower.Substring(0, 1)) && domainlessLogin.Contains(lastNameLower));

            return isLoginMatch;
        }
    }
}
