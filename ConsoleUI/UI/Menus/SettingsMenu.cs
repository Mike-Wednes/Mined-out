using Core;

namespace ConsoleUI
{ 
    internal class SettingsMenu : ButtonMenu
    {
        private SliderDisplayer sliderDisplayer;

        public override ModeType ThisMode { get { return ModeType.Settings; } }

        public SettingsMenu()
        {
            sliderDisplayer = new SliderDisplayer();
        }

        protected override void StartDisplaying()
        {
            base.StartDisplaying();
            sliderDisplayer.DisplaySliderFields(ModeType.Settings);
        }

        protected override ModeType? KeyRespond(ConsoleKey key)
        {
            ChangeCursorLevel(ModeType.Settings, 1, 2, key);
            if (key == ConsoleKey.Enter)
            {
                ChangingSliderPos(IntToSettingsProperty(cursorLevel));
            }
            if (key == ConsoleKey.Escape)
            {
                return ModeType.Start;
            }
            return ModeType.Settings;
        }
        private string IntToSettingsProperty(int i)
        {
            switch (i)
            {
                case 1:
                    return "FieldSize";
                case 2:
                    return "DifficultyLevel";
                default:
                    return "none";
            }
        }

        private void ChangingSliderPos(string propName)
        {
            sliderDisplayer.DisplaySliderField(ModeType.Settings, propName, ElementTag.Selected);
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Enter || key == ConsoleKey.Escape)
            {
                sliderDisplayer.DisplaySliderField(ModeType.Settings, propName, ElementTag.Unselected);
            }
            else
            {
                ChangeSliderPos(key, propName);
                ChangingSliderPos(propName);
            }
        }

        private void ChangeSliderPos(ConsoleKey key, string propertyName)
        {
            Settings settings = new Settings();
            var propValue = settings.GetProperty(propertyName);
            if(propValue == null)
            {
                return;
            }
            int value = ChangeValueByArrows((int)propValue, 1, 3, key);
            settings.SetProperty(propertyName, value);
            sliderDisplayer.DisplaySliderField(ModeType.Settings, propertyName, ElementTag.Selected);
        }

        private int ChangeValueByArrows(int value, int min, int max, ConsoleKey key)
        {
            if (key == ConsoleKey.RightArrow && value < max)
            {
                return ++value;
            }
            if (key == ConsoleKey.LeftArrow && value > min)
            {
                return --value;
            }
            return value;
        }
    }
}
