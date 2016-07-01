using System;
using System.IO;
using System.Web.Mvc;
using System.Web;

namespace IntraVision.Web.Mvc
{
    public class SQLFileStreamResult : FileResult
    {
        private readonly Action<int, string, Stream> _action;
        private readonly int _id;
        private readonly string _dataTable;

        public SQLFileStreamResult(int id, string dataTable, Action<int, string, Stream> action, string contentType)
            :base(contentType)
        {
            _id = id;
            _action = action;
            _dataTable = dataTable;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            _action(_id, _dataTable, response.OutputStream);
        }
    }
}