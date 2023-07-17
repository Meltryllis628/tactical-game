using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TachGame {
    [Serializable]
    public class CharacterData:Save {
        public string Name { get; set; }
        public int STR { get; set; }
        public int DEX { get; set; }
        public int CON { get; set;}
        public int INT { get; set; }
        public int WIS { get; set; }
        public int CHA { get; set; }

        public int HP { get; set; }
        public int MP { get; set; }
        public MajorArcana Path { get; set; }
        public ClassStg9 C9 { get; set; }
        public ClassStg8 C8 { get; set; }
        public ClassStg7 C7 { get; set; }
        public Vector2Int Position { get; set; }

    }
}
