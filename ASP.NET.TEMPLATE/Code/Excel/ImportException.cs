using System;

namespace ASP.NET.TEMPLATE
{
    public class ImportException : Exception
    {
        public ImportException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
