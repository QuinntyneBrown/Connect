using Connect.Core.Models;

namespace Connect.API.Features.Reports
{
    public class ReportApiModel
    {        
        public int ReportId { get; set; }
        public string Name { get; set; }

        public static ReportApiModel FromReport(Report report)
        {
            var model = new ReportApiModel();
            model.ReportId = report.ReportId;
            
            return model;
        }
    }
}
