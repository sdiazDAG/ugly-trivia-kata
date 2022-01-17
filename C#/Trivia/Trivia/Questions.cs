using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Questions
    {
        public Questions(string category)
        {
            Category = category;
            Items = new LinkedList<string>();

            for (var i = 0; i < 50; i++)
                Items.AddLast($"{category} Question " + i);
        }

        public string Category { get; }
        public LinkedList<string> Items { get; }

        public void Ask()
        {
            Console.WriteLine(Items.First());
            Items.RemoveFirst();
        }
    }
}