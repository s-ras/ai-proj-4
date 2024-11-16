namespace ai_proj_4 {
    public class Algorithm {
        public string Title { get; }
        public Func<int, int, List<int>, List<List<bool>>, Answer> Run { get; }

        public Algorithm(string t, Func<int, int, List<int>, List<List<bool>>, Answer> r) {
            this.Title = t;
            this.Run = r;
            AlgorithmList.AddAlgorithm(this);
        }
    }

    public static class AlgorithmList {

        public static List<Algorithm> Algorithms { get; } = [];

        public static void AddAlgorithm(Algorithm a) {
            Algorithms.Add(a);
        }

        public static int Count() {
            return Algorithms.Count;
        }

        public static void ListAlgorithms() {
            for (int i = 0; i < Algorithms.Count; i++) {
                Console.WriteLine($"{i + 1}: {Algorithms[i].Title}");
            }
        }

    }

    public static class AlgorithmDefinitions {

        public static readonly Algorithm AlgorithmA;

        public static void Init() {
            return;
        }

        static AlgorithmDefinitions() {
            AlgorithmA = new("Test", (wc, jc, j, c) => {
                return new Answer(0, []);
            });
        }

    }


}