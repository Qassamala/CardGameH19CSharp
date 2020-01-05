using System;
using System.IO;
using System.Linq;

namespace CardGameH19CSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("Welcome!");
                Console.WriteLine("Press [P] to play a game");
                Console.WriteLine("Press [R] to show the rules of the game");
                Console.WriteLine("Press [S] to show statistics");
                Console.WriteLine("Press [E] to Exit");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.P:
                        PlayGame();
                        break;
                    case ConsoleKey.R:
                        DisplayGameRules();
                        break;
                    case ConsoleKey.S:
                        DisplayGameStatistics();
                        break;
                    case ConsoleKey.E:
                        exit = true;
                        break;
                    default:                        
                        break;
                }
            }            
        }

        private static void PlayGame()
        {
            Console.Clear();
            
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            PlayingCardGame game = new PlayingCardGame(name);

            game.ShuffleCardDeck();

            PlayGame(game);
        }

        private static void PlayGame(PlayingCardGame game)
        {                     
            game.PlayingCardDeck.GetTopCard();
            game.PlayingCardDeck.GetTopCard();

            PlayingCard hiddenCard = game.PlayingCardDeck.DealtCards[0];
            PlayingCard shownCard = game.PlayingCardDeck.DealtCards[1];

            bool hiddenCardIsHigher = CheckIfHigher(hiddenCard, shownCard);

            Console.Clear();

            Console.WriteLine($"Hello {game.PlayerName}");
            Console.WriteLine($"You are playing Higher or Lower! GameID: {game.GameId}");
            Console.WriteLine();
            Console.WriteLine($"The shown card is {ToString(shownCard).ToUpper()}");
            Console.WriteLine();
            Console.WriteLine("Is the hidden card higher or lower than the shown card?");            
            Console.WriteLine("Press [H] for higher");
            Console.WriteLine("Press [L] for lower");            

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.H:
                    if (hiddenCardIsHigher)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Congratz! Your hidden card was {ToString(hiddenCard).ToUpper()}.");
                        game.Wins++;
                        Console.WriteLine($"Wins: {game.Wins}, Losses: {game.Losses}");
                        Pause();                        
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Better luck next time. Your hidden card was {ToString(hiddenCard)}.");
                        game.Losses++;
                        Console.WriteLine($"Wins: {game.Wins}, Losses: {game.Losses}");
                        Pause();                        
                    }
                    break;
                case ConsoleKey.L:
                    if (!hiddenCardIsHigher)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Congratz! Your hidden card was {ToString(hiddenCard)}.");
                        game.Wins++;
                        Console.WriteLine($"Wins: {game.Wins}, Losses: {game.Losses}");
                        Pause();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Better luck next time. Your hidden card was {ToString(hiddenCard)}.");
                        game.Losses++;
                        Console.WriteLine($"Wins: {game.Wins}, Losses: {game.Losses}");
                        Pause();
                    }
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }

            AskToPlayAgain(game);
        }

        private static void AskToPlayAgain(PlayingCardGame game)
        {
            Console.Clear();
            Console.WriteLine("Do you want to play another round?");
            Console.WriteLine("Press [P] to Play another round");
            Console.WriteLine("Press [E] to save score and return to the main menu");

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.P:
                    game.PlayingCardDeck.AddCardToBottom(game.PlayingCardDeck.DealtCards[0]);
                    game.PlayingCardDeck.AddCardToBottom(game.PlayingCardDeck.DealtCards[1]);
                    game.PlayingCardDeck.DealtCards.Clear();
                    PlayGame(game);
                    break;
                case ConsoleKey.E:
                    WriteToFile(game);
                    break;
                default:
                    AskToPlayAgain(game);
                    break;
            }
        }

        private static void WriteToFile(PlayingCardGame game)
        {
            string path = @"C:\Users\Abdi G\OneDrive\C#\OneDrive\C#\CardGameH19CSharp\CardGameH19CSharp\Statistics.txt";

            string[] statistics = File.ReadAllLines(path);

            int counter = 0;
            string newString = null;

                foreach (var item in statistics)
                {
                    if (item.Contains(game.PlayerName.ToString()))
                    {
                        string oldString = item;
                        int firstStringPos = item.IndexOf("wins: ");
                        int secondStringPos = item.IndexOf(", Total losses");
                        int thirdStringPos = item.IndexOf("losses");
                        int fourthStringPos = item.IndexOf(".");
                        string winsToParse = item.Substring(firstStringPos + 6, (secondStringPos - (firstStringPos+6)));
                        int previousWins;
                        bool successWins = int.TryParse(winsToParse, out previousWins);
                        string lossesToParse = item.Substring(thirdStringPos + 8, (fourthStringPos - (thirdStringPos + 8)));
                        int previousLosses;
                        bool succesLosses = int.TryParse(lossesToParse, out previousLosses);
                        newString = $"Player name: {game.PlayerName}, Total wins: {game.Wins + previousWins}, Total losses: {game.Losses + previousLosses}.";
                        statistics[counter] = newString;
                        File.WriteAllLines(path, statistics);
                    }
                    counter++;
                }

            if (!statistics.Contains(newString))
            {
                using (StreamWriter file = File.AppendText(path))
                {
                    file.WriteLine($"Player name: {game.PlayerName}, Total wins: {game.Wins}, Total losses: {game.Losses}.");
                }
            }
        }
        
        private static void DisplayGameRules()
        {
            Console.Clear();

            Console.WriteLine("Rules for higher or lower");
            Console.WriteLine();
            Console.WriteLine("If you choose to play you will be prompted for a player name");
            Console.WriteLine("The deck of cards consists of a classic 52 cards");
            Console.WriteLine("For each new game, the deck of cards is shuffled and \n" +
                "the top two cards will be drawn");
            Console.WriteLine();
            Console.WriteLine("One of the cards is given to you and is hidden");
            Console.WriteLine("The other card is shown to you in the console");
            Console.WriteLine("You will then be presented with an option to determine if \n" +
                "your hidden card is either higher or lower than shown card ");
            Console.WriteLine("If you guess correctly, you will be awarded a score of 1");
            Console.WriteLine("After the round is finished, you will be asked to choose if \n" +
                "you want to play another round, where you have the chance to accumulate score \n" +
                "or return to the main menu");
            Console.WriteLine("If you return to the main menu your total score will be saved, and can be \n" +
                "viewed in the Statistics section");
            Console.WriteLine("");
            Console.WriteLine("There is no limit on how many times you can play");
            Console.WriteLine();

            Pause();
        }

        private static void DisplayGameStatistics()
        {
            Console.Clear();

            string path = @"C:\Users\Abdi G\OneDrive\C#\OneDrive\C#\CardGameH19CSharp\CardGameH19CSharp\Statistics.txt";

            string[] statistics = File.ReadAllLines(path);

            foreach (var item in statistics)
            {
                Console.WriteLine(item);
            }

            Pause();
        }

        private static bool CheckIfHigher(PlayingCard hiddenCard, PlayingCard shownCard)
        {
            bool isHigher = false;

            if ((int)hiddenCard.Valör == (int)shownCard.Valör)
            {
                if ((int)hiddenCard.Färg > (int)shownCard.Färg)
                {
                    isHigher = true;
                }
            }
            else if ((int)hiddenCard.Valör > (int)shownCard.Valör)
            {
                isHigher = true;
            }
            return isHigher;
        }

        static string ToString(PlayingCard card)
        {
            return $"{card.Färg} {card.Valör}";
        }

        private static void Pause()
        {
            Console.Write("Press any key to continue");
            Console.ReadKey(true);
            Console.WriteLine();
        }
    }
}
