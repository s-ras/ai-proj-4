using System;
using System.Diagnostics;

namespace ai_proj_4 {
	internal class Program {
		public static bool finished = false;
		static void Main(string[] args) {
			Reader.ParseArgs(args);
			Reader.ReadDataSet(out int WorkersCount, out int JobsCount, out List<int> Jobs, out List<List<bool>> Capabilities);
			AlgorithmDefinitions.Init();
			Algorithm a = Input.GetInput();
			Stopwatch sw = new();
			Answer ans = a.Run(WorkersCount, JobsCount, Jobs, Capabilities);
			sw.Stop();
			TimeSpan ts = sw.Elapsed;
			finished = true;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine($"Execution Time: {ts} ");
			ans.Print();
		}
	}
}