using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameH19CSharp
{
    class PlayingCardGame
    {
        static int gameIdCounter = 0;
        public int GameId { get; set; }
        public PlayingCardDeck PlayingCardDeck { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public string PlayerName { get; set; }

        public PlayingCardGame(string playerName)
        {
            GameId = ++gameIdCounter;
            PlayingCardDeck = new PlayingCardDeck();
            PlayerName = playerName;
        }

        public void ShuffleCardDeck()
        {                       
            Random random = new Random();
            var shuffledCards = PlayingCardDeck.DeckOfCards
                .OrderBy(c => random.Next(0, 53))
                .ToList();
            PlayingCardDeck.DeckOfCards = shuffledCards;
                            
        }
    }
}
