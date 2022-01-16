using System;
using System.IO;
using Xunit;

namespace Trivia.Tests
{
    public class GameTest
    {
        [Fact]
        public void Generate_golden_master_files()
        {
            const string folder = @"../../../GoldenMaster";
            Directory.CreateDirectory(folder);

            for (var i = 0; i <= 100; i++)
            {
                var file = $"trivialOutput.{i}.txt";
                using var fileStream = new FileStream(Path.Combine(folder, file), FileMode.Create);
                using var outputStream = new StreamWriter(fileStream);
                Console.SetOut(outputStream);
                GameRunnerNotRamdom.Execute(i);
            }
        }
    }
}