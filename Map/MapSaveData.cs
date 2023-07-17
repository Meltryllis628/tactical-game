using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachGame {
    [Serializable]
    public class MapSaveData: Save {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TileTypes[] GroundTiles { get; set; }
        public TileTypes[] ObjectTiles { get; set; }

        public int[] AcMap { get; set; }
    }
}
