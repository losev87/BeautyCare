using System;

namespace IntraVision.Web.Mvc
{
    public class DateInput
    {
        public DateInput()
        {
            DateTime = DateTime.Now;
        }

        public DateInput(DateTime date)
        {
            DateTime = date;
        }

        public DateInput(DateTime? date)
        {
            if (date == null)
                DateTime = DateTime.Now;
            else
                DateTime = date.Value;
        }

        public string Date { get; set; }
        public string Time { get; set; }

        public DateTime DateTime { get { return DateTime.Parse(Date + " " + Time); } set { Date = value.ToShortDateString(); Time = value.ToShortTimeString(); } }
    }
}
