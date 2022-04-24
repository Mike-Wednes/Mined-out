using Core;

namespace UI
{
    public interface IInterface
    {
        public void DisplayField(Field field);

        public void DisplayKeys();

        public int GetFieldSize(string linearSize);

        public void Move(List<LogicCell> vector, Field field);

        public void DisplayCell(LogicCell cell, Field field);

        public void GameOver();

        public void Success();
    }
}
