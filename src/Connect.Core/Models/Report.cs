using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class Report: AggregateRoot                                                 
    {
        public int ReportId { get; set; }        
        public int ProfileId { get; set; }
		public string Body { get; set; }
        public int ReportedByProfileId { get; set; }
    }
}
