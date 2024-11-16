namespace ai_proj_4 {

    public static class Input {

        public static Algorithm GetInput() {
            int choice = -1;

            do {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Which heuristic function do you want to use?");
                Console.ResetColor();
                AlgorithmList.ListAlgorithms();

                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= AlgorithmList.Count()) {
                    return AlgorithmList.Algorithms[choice - 1];
                }

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice. Please try again.");
                Console.ResetColor();
            } while (true);
        }

    }

}