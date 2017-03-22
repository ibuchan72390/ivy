using IBFramework.Core.Utility;
using System.Text.RegularExpressions;

namespace IBFramework.Utility
{
    public class ValidationHelper : IValidationHelper
    {
        public bool ValidateEmail(string email)
        {
            var match = Regex.Match(email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z");
            return match.Success;
        }
    }
}
