using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> players = new List<Player>();
        private readonly Questions popQuestions = new Questions("Pop");
        private readonly Questions rockQuestions = new Questions("Rock");
        private readonly Questions scienceQuestions = new Questions("Science");
        private readonly Questions sportsQuestions = new Questions("Sports");
        private int currentPlayer;
        private bool isGettingOutOfPenaltyBox;

        public void Add(string playerName)
        {
            players.Add(new Player(playerName));
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
        }

        public void Roll(int roll)
        {
            Console.WriteLine(players[currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            if (players[currentPlayer].InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[currentPlayer].Name + " is getting out of the penalty box");
                    CurrentPlayer().Places += roll;
                    if (players[currentPlayer].Places > 11) players[currentPlayer].Places -= 12;

                    Console.WriteLine(players[currentPlayer].Name
                                      + "'s new location is "
                                      + players[currentPlayer].Places);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer].Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                players[currentPlayer].Places += roll;
                if (players[currentPlayer].Places > 11) players[currentPlayer].Places -= 12;
                Console.WriteLine(players[currentPlayer].Name
                                  + "'s new location is "
                                  + players[currentPlayer].Places);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private Player CurrentPlayer()
        {
            return players[currentPlayer];
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.Items.First());
                popQuestions.Items.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.Items.First());
                scienceQuestions.Items.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.Items.First());
                sportsQuestions.Items.RemoveFirst();
            }
            if (CurrentCategory() != "Rock") return;
            Console.WriteLine(rockQuestions.Items.First());
            rockQuestions.Items.RemoveFirst();
        }

        private string CurrentCategory()
        {
            switch (players[currentPlayer].Places)
            {
                case 0:
                case 4:
                case 8:
                    return "Pop";
                case 1:
                case 5:
                case 9:
                    return "Science";
                case 2:
                case 6:
                case 10:
                    return "Sports";
                default:
                    return "Rock";
            }
        }

        public bool WasCorrectlyAnswered()
        {
            bool winner;
            if (players[currentPlayer].InPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    players[currentPlayer].Purses++;
                    Console.WriteLine(players[currentPlayer].Name
                                      + " now has "
                                      + players[currentPlayer].Purses
                                      + " Gold Coins.");
                    winner = DidPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return winner;
                }
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;
                return true;
            }

            Console.WriteLine("Answer was correct!!!!");
            players[currentPlayer].Purses++;
            Console.WriteLine(players[currentPlayer].Name
                              + " now has "
                              + players[currentPlayer].Purses
                              + " Gold Coins.");

            winner = DidPlayerWin();
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;

            return winner;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer].Name + " was sent to the penalty box");
            players[currentPlayer].InPenaltyBox = true;
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }

        private bool DidPlayerWin()
        {
            return players[currentPlayer].Purses != 6;
        }
    }
}