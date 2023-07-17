using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TachGame {
    public enum TileTypes {
        None = 0,

        Grass = 10101,
        Dirt = 10102,

        Hedge_short = 10301,
        Hedge_tall = 10901,

        Street = 201
    }

    public class TileDict{
        public IDictionary<TileTypes, int> ac = new Dictionary<TileTypes, int>() {
            {TileTypes.None, 0},
            {TileTypes.Grass, 1},
            {TileTypes.Dirt, 1},
            {TileTypes.Hedge_short, 3},
            {TileTypes.Hedge_tall, 9}
        };
    }


}
