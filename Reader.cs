namespace ai_proj_4 {

    public static class Reader {
        public const string filePath = "dataset.txt";

        private static string fp = filePath;

        public static void ParseArgs(string[] args) {
            if (args.Length == 0) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine($"No arguments given. Defaulting to file \"{filePath}\" ");
                Console.ResetColor();
                return;
            } else if (args.Length > 1) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Too many arguments!");
                Console.ResetColor();
                Environment.Exit(1);
            } else {
                fp = args[0];
            }
        }

        public static void ReadDataSet(out int WorkersCount, out int JobsCount, out List<Job> Jobs, out List<List<bool>> Compatibilities) {

            if (!File.Exists(fp)) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Dataset file does not exist!");
                Console.ResetColor();
                Environment.Exit(1);
            }

            List<string> lines = [.. File.ReadAllLines(fp)];

            string InfoLine = lines[0];
            string JobsLine = lines[1];

            List<string> InfoString = [.. InfoLine.Split(" ")];

            _ = int.TryParse(InfoString[0], out JobsCount);
            _ = int.TryParse(InfoString[1], out WorkersCount);

            List<string> JobString = [.. JobsLine.Split(" ")];

            Jobs = [];

            for (int i = 0; i < JobString.Count; i++) {
                if (int.TryParse(JobString[i], out int value)) {
                    Jobs.Add(new(i, value));
                }
            }

            Compatibilities = [];

            for (int i = 2; i < lines.Count; i++) {
                List<string> CapabilitiesString = [.. lines[i].Split(" ")];
                List<bool> JobCapability = [];
                foreach (string c in CapabilitiesString) {
                    if (c == "0") {
                        JobCapability.Add(false);
                    } else if (c == "1") {
                        JobCapability.Add(true);
                    }
                }
                Compatibilities.Add(JobCapability);
            }

        }
    }

}