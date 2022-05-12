namespace BeerGame
{
    public static class ConsoleHelpers
    {
        public static int GetCenteredStartX(string s, int offset = 0, int width = 0)
        {
            if (width == 0)
            {
                width = Console.WindowWidth;
            }

            return offset + (width / 2) - (s.Length / 2);
        }

        public static void CenterText(string s, int offset = 0, int width = 0)
        {
            Console.CursorLeft = GetCenteredStartX(s, offset, width);
            Console.Write(s);
        }
    }
}
