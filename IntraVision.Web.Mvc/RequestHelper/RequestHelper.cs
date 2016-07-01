using System;
using System.Globalization;
using System.Web;

namespace IntraVision.Web.Mvc
{
    public static class RequestHelper
    {
        public static byte[] GetPostedFileContent(HttpPostedFileBase file, int? maxContentLength)
        {
            if (file == null || file.ContentLength == 0)
                return new byte[0];

            //Проверка на максимальный размер
            if (maxContentLength != null && file.ContentLength > maxContentLength)
                throw new Exception(String.Format(CultureInfo.InvariantCulture, "Вы пытаетесь загрузить слишком большой файл."));

            //Считывание файла в поток байт
            var data = new byte[file.ContentLength];
            try
            {
                file.InputStream.Read(data, 0, file.ContentLength);
            }
            catch (Exception e)
            {
                throw new Exception("Ошибка загрузки файла", e);
            }

            return data;
        }

        public static byte[] GetPostedFileContent(HttpPostedFileBase file)
        {
            return GetPostedFileContent(file, null);
        }
    }
}
