namespace ai_proj_4 {
    public class Answer {
        public int Difference { get; }
        public Dictionary<int, List<Job>> JobsPerWorker { get; }

        public Answer(Dictionary<int, List<Job>> jpw) {
            this.JobsPerWorker = jpw;
            this.Difference = this.CalculateDiff();
        }

        public void Print() {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Minimum difference : {Difference}");
            Console.ResetColor();
            foreach (var kvp in JobsPerWorker) {
                Console.Write($"# Worker ID : {kvp.Key} => ");
                foreach (Job j in kvp.Value) {
                    Console.Write($"{j.Hour} ");
                }
                Console.WriteLine("");
            }
        }

        private int CalculateDiff() {
            int max = int.MinValue;
            int min = int.MaxValue;
            foreach (List<Job> jobs in this.JobsPerWorker.Values) {
                int sum = jobs.Sum(x => x.Hour);
                if (sum > max) {
                    max = sum;
                } else if (sum < min) {
                    min = sum;
                }
            }
            return max - min;
        }

        private static int PickRandomAvailableWorker(List<bool> wpj, int? avoid) {
            Random rand = new();

            List<int> picks = [];

            for (int i = 0; i < wpj.Count; i++) {
                if (wpj[i]) {
                    if (avoid != null && i == avoid) {
                        continue;
                    }
                    picks.Add(i);
                }
            }

            if (picks.Count < 1) {
                if (avoid != null) {
                    return (int)avoid;
                }
                throw new InvalidDataException("No worker found to handle the job!");
            }

            return picks[rand.Next(picks.Count)];
        }

        public Answer Mutate(List<List<bool>> c) {
            while (true) {
                var newJPW = this.JobsPerWorker.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new List<Job>(kvp.Value)
                );

                Random rand = new();

                int from = rand.Next(JobsPerWorker.Count);
                List<Job> fromJobList = newJPW[from];

                if (fromJobList.Count == 0) {
                    continue;
                }

                int movingId = rand.Next(fromJobList.Count);
                Job movingJob = fromJobList[movingId];

                int to = PickRandomAvailableWorker(c[movingJob.Id], from);
                List<Job> toJobList = newJPW[to];

                fromJobList.Remove(movingJob);
                toJobList.Add(movingJob);

                return new(newJPW);

            }
        }

        public static Answer GenerateRandomAnswer(List<Job> j, int wc, List<List<bool>> c) {
            Dictionary<int, List<Job>> jpw = [];

            for (int i = 0; i < wc; i++) {
                jpw.Add(i, []);
            }

            for (int i = 0; i < j.Count; i++) {
                int wid = PickRandomAvailableWorker(c[i], null);
                jpw[wid].Add(j[i]);
            }

            return new(jpw);
        }


    }
}