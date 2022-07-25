namespace GraficRedactor
{
    internal abstract class Mode
    {
        protected Redactor redactor;

        public Mode(Redactor redactor)
        {
            this.redactor = redactor;
        }

        public abstract RedactMode? Handle();

        protected void MoveInGrafic(ConsoleKey key)
        {
            if (KeyStorage.ArrowToDirection.ContainsKey(key))
            {
                redactor.Move(key, true);
            }
        }

        protected void MoveOnSide(ConsoleKey key)
        {
            if (KeyStorage.ArrowToDirection.ContainsKey(key))
            {
                redactor.Move(key);
            }
        }

        protected void EnterColor(ConsoleKey key, Palette palette, string propName)
        {
            if (key == ConsoleKey.Enter)
            {
                if (IsCursorAtPalette())
                {
                    redactor.ChangeProperty(propName, palette.GetColor(redactor.cursor));
                }
            }
        }

        private bool IsCursorAtPalette()
        {
            if (redactor.cursor.X >= redactor.paletteColor.OffSet.X && redactor.cursor.X < redactor.paletteColor.OffSet.X + redactor.paletteColor.Cols)
            {
                if (redactor.cursor.Y >= redactor.paletteColor.OffSet.Y && redactor.cursor.Y < redactor.paletteColor.OffSet.Y + redactor.paletteColor.Rows)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
