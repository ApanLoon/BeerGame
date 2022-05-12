namespace BeerGame
{
    public class Game
    {
        private enum GameState
        {
            Menu,
            Started
        }

        private GameState _state;
        private Player[]? _players;

        public void Init()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            DrawMenu();

            _state = GameState.Menu;
        }

        private static void DrawMenu()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.CursorTop = 4;
            ConsoleHelpers.CenterText("Beer Game");
            Console.CursorTop = 5;
            ConsoleHelpers.CenterText("=========");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.CursorTop = 7;
            ConsoleHelpers.CenterText("Player 1", 0, Console.WindowWidth / 2);
            ConsoleHelpers.CenterText("Player 2", Console.WindowWidth / 2, Console.WindowWidth / 2);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorTop = 9;
            ConsoleHelpers.CenterText("a and s to drink", 0, Console.WindowWidth / 2);
            ConsoleHelpers.CenterText("k and l to drink", Console.WindowWidth / 2, Console.WindowWidth / 2);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorTop = 14;
            ConsoleHelpers.CenterText("space to start");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.CursorTop = 16;
            ConsoleHelpers.CenterText("q to quit");

        }

        private void Start()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            _players = new[]
            {
                new Player(key0: 'a', key1: 's', isLeft: true),
                new Player(key0: 'k', key1: 'l')
            };

            _state = GameState.Started;
        }

        public void Run()
        {
            var isRunning = true;
            while (isRunning)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case 'q':
                            if (_state == GameState.Started)
                            {
                                Init();
                            }
                            else
                            {
                                isRunning = false;
                            }
                            break;

                        case ' ':
                            if (_state == GameState.Menu)
                            {
                                Start();
                            }
                            break;

                        default:
                            if (_state == GameState.Menu)
                            {
                                break;
                            }

                            if (_players != null)
                                foreach (var player in _players)
                                {
                                    player.Toggle(key.KeyChar);
                                }

                            break;
                    }
                }
            }
        }
    }
}
