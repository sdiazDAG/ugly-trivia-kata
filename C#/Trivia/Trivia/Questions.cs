using System.Collections.Generic;

namespace Trivia
{
    public class Questions
    {
        public string Type { get; }
        public LinkedList<string> Items { get; }

        public Questions(string type)
        {
            this.Type = type;
            this.Items = new LinkedList<string>();

            for (var i = 0; i < 50; i++)
                Items.AddLast($"{type} Question " + i);
        }
    }
}