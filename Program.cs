using System;
using System.Diagnostics;

namespace ai_proj_4 {
	internal class Program {
		public static bool _shouldTerminate = false;
		static void Main(string[] args) {
			Reader.ParseArgs(args);
			Reader.ReadDataSet(out int WorkersCount, out int JobsCount, out List<Job> Jobs, out List<List<bool>> Compatibilities);
			AlgorithmDefinitions.Init();
			Input.GetInput(out Algorithm a, out int t);
			Console.CancelKeyPress += (sender, e) => {
				_shouldTerminate = true;
				e.Cancel = true;
			};
			Answer? ans = a.Run(WorkersCount, JobsCount, Jobs, Compatibilities, t);
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			ans?.Print();
			Console.ResetColor();
		}
	}
}