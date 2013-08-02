namespace Logs.Models
{
    using System;

    public class Log
    {
        
        public int Id { get; set; }
        public DateTime LogDate { get; set; }
        public string QueryXml { get; set; }
    }
}
