namespace IntraVision.Web.Mvc.Security
{
    public class LogOnResult
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }

        public LogOnResult()
        {
            Successful = true;
        }

        public LogOnResult(string errorMessage)
        {
            Successful = false;
            ErrorMessage = errorMessage;
        }
    }
}
