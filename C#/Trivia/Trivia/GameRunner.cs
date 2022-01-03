using System;

namespace Trivia
{
    public static class GameRunner
    {
        private static bool notAWinner;

        public static void Main(string[] args)
        {
            var aGame = new Game();
            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");
            var rand = new Random();
            do
            {
                aGame.Roll(rand.Next(5) + 1);
                notAWinner = rand.Next(9) == 7 ? aGame.WrongAnswer() : aGame.WasCorrectlyAnswered();
            } while (notAWinner);
        }
    }
}