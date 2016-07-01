using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IntraVision.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EqualValuesAttribute : ValidationAttribute
    {
        private readonly object[] _valueToCompare;

        public EqualValuesAttribute(object[] valueToCompare)
            : base("{0} допустимые значения: {1}.")
        {
            _valueToCompare = valueToCompare;
        }

        #region ValidationAttribute overrides

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, string.Join(", ", _valueToCompare));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_valueToCompare == null)
            {
                throw new NullReferenceException();
            }

            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (_valueToCompare.Any(value.Equals))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        #endregion
    }
}
