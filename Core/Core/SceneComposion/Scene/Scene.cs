using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    public class Scene
    {
        [DataMember]
        public SceneType Type { get; set; }

        [DataMember]
        public Cell Offset { get; set; }

        [DataMember]
        public List<SceneElement> Elements;

        public Scene(SceneType type, Cell offset, List<SceneElement> elements)
        {
            Type = type;
            Offset = offset;
            Elements = elements;
        }
    }
}
