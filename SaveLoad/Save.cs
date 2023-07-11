using System;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts {
    public class Save {
        public void SaveData(string name) {
            // 创建 XML 序列化器
            XmlSerializer serializer = new XmlSerializer(GetType());
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            // 创建文件流，用于写入 XML 数据
            using (FileStream fileStream = new FileStream(path + name + ".xml", FileMode.Create)) {
                // 使用序列化器将对象数据写入文件
                serializer.Serialize(fileStream, this);
            }
        }
    }
}
