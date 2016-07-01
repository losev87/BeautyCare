using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace IntraVision.Web.Mvc.Attributes
{
    public sealed class HttpPostedFileWrapperTypeAttribute : ValidationAttribute
    {
        private string _ErrorText;

        private string[] TypeString;
        /// <summary>
        /// Атрибут для валидации загружаемого типа данных
        /// </summary>
        /// <param name="ErrorText">Сообщение об ошибке</param>
        /// <param name="Type">перечисляем типы загружаемых данных(например "image/jpeg", "application/x-shockwave-flash")</param>
        public HttpPostedFileWrapperTypeAttribute(string ErrorText, params string[] Type)
        {
            _ErrorText = ErrorText;
            TypeString = Type;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return null;
            var data = (HttpPostedFileWrapper)value;
            if (!TypeString.Contains(data.ContentType))
            {
                return new ValidationResult(_ErrorText);
            }
            return null;
        }

    }

}