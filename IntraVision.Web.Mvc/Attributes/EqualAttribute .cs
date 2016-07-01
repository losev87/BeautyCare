using System;
using System.ComponentModel.DataAnnotations;

namespace IntraVision.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EqualAttribute : ValidationAttribute
    {
        private readonly object _valueToCompare;

        public EqualAttribute(object valueToCompare)
            : base("{0} должен быть равен {1}.")
        {
            _valueToCompare = valueToCompare;
        }

        #region ValidationAttribute overrides

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _valueToCompare);
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

            if (value.Equals(_valueToCompare))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        #endregion
    }
}
