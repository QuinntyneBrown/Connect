using Connect.Core.Models;

namespace Connect.API.Features.Cards
{
    public class CardDto
    {        
        public int CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static CardDto FromCard(Card card)
        {
            var model = new CardDto();
            model.CardId = card.CardId;
            model.Name = card.Name;
            model.Description = card.Description;
            return model;
        }
    }
}
