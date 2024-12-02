using System.Diagnostics;

namespace ai_proj_4 {
    public class Algorithm {
        public string Title { get; }
        public Func<int, int, List<Job>, List<List<bool>>, int, Answer?> Run { get; }

        public Algorithm(string t, Func<int, int, List<Job>, List<List<bool>>, int, Answer?> r) {
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
        public static readonly Algorithm AlgorithmB;
        public static readonly Algorithm AlgorithmC;
        public static readonly Algorithm AlgorithmD;
        public static readonly Algorithm AlgorithmE;
        public static readonly Algorithm AlgorithmF;

        public static void Init() {
            return;
        }

        static AlgorithmDefinitions() {
            AlgorithmA = new("Random", (wc, jc, j, c, t) => {
                Stopwatch sw = Stopwatch.StartNew();

                Answer? best = null;

                while (true) {
                    if ((t > 0 && sw.ElapsedMilliseconds > t) || Program._shouldTerminate) {
                        break;
                    }
                    Answer random = Answer.GenerateRandomAnswer(j, wc, c);

                    if (best == null) {
                        best = random;
                    } else if (random.Difference <= best.Difference) {
                        best = random;
                    }
                }

                return best;
            });

            AlgorithmB = new("First Choice Hill Climbing", (wc, jc, j, c, t) => {
                Stopwatch sw = Stopwatch.StartNew();

                Answer current = Answer.GenerateRandomAnswer(j, wc, c);

                Answer best = current;

                while (true) {
                    if ((t > 0 && sw.ElapsedMilliseconds > t) || Program._shouldTerminate) {
                        break;
                    }

                    Answer newAns = current.Mutate(c);

                    if (newAns.Difference <= current.Difference) {
                        current = newAns;
                    }

                    if (newAns.Difference <= best.Difference) {
                        best = newAns;
                    }

                }

                return best;

            });

            AlgorithmC = new("Low Attention Hill Climbing", (wc, jc, j, c, t) => {
                const int ATTENTION_SPAN = 100;

                Stopwatch sw = Stopwatch.StartNew();

                Answer? best = null;

                while (true) {
                    if ((t > 0 && sw.ElapsedMilliseconds > t) || Program._shouldTerminate) {
                        break;
                    }

                    int attention = ATTENTION_SPAN;
                    Answer current = Answer.GenerateRandomAnswer(j, wc, c);
                    if (best == null) {
                        best = current;
                    } else if (current.Difference <= best.Difference) {
                        best = current;
                    }
                    while (attention > 0) {
                        if ((t > 0 && sw.ElapsedMilliseconds > t) || Program._shouldTerminate) {
                            break;
                        }

                        Answer newAns = current.Mutate(c);

                        if (newAns.Difference <= current.Difference) {
                            current = newAns;
                        }

                        if (newAns.Difference <= best.Difference) {
                            best = newAns;
                        }
                    }

                }

                return best;

            });

            AlgorithmD = new("Stochastic Hill Climbing", (wc, jc, j, c, t) => {
                const int SAMPLE_SIZE = 10;

                Answer GiveBestNeighbor(Answer a) {
                    List<Answer> neighbors = [];

                    for (int i = 0; i < SAMPLE_SIZE; i++) {
                        neighbors.Add(a.Mutate(c));
                    }

                    Answer best = neighbors.MinBy(x => x.Difference);

                    return best;
                }

                Stopwatch sw = Stopwatch.StartNew();

                Answer current = Answer.GenerateRandomAnswer(j, wc, c);

                Answer best = current;

                while (true) {
                    if ((t > 0 && sw.ElapsedMilliseconds > t) || Program._shouldTerminate) {
                        break;
                    }

                    Answer newAns = GiveBestNeighbor(current);

                    if (newAns.Difference <= current.Difference) {
                        current = newAns;
                    }

                    if (newAns.Difference <= best.Difference) {
                        best = newAns;
                    }

                }

                return best;

            });

            AlgorithmE = new("Mutational Hill Climbing", (wc, jc, j, c, t) => {
                const int SAMPLE_SIZE = 10;

                Answer GiveBestDescendant(Answer a) {
                    List<Answer> decendants = [];

                    Answer current = a;

                    for (int i = 0; i < SAMPLE_SIZE; i++) {
                        Answer newDecendant = current.Mutate(c);
                        decendants.Add(newDecendant);
                        current = newDecendant;
                    }

                    Answer best = decendants.MinBy(x => x.Difference);

                    return best;
                }

                Stopwatch sw = Stopwatch.StartNew();

                Answer current = Answer.GenerateRandomAnswer(j, wc, c);

                Answer best = current;

                while (true) {
                    if ((t > 0 && sw.ElapsedMilliseconds > t) || Program._shouldTerminate) {
                        break;
                    }

                    Answer newAns = GiveBestDescendant(current);

                    if (newAns.Difference <= current.Difference) {
                        current = newAns;
                    }

                    if (newAns.Difference <= best.Difference) {
                        best = newAns;
                    }

                }

                return best;

            });

            AlgorithmB = new("Long Step Hill Climbing", (wc, jc, j, c, t) => {
                int MAXIMUM_BAD_STEPS = 10;

                Stopwatch sw = Stopwatch.StartNew();

                Answer current = Answer.GenerateRandomAnswer(j, wc, c);

                Answer soul = current;

                Answer best = current;

                int bad_steps = 0;

                while (true) {
                    if ((t > 0 && sw.ElapsedMilliseconds > t) || Program._shouldTerminate) {
                        break;
                    }

                    soul = current.Mutate(c);

                    if (soul.Difference <= current.Difference) {
                        current = soul;
                        bad_steps = 0;
                        if (current.Difference <= best.Difference) {
                            best = current;
                        }
                    } else {
                        bad_steps++;
                    }

                    if (bad_steps > MAXIMUM_BAD_STEPS) {
                        soul = current;
                    }
                }

                return best;

            });

            AlgorithmF = new("Tolerant Hill Climbing", (wc, jc, j, c, t) => {
                int FAULT_TOLERANCE = 1;

                Stopwatch sw = Stopwatch.StartNew();

                Answer current = Answer.GenerateRandomAnswer(j, wc, c);

                Answer best = current;

                Random rand = new();

                while (true) {
                    if ((t > 0 && sw.ElapsedMilliseconds > t) || Program._shouldTerminate) {
                        break;
                    }

                    Answer newAns = current.Mutate(c);

                    if (newAns.Difference <= current.Difference || rand.Next(10) <= FAULT_TOLERANCE) {
                        current = newAns;
                    }

                    if (newAns.Difference <= best.Difference) {
                        best = newAns;
                    }

                }

                return best;

            });

        }

    }


}