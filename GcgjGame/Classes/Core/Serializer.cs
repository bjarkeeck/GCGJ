using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace GcgjGame.Classes.Core
{
    [Serializable]
    public class ObjectData
    {
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    [Serializable]
    public class ObjectRootData
    {
        public List<ObjectData> lvlData = new List<ObjectData>();
    }


    public class Serializer
    {
        public static LevelData DeserializeLevel()
        {
            string path = Path.Combine(Assembly.GetExecutingAssembly().Location, "../../../../Content/LevelData.xml");
            ObjectRootData root = DeserializeFromXml<ObjectRootData>(path);

            LevelData levelData = new LevelData();
            foreach (var item in root.lvlData)
            {
                GameObject g = (GameObject)Activator.CreateInstance(Type.GetType(item.Type));
                g.Initialize(new Vector2(item.X, item.Y));
                levelData.GameObjects.Add(g);
            }
            return levelData;
        }

        public static void SerializeLevel(LevelData levelData)
        {
            string path = Path.Combine(Assembly.GetExecutingAssembly().Location, "../../../../Content/LevelData.xml");

            ObjectRootData root = new ObjectRootData();
            foreach (var item in levelData.GameObjects)
            {
                root.lvlData.Add(new ObjectData()
                {
                    Type = item.GetType().FullName,
                    X = (int)item.Position.X,
                    Y = (int)item.Position.Y
                });
            }
            SerializeToXml(root, path);
        }


        public static void SerializeToXml<T>(T obj, string fileName)
        {
            File.WriteAllText(fileName, "");
            XmlSerializer ser = new XmlSerializer(typeof(T));
            var fileStream = File.OpenWrite(fileName);
            ser.Serialize(fileStream, obj);
            fileStream.Close();
        }

        public static T DeserializeFromXml<T>(string filename)
        {
            XmlSerializer _xmlSerializer = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var result = (T)_xmlSerializer.Deserialize(stream);
            stream.Close();
            return result;
        }
    }
}
