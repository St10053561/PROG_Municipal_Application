using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    public class ExceptionHandling
    {
        /// <summary>
        /// Providing a Exception Handling for the form fields
        /// </summary>
        /// <param name="location"></param>
        /// <param name="category"></param>
        /// <param name="description"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidateFormFields(string location, string category, string description)
        {
            var fields = new Dictionary<string, string>
            {
                { "Location", location },
                { "Category", category },
                { "Description", description }
            };

            var invalidField = fields.FirstOrDefault(field => string.IsNullOrEmpty(field.Value));
            if (!string.IsNullOrEmpty(invalidField.Key))
            {
                throw new ArgumentException($"The {invalidField.Key} field must not be empty. Please provide a valid {invalidField.Key.ToLower()}.");
            }
        }
    }
}
