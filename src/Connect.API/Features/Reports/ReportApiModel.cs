using Connect.Core.Models;

namespace Connect.API.Features.Reports
{
    public class ReportDto
    {        
        public int ReportId { get; set; }
        public string Name { get; set; }

        public static ReportDto FromReport(Report report)
        {
            var model = new ReportDto();
            model.ReportId = report.ReportId;
            
            return model;
        }
    }
}
