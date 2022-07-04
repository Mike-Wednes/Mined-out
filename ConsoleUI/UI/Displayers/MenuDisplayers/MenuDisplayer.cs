using Core;

namespace ConsoleUI
{
    internal abstract class MenuDisplayer : ConsoleDisplayer
    {

        internal void DisplayElements(ModeType modeType, ElementType elementType, ElementTag tag)
        {
            var elements = FiltredElements(modeType, elementType, tag);
            foreach (var element in elements)
            {
                DisplayGraficCollection(element.GraficName, adaptSceneOffset(SceneComposition.GetScene(modeType), element));
            }
        }

        private SceneElement[] FiltredElements(ModeType modeType, ElementType elementType, ElementTag tag)
        {
            var scene = SceneComposition.GetScene(modeType);
            var elements = scene.Elements
                .Where(g => g.GraficName.Contains(elementType.ToString()) 
                && g.GraficName.Contains(tag.ToString()));
            return elements.ToArray();
        }

        internal void DisplayElement(ModeType type, string elementName)
        {
            var scene = SceneComposition.GetScene(type);
            var element = scene.Elements
                .Where(g => g.GraficName.Contains(elementName))
                .FirstOrDefault();
            if (element == null)
            {
                return;
            }
            DisplayGraficCollection(element.GraficName, adaptSceneOffset(scene, element));
        }

        protected Cell adaptSceneOffset(SceneComposition scene, SceneElement element)
        {
            return new Cell(scene.Offset.X + element.Offset.X, scene.Offset.Y + element.Offset.Y);
        }
    }
}
