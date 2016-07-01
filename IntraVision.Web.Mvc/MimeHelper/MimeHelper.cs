namespace IntraVision.Web.Mvc
{
    public static class MimeHelper
    {
        public static string GetMimeType(string strFileName)
        {
            string retval;
            switch (System.IO.Path.GetExtension(strFileName).ToLower())
            {
                case ".txt":
                    return "text/plain";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".doc":
                    return "application/vnd.ms-word";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".css":
                    return "text/css";
                case ".js":
                    return "text/javascript";
                case ".png":
                    return "image/png";
                case ".jpg":
                    return "image/jpeg";
                case ".jpeg":
                    return "image/jpeg";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                case ".pdf":
                    return "application/pdf";
                case ".html":
                    return "text/html";
                case ".htm":
                    return "text/html";
                case ".zip":
                    return "multipart/x-zip";
                case ".rar":
                    return "application/x-rar-compressed";
                case ".xml":
                    return "text/xml";

                default: retval = "application/octet-stream"; break;
            }
            return retval;
        }
    }

}
