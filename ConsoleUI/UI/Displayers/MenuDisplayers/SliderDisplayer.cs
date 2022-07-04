using Core;

namespace ConsoleUI
{
    internal class SliderDisplayer : MenuDisplayer
    {
        internal void DisplaySliderFields(ModeType type)
        {
            var propNames = typeof(Settings).GetProperties();
            foreach (var prop in propNames)
            {
                DisplaySliderField(type, prop.Name, ElementTag.Unselected);
            }
        }

        internal void DisplaySliderField(ModeType type, string propertyName, ElementTag tag)
        {
            var scene = SceneComposition.GetScene(type);
            var slider = scene.Elements.Where(g => g.GraficName.Contains(propertyName + "Slider")).First();
            DisplayGraficCollection("SliderField", adaptSceneOffset(scene, slider));
            DisplaySlider(adaptSceneOffset(scene, slider), propertyName, tag);
        }

        private void DisplaySlider(Cell fieldDefaultPos, string propertyName, ElementTag tag)
        {
            var propValue = new Settings().GetProperty(propertyName);
            if (propValue == null)
            {
                return;
            }
            int value = (int)propValue;
            fieldDefaultPos++;
            ClearSlider(fieldDefaultPos);
            fieldDefaultPos.X += --value * 3;
            DisplayGraficCollection("Slider" + tag.ToString(), fieldDefaultPos);
        }

        private void ClearSlider(Cell cell)
        {
            DisplayGraficCollection("ClearedSlider", cell);
        }
    }
}
