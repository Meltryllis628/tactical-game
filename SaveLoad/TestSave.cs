using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts {
    internal class TestSave: Save {
        int a = 1;
        string aa = "foo";
        int[,] b = { { 1, 2 }, { 3, 4 }, { 5, 6 }, };
        GroundTileTypes c = GroundTileTypes.Dirt;
    }
}
