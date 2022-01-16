using System;
using System.IO;
using System.Text;
using Xunit;

namespace Trivia.Tests
{
    public class GameTest
    {
        [Fact(Skip = "true")]
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

        [Fact]
        public void Compare_that_refactoring_output_is_equal_to_golden_master_output()
        {
            for (var i = 0; i <= 100; i++)
            {
                var file = $"trivialOutput.{i}.txt";

                var currentOutput = new StringBuilder();
                Console.SetOut(new StringWriter(currentOutput));

                GameRunnerNotRamdom.Execute(i);
                Assert.Equal(currentOutput.ToString(), File.ReadAllText($@"../../../GoldenMaster/{file}"));
            }
        }
    }
}