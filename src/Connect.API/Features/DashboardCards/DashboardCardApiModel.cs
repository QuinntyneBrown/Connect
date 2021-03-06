using Connect.Core.Models;
using Newtonsoft.Json;

namespace Connect.API.Features.DashboardCards
{
    public class DashboardCardDto
    {        
        public System.Guid DashboardCardId { get; set; }
        public System.Guid DashboardId { get; set; }
        public int? CardId { get; set; }
        public int? CardLayoutId { get; set; }
        public OptionsDto Options { get; set; }

        public static DashboardCardDto FromDashboardCard(DashboardCard dashboardCard)
            => new DashboardCardDto
            {
                DashboardCardId = dashboardCard.DashboardCardId,
                DashboardId = dashboardCard.DashboardId,
                CardId = dashboardCard.CardId,
                CardLayoutId = dashboardCard.CardLayoutId,
                Options = JsonConvert.DeserializeObject<OptionsDto>(dashboardCard.Options)
            };

        public class OptionsDto {
            public int Top { get; set; } = 1;
            public int Left { get; set; } = 1;
            public int Height { get; set; } = 1;
            public int Width { get; set; } = 1;
        }
    }
}
