using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> players = new List<Player>();
        private readonly LinkedList<string> popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> rockQuestions = new LinkedList<string>();
        private readonly LinkedList<string> scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> sportsQuestions = new LinkedList<string>();
        private int currentPlayer;
        private bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast("Science Question " + i);
                sportsQuestions.AddLast("Sports Question " + i);
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        private static string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public void Add(string playerName)
        {
            players.Add(new Player(playerName));
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
        }

        private int HowManyPlayers()
        {
            return players.Count;
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
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() != "Rock") return;
            Console.WriteLine(rockQuestions.First());
            rockQuestions.RemoveFirst();
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