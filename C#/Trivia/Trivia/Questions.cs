using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Questions
    {
        public string Category { get; }
        public LinkedList<string> Items { get; }

        public Questions(string category)
        {
            this.Category = category;
            this.Items = new LinkedList<string>();

            for (var i = 0; i < 50; i++)
                Items.AddLast($"{category} Question " + i);
        }

        public void AskQuestion()
        {
            Console.WriteLine(this.Items.First());
            this.Items.RemoveFirst();
        }
    }
}