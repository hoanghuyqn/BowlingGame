// See https://aka.ms/new-console-template for more information
using bowlingGame.Const;
using bowlingGame.Model;

Console.WriteLine("Hello, Bowling Game!");


Match newMatch = new Match(GameConstants.Frame);
newMatch.Play();

foreach (var frame in newMatch.frames)
{
    Console.WriteLine("{0},{1}|{2}", frame.tries[0].tryScore, frame.tries[1].tryScore, frame.frameScore);
}

Console.WriteLine("Total score: {0}", newMatch.matchScore);


Console.ReadLine();