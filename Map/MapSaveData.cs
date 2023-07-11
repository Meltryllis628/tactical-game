using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts {
    [Serializable]
    public class MapSaveData: Save {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GroundTileTypes[] GroundTiles { get; set; }
        public ObjectTileTypes[] objectTiles { get; set; }
    }
}
