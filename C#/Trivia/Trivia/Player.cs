namespace Trivia
{
    public class Player
    {
        public string Name { get; }
        public int Places { get; set; }
        public bool InPenaltyBox { get; set; }
        public int Purses { get; set; }

        public Player(string name)
        {
            this.Name = name;
            this.Places = 0;
            this.InPenaltyBox = false;
            this.Purses = 0;
        }
    }
}