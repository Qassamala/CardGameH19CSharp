using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameH19CSharp
{
    class PlayingCardDeck
    {        
        public List<PlayingCard> DeckOfCards { get; set; }        
        public List<PlayingCard> DealtCards { get; set; }

        public PlayingCardDeck()
        {
            DeckOfCards = new List<PlayingCard>();
            DealtCards = new List<PlayingCard>();

            CreateDeckOfCards();
        }

        private void CreateDeckOfCards()
        {
            for (int i = 0; i <=3; i++)
            {
                for (int v = 0; v <= 12; v++)
                {
                    DeckOfCards.Add(new PlayingCard((Färg)i, (Valör)v));
                }
            }
        }

        public void GetTopCard()
        {
            DealtCards.Add(DeckOfCards[0]);
            DeckOfCards.RemoveAt(0);         
        }

        public void AddCardToBottom(PlayingCard card)
        {
            DeckOfCards.Add(card);
        }
    }
}
