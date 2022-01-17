using System;

namespace Trivia
{
    public class Player
    {
        private int Places;

        public Player(string name)
        {
            Name = name;
            Places = 0;
            InPenaltyBox = false;
            Purses = 0;
            IsGettingOutOfPenaltyBox = false;
        }

        public string Name { get; }
        public bool InPenaltyBox { get; set; }
        public int Purses { get; set; }
        public bool IsGettingOutOfPenaltyBox { get; private set; }

        public string CurrentCategory()
        {
            switch (Places)
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

        public bool RollAndMove(int roll)
        {
            Console.WriteLine(Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            if (InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    IsGettingOutOfPenaltyBox = true;

                    Console.WriteLine(Name + " is getting out of the penalty box");
                    Move(roll);
                }
                else
                {
                    Console.WriteLine(Name + " is not getting out of the penalty box");
                    IsGettingOutOfPenaltyBox = false;
                    return false;
                }
            }
            else
            {
                Move(roll);
            }

            return true;
        }

        private void Move(int roll)
        {
            Places += roll;
            if (Places > 11) Places -= 12;

            Console.WriteLine(Name
                              + "'s new location is "
                              + Places);
            Console.WriteLine("The category is " + CurrentCategory());
        }
    }
}