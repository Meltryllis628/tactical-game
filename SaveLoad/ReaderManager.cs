using System.IO;
using UnityEngine;

namespace TachGame {
    public class ReaderManager : UnitySingleton<ReaderManager> {

        public string logText = "ReaderManager";

        public Message ReadMapFromCsv(string filePath) {
            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int columns = lines[0].Split(',').Length;
            TileTypes[] map = new TileTypes[rows * columns];
            int i = 0;
            int j = 0;

            foreach (string line in lines) {
                string[] values = line.Split(',');
                foreach (string value in values) {
                    map[i * columns + j] = (TileTypes)int.Parse(value);
                    j++;
                }
                j = 0;
                i++;
            }

            Message message = new Message(ManagerCode.MAP_MANAGER, MessagesCode.READ_MAP_FROM_CSV);
            message.Arg1 = rows; message.Arg2 = columns;
            message.Obj1 = map;


            return message;
        }

        private void Start() {
            Debug.Log(Instance.logText);
        }
    }
}
