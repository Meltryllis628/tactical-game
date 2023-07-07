using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script
{
    public enum StructType { 
        None,
        Bush_short,
        Bush_long,
    }

    public class TileData {
        private int AcPoint; 
        private Vector2Int position;
        //private GroundType groundType;
        private TileBase tile;
        private TileBase tile1 = null;
        

        public void SetTile(TileBase tile)
        {
            this.tile = tile;
        }
        public void SetPosition(Vector2Int position)
        {
            this.position = position;
        }
        public TileBase GetTile() {
            return tile;
        }
        public TileBase GetTile1()
        {
            return tile1;
        }
        public Vector2Int GetPosition()
        {
            return position;
        }
        public int GetAc() {
            return AcPoint;
        }

        public static TileData Create(TileBase CurrentTile) => new TileData
        {
            //groundType = GroundType.Grass,
            tile = CurrentTile,
            AcPoint = 1,
            position = new Vector2Int(0, 0),
        };

        public void AddStruct(TileBase CurrentTile) { 
            tile1 = CurrentTile;
            AcPoint += 2;
        }

    }
}


