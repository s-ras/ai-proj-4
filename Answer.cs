namespace ai_proj_4 {
    public class Answer(int d, Dictionary<int, List<int>> jpw) {
        int Difference { get; } = d;
        Dictionary<int, List<int>> JobsPerWorker { get; } = jpw;

        public void Print() {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Minimum difference : {Difference}");
            Console.ResetColor();
            foreach (var kvp in JobsPerWorker) {
                Console.Write($"# Worker ID : {kvp.Key} => ");
                foreach (int j in kvp.Value) {
                    Console.Write($"{kvp.Key} ");
                }
                Console.WriteLine("");
            }
        }
    }
}