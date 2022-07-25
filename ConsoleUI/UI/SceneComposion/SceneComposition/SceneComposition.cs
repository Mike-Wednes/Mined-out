using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Core;

namespace ConsoleUI
{
    [DataContract]
    public class SceneComposition
    {
        [DataMember]
        public ModeType Type { get; set; }

        [DataMember]
        public Cell Offset { get; set; }

        [DataMember]
        public List<SceneElement> Elements;

        public SceneComposition(ModeType type, Cell offset, List<SceneElement> elements)
        {
            Type = type;
            Offset = offset;
            Elements = elements;
        }

        public static SceneComposition GetScene(ModeType type)
        {
            var scene = GetAllScenes()
                .Where(g => g.Type == type)
                .First();
            return scene;
        }

        private static SceneComposition[] GetAllScenes()
        {
            DirectoryInfo dir = new DirectoryInfo(@"Grafics/Scenes");
            var files = dir.GetFiles();
            List<SceneComposition> list = new List<SceneComposition>();
            foreach (var file in files)
            {
                var scene = SceneDeserialize(file.FullName);
                if (scene != null)
                {
                    list.Add(scene);
                }
            }
            return list.ToArray();
        }

        private static SceneComposition? SceneDeserialize(string path)
        {
            var jsonformatter = new DataContractJsonSerializer(typeof(SceneComposition));
            try
            {
                using (var file = new FileStream(path, FileMode.Open))
                {
                    var scene = jsonformatter.ReadObject(file) as SceneComposition;
                    return scene;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
