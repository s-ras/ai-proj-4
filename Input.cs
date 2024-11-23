namespace ai_proj_4 {

    public static class Input {

        public static void GetInput(out Algorithm algorithm, out int time) {
            int choice = -1;

            do {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Which algorithm do you want to use?");
                Console.ResetColor();
                AlgorithmList.ListAlgorithms();

                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= AlgorithmList.Count()) {
                    algorithm = AlgorithmList.Algorithms[choice - 1];
                    break;
                }

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice. Please try again.");
                Console.ResetColor();
            } while (true);

            int s = 0;

            do {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("How many seconds do you want to run the algorithm? (0 for infinite)");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out s) && s >= 0) {
                    time = s * 1000;
                    break;
                }

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid time. Please try again");
                Console.ResetColor();
            } while (true);
        }

    }

}