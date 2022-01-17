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
        private int currentPlayerIndex;
        private bool isGettingOutOfPenaltyBox;

        private Player CurrentPlayer() => players[currentPlayerIndex];

        private bool DidPlayerWin() => CurrentPlayer().Purses != 6;

        private void MoveCurrentPlayer(int roll)
        {
            CurrentPlayer().Places += roll;
            if (CurrentPlayer().Places > 11) CurrentPlayer().Places -= 12;

            Console.WriteLine(CurrentPlayer().Name
                              + "'s new location is "
                              + CurrentPlayer().Places);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        public void Add(string playerName)
        {
            players.Add(new Player(playerName));
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
        }

        public void Roll(int roll)
        {
            Console.WriteLine(CurrentPlayer().Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            if (CurrentPlayer().InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(CurrentPlayer().Name + " is getting out of the penalty box");
                    MoveCurrentPlayer(roll);
                }
                else
                {
                    Console.WriteLine(CurrentPlayer().Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                MoveCurrentPlayer(roll);
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
                popQuestions.AskQuestion();
            if (CurrentCategory() == "Science")
                scienceQuestions.AskQuestion();
            if (CurrentCategory() == "Sports")
                sportsQuestions.AskQuestion();
            if (CurrentCategory() == "Rock")
                rockQuestions.AskQuestion();
        }

        private string CurrentCategory()
        {
            switch (CurrentPlayer().Places)
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
            if (CurrentPlayer().InPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    CurrentPlayer().Purses++;
                    Console.WriteLine(CurrentPlayer().Name
                                      + " now has "
                                      + CurrentPlayer().Purses
                                      + " Gold Coins.");
                    winner = DidPlayerWin();
                    currentPlayerIndex++;
                    if (currentPlayerIndex == players.Count) currentPlayerIndex = 0;
                    return winner;
                }
                currentPlayerIndex++;
                if (currentPlayerIndex == players.Count) currentPlayerIndex = 0;
                return true;
            }

            Console.WriteLine("Answer was correct!!!!");
            CurrentPlayer().Purses++;
            Console.WriteLine(CurrentPlayer().Name
                              + " now has "
                              + CurrentPlayer().Purses
                              + " Gold Coins.");

            winner = DidPlayerWin();
            currentPlayerIndex++;
            if (currentPlayerIndex == players.Count) currentPlayerIndex = 0;

            return winner;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(CurrentPlayer().Name + " was sent to the penalty box");
            CurrentPlayer().InPenaltyBox = true;
            currentPlayerIndex++;
            if (currentPlayerIndex == players.Count) currentPlayerIndex = 0;
            return true;
        }

        
    }
}