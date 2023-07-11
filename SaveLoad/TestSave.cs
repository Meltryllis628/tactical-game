using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assets.Scripts {
    [Serializable]
    public class TestSave: Save {
        private int a = 1;
        private string aa = "foo";
        private int[] b = { 1, 2, 3, 4 };
        private GroundTileTypes c = GroundTileTypes.Dirt;

        public int A { get => a; set => a = value; }
        public string Aa { get => aa; set => aa = value; }
        public int[] B { get => b; set => b = value; }
        public GroundTileTypes C { get => c; set => c = value; }
    }
}
