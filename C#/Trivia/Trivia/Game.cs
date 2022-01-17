using System;
using System.Collections.Generic;

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


        private Player CurrentPlayer => players[currentPlayerIndex];
        private string CurrentCategory => CurrentPlayer.CurrentCategory();
        private bool DidPlayerWin => CurrentPlayer.Purses != 6;


        public void Add(string playerName)
        {
            players.Add(new Player(playerName));
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
        }

        public void Roll(int roll)
        {
            if (CurrentPlayer.RollAndMove(roll))
                AskQuestion();
        }


        private void AskQuestion()
        {
            switch (CurrentCategory)
            {
                case "Pop":
                    popQuestions.Ask();
                    break;
                case "Science":
                    scienceQuestions.Ask();
                    break;
                case "Sports":
                    sportsQuestions.Ask();
                    break;
                case "Rock":
                    rockQuestions.Ask();
                    break;
            }
        }

        public bool WasCorrectlyAnswered()
        {
            bool winner;
            if (CurrentPlayer.InPenaltyBox)
            {
                if (CurrentPlayer.IsGettingOutOfPenaltyBox) return CorrectAnswer();

                SetCurrentPlayerIndex();
                return true;
            }

            return CorrectAnswer();
        }

        private void SetCurrentPlayerIndex()
        {
            currentPlayerIndex++;
            if (currentPlayerIndex == players.Count) currentPlayerIndex = 0;
        }

        private bool CorrectAnswer()
        {
            Console.WriteLine("Answer was correct!!!!");
            CurrentPlayer.Purses++;
            Console.WriteLine(CurrentPlayer.Name
                              + " now has "
                              + CurrentPlayer.Purses
                              + " Gold Coins.");
            var winner = DidPlayerWin;
            SetCurrentPlayerIndex();
            return winner;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(CurrentPlayer.Name + " was sent to the penalty box");
            CurrentPlayer.InPenaltyBox = true;
            SetCurrentPlayerIndex();
            return true;
        }
    }
}