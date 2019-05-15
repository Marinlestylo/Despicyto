using System;

namespace deSPICYtoINVADER.Utils
{
    public static class Sprites
    {
        public static readonly string[] playerDesign = new string[]
        {
            "▄",
            "/ \\",
            "| o |",
            "|   |",//UN GRAND MERCI A KALINVA POUR l'AVIS CRITIQUE APPORTÉ LORS DU DESIGN DU VAISSEAU
            "| o |",
            "|   |",
            "/| o |\\",
            "/ |___| \\",
            "| / |@| \\ |",
            "|/  |@|  \\|"
        };
        /*{
            "     ▄     ",
            "    / \    ",
            "   | o |   ",
            "   |   |   ",
            "   | o |   ",
            "   |   |   ",
            "  /| o |\   ",
            " / |___| \  ",
            "| / |@| \ |",
            "|/  |@|  \|"*/

        public static readonly string[] enemyDesign = new string[]
        {
            "▄     ▄",
            "█   █",
            "███████",
            "██ ███ ██",
            "███████████",
            "█ ███████ █",
            "█ █     █ █",
            "██ ██"
        }; //Design du monstre
           /*{
               "  ▄     ▄  ",
               "   █   █   ",
               "  ███████  ",
               " ██ ███ ██ ",
               "███████████",
               "█ ███████ █",
               "█ █     █ █",
               "   ██ ██   " }; //Design du monstre*/

        /// <summary>
        /// Permet d'écrire le titre voulu
        /// </summary>
        /// <param name="title">tableau de string écrivant un mot en ascii</param>
        /// <param name="topLeft">Point en haut à gauche du titre</param>
        public static void DrawTitle(string[] title, Point topLeft)
        {
            for (int i = 0; i < title.Length; i++)
            {
                Console.SetCursorPosition(topLeft.X, topLeft.Y + i);
                Console.WriteLine(title[i]);
            }
        }

        public static readonly string[] mainTitle = new string[]
        {
            "   _____       _            _ _   _                _           ",
            "  / ____|     (_)          ( ) \\ | |              | |          ",
            " | (___  _ __  _  ___ _   _|/|  \\| |_   ____ _  __| | ___ _ __ ",
            "  \\___ \\| '_ \\| |/ __| | | | | . ` \\ \\ / / _` |/ _` |/ _ \\ '__|",
            "  ____) | |_) | | (__| |_| | | |\\  |\\ V / (_| | (_| |  __/ |   ",
            " |_____/| .__/|_|\\___|\\__, | |_| \\_| \\_/ \\__,_|\\__,_|\\___|_|   ",
            "        | |            __/ |                                   ",
            "        |_|           |___/                                    "
        };

        public static readonly string[] optionsTitle = new string[]
        {
            "   ___        _   _ ",
            "  / _ \\ _ __ | |_(_) ___  _ __  ___",
            " | | | | '_ \\| __| |/ _ \\| '_ \\/ __|",
            " | |_| | |_) | |_| | (_) | | | \\__ \\",
            "  \\___/| .__/ \\__|_|\\___/|_| |_|___/",
            "       |_|"
        };

        public static readonly string[] highScoreTitle = new string[]
        {
            "  __  __      _ _ _  ",
            " |  \\/  | ___(_) | | ___ _   _ _ __ ___   ___  ___ ___  _ __ ___  ___ ",
            " | |\\/| |/ _ \\ | | |/ _ \\ | | | '__/ __| / __|/ __/ _ \\| '__/ _ \\/ __|",
            " | |  | |  __/ | | |  __/ |_| | |  \\__ \\ \\__ \\ (_| (_) | | |  __/\\__ \\",
            " |_|  |_|\\___|_|_|_|\\___|\\__,_|_|  |___/ |___/\\___\\___/|_|  \\___||___/"
        };

        public static readonly string[] aboutTitle = new string[]
        {
            "     ___    _ __  _ __ ___  _ __   ___  ___",
            "    /   |  | '_ \\| '__/ _ \\| '_ \\ / _ \\/ __|",
            "   / /| |  | |_) | | | (_) | |_) | (_) \\__ \\",
            "  / ___ |  | .__/|_|  \\___/| .__/ \\___/|___/",
            " /_/  |_|  |_|             |_|"
        };

        public static readonly string[] gameOver = new string[]
        {
            "  ______       ___       ___  ___   _______      ______   ____    ____  _______  ______  ",
            " /  ____|     /   \\     |   \\/   | |   ____|    /  __  \\  \\   \\  /   / |   ____||   _  \\",
            "|  |  __     /  ^  \\    |  \\  /  | |  |__      |  |  |  |  \\   \\/   /  |  |__   |  |_)  |",
            "|  | |_ |   /  /_\\  \\   |  |\\/|  | |   __|     |  |  |  |   \\      /   |   __|  |      /",
            "|  |__| |  /  _____  \\  |  |  |  | |  |____    |  `--'  |    \\    /    |  |____ |  |\\  \\_",
            " \\______| /__/     \\__\\ |__|  |__| |_______|    \\______/      \\__/     |_______|| _| `.__|"
        };

        public static readonly string[] joAscii = new string[]
        {
            "    ___                   _   _ ",
            "   |_  |                 | | | |",
            "     | | ___  _ __   __ _| |_| |__   __ _ _ __",
            "     | |/ _ \\| '_ \\ / _` | __| '_ \\ / _` | '_ \\",
            " /\\__/ / (_) | | | | (_| | |_| | | | (_| | | | |",
            " \\____/ \\___/|_| |_|\\__,_|\\__|_| |_|\\__,_|_| |_|"
        };

        public static readonly string[] filipeAscii = new string[]
        {
            "______ _ _ _ ",
            "|  ___(_| |_)",
            "| |_   _| |_ _ __   ___",
            "|  _| | | | | '_ \\ / _ \\",
            "| |   | | | | |_) |  __/",
            "|_|   |_|_|_| .__/ \\___|",
            "            | |",
            "            |_|"
        };

        public static readonly string[] andAscii = new string[]
        {
            "      _",
            "     | |",
            "  ___| |_ ",
            " / _ \\ __|",
            "|  __/ |_",
            " \\___|\\__|"
        };
    }
}
