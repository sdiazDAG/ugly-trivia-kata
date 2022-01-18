using System;

namespace Trivia
{
    public static class GameRunnerNotRamdom
    {
        private static bool notAWinner;

        public static void Execute(int seed)
        {
            var aGame = new Game();
            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");
            var rand = new Random(seed);
            do
            {
                aGame.Roll(rand.Next(5) + 1);
                notAWinner = rand.Next(9) == 7 ? aGame.WrongAnswer() : aGame.CorrectAnswer();
            } while (notAWinner);
        }
    }
}