using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

namespace Assets.Scripts {

    [Serializable]
    public class Save {
        private string savePath;

        public string SavePath { get => savePath; set => savePath = value; }

        public void SaveData(string name) {
            // 创建 XML 序列化器
            XmlSerializer serializer = new XmlSerializer(GetType());
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            SavePath = path + "\\" + name + ".xml";

            // 创建文件流，用于写入 XML 数据
            using (TextWriter writer = new StreamWriter(SavePath)) {
                // 使用序列化器将对象数据写入文件
                serializer.Serialize(writer, this);
            }
            //Debug.Log("Save to" + SavePath);
        }

        public static T LoadData<T>(string filePath) where T : Save {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StreamReader(filePath)) {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
 