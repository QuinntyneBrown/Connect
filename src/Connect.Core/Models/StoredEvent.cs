using System;

namespace Connect.Core.Common
{
    public class StoredEvent
    {        
        public System.Guid StoredEventId { get; set; }
        public System.Guid StreamId { get; set; }
        public string Type { get; set; }
        public string Aggregate { get; set; }
        public string Data { get; set; }
        public string Subject { get; set; }
        public DateTime EventTime { get; set; }
        public System.Guid Version { get; set; }
        public dynamic EventData { get; set; }
        public string DotNetType { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
