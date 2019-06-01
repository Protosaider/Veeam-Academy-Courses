using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ClientApp.Validators
{
/*    public class ValidateLogin : ValidationRule
    {
        public override ValidationResult Validate(Object value, CultureInfo cultureInfo)
        {
            var login = value as String;

            if (String.IsNullOrEmpty(login))
                return new ValidationResult(false, "Please, enter your login");

            var strLength = login.Length;

            if (strLength <= 3)
                return new ValidationResult(false, "Your login should be longer then 3 characters");

            if (strLength > 20)
                return new ValidationResult(false, "Your login should be shorter then 20 characters");

            Regex regex = new Regex(@"^[\w_-]{3,20}$");

            if (!regex.IsMatch(login))
                return new ValidationResult(false, "Your login can consist of letters, digits, underscores or hyphens");

            return new ValidationResult(true, "Success!");
        }
    }
    */

        //TODO English language only!
	internal sealed class CLoginValidator
    {
        public Boolean ValidateSpelling(String login, out ICollection<String> validationErrors)
        {
            validationErrors = new List<String>();

            if (String.IsNullOrEmpty(login))
            {
                validationErrors.Add("Please, enter your login");
                return false;
            }

            if (login.Length <= 3)
                validationErrors.Add("Your login should be longer then 3 characters");

            Regex regex = new Regex(@"^[\w_-]{3,20}$");

            if (!regex.IsMatch(login))
                validationErrors.Add("Your login can consist of letters, digits, underscores or hyphens");

            return validationErrors.Count == 0;
        }
    }
}
