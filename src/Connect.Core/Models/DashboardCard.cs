using Connect.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connect.Core.Models
{
    public class DashboardCard: AggregateRoot
    {
        public int DashboardCardId { get; set; }
        [ForeignKey("Dashboard")]
        public int DashboardId { get; set; }
        [ForeignKey("Card")]
        public int? CardId { get; set; }
        [ForeignKey("CardLayout")]
        public int? CardLayoutId { get; set; }
        public string Options { get; set; }
        public Dashboard Dashboard { get; set; }
        public Card Card { get; set; }
        public CardLayout CardLayout { get; set; }
    }
}
