using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ClientApp.Validators
{
	internal sealed class CMessageValidator
    {     
        public Boolean Validate(String message, out ICollection<String> validationErrors)
        {
            validationErrors = new List<String>();

            if (String.IsNullOrEmpty(message))
            {
                validationErrors.Add("Can't send empty message");
                return false;
            }

            String anyNonWhitespace = @"\S+";
            Regex regex = new Regex(anyNonWhitespace);

            if (!regex.IsMatch(message))
            {
                validationErrors.Add("Can't send message that contains whitespaces only");
                return false;
            }

            return validationErrors.Count == 0;
        }
    }
}