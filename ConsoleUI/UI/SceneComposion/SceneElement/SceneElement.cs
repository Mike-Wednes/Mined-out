using System.Runtime.Serialization;
using Core;

namespace ConsoleUI
{
    [DataContract]
    public class SceneElement
    {
        [DataMember]
        public String GraficName { get; set; }

        [DataMember]
        public Cell Offset { get; set; }

        public SceneElement(string graficName, Cell offset)
        {
            Offset = new Cell(offset);
            GraficName = graficName;
        }
    }
}
