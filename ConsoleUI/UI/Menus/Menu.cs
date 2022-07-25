using Core;

namespace ConsoleUI
{
    internal abstract class Menu
    {
        public abstract ModeType ThisMode { get; }

        public Menu()
        {

        }

        public ModeType? Handle()
        {
            StartDisplaying();
            return KeyListened();
        }

        protected ModeType? KeyListened()
        {
            ModeType? output;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                output = KeyRespond(key.Key);
            }
            while (output == ThisMode);
            return output;
        }

        protected abstract void StartDisplaying();

        protected abstract ModeType? KeyRespond(ConsoleKey key);
    }
}
