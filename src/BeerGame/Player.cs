namespace BeerGame
{
    public class Player
    {
        private readonly int _xPos;
        private readonly int _yPos;
        private readonly char _key0;
        private readonly char _key1;
        private readonly bool _isLeft;

        private int _score;
        private bool _toggle;

        private const int FullLevel = 11;
        private int _level = FullLevel;

        private const int BeerWidth        = 20;
        private const int BeerXOffsetRight = 2;
        private const int BeerXOffsetLeft  = 10;
        private const int BeerYOffset      = 11;

        private static readonly string[] _glass = new string[]
        {
            "**                    **        ",
            "**                    **        ",
            "**                    ** *****  ",
            "**                    ****   ** ",
            "**                    **      **",
            "**                    **      **",
            "**                    **      **",
            "**                    **      **",
            "**                    **      **",
            "**                    **    *** ",
            "**                    *******   ",
            "**                    ****      ",
            "************************        "
        };

        public Player(char key0, char key1, bool isLeft = false)
        {
            _key0 = key0;
            _key1 = key1;
            _isLeft = isLeft;

            _xPos = _isLeft
                ? ConsoleHelpers.GetCenteredStartX(_glass[0], 0,                       Console.WindowWidth / 2)
                : ConsoleHelpers.GetCenteredStartX(_glass[0], Console.WindowWidth / 2, Console.WindowWidth / 2);
            _yPos = 2;

            DrawGlass();
            Fill();
            DrawScore();
        }

        public void Fill()
        {
            _level = FullLevel;
            DrawBeer();
        }

        public void DrawGlass()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            int y = _yPos;
            foreach (var s in _glass)
            {
                Console.CursorLeft = _xPos;
                Console.CursorTop = y++;
                var line = s;
                if (_isLeft)
                {
                    var chars = s.ToCharArray();
                    Array.Reverse(chars);
                    line = new string(chars);
                }
                Console.Write(line);
            }
        }

        private void DrawBeer()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Gray;
            for (var y = 0; y < FullLevel; y++)
            {
                Console.CursorLeft = _xPos + (_isLeft ? BeerXOffsetLeft : BeerXOffsetRight);
                Console.CursorTop = _yPos + BeerYOffset - y;

                if (y >= _level)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    for (int x = 0; x < BeerWidth; x++)
                    {
                        Console.Write(' ');
                    }
                    continue;
                }

                if (y == _level - 1)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                for (int x = 0; x < BeerWidth; x++)
                {
                    Console.Write(Random.Shared.NextSingle() < 0.5f ? '*' : ' ');
                }
            }
        }

        private void DrawScore()
        {
            string s = _isLeft ? $"{_score} :Score" : $"Score: {_score}";

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.CursorLeft = _xPos + (_isLeft ? _glass[0].Length - s.Length : 0);
            Console.CursorTop = _yPos + _glass.Length + 1;

            Console.Write(s);
        }

        public bool Toggle(char key)
        {
            if (!_toggle && key == _key1)
            {
                _toggle = true;
                return false;
            }

            if (_toggle && key == _key0)
            {
                _toggle = false;
                Drink();
                return true;
            }

            return false;
        }

        private void Drink()
        {
            _level--;
            DrawBeer();
            
            if (_level != 0)
            {
                return;
            }

            _score++;
            DrawScore();
            Fill();
        }
    }
}
