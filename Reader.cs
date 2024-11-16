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

        public static void ReadDataSet(out int WorkersCount, out int JobsCount, out List<int> Jobs, out List<List<bool>> Capabilities) {

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

            foreach (string j in JobString) {
                if (int.TryParse(j, out int value)) {
                    Jobs.Add(value);
                }
            }

            Capabilities = [];

            for (int i = 2; i < lines.Count; i++) {
                List<string> CapabilitiesString = [.. lines[i].Split(" ")];
                List<bool> JobCapability = [];
                foreach (string c in CapabilitiesString) {
                    if (bool.TryParse(c, out bool value)) {
                        JobCapability.Add(value);
                    }
                }
                Capabilities.Add(JobCapability);
            }

        }
    }

}