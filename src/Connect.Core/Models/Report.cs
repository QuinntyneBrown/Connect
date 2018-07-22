using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class Report: AggregateRoot                                                 
    {
        public System.Guid ReportId { get; set; }        
        public System.Guid ProfileId { get; set; }
		public string Body { get; set; }
        public System.Guid ReportedByProfileId { get; set; }
    }
}
