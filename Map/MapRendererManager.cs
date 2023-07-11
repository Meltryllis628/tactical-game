using Assets.Scripts;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script {
    public class MapRendererManager : UnitySingleton<MapRendererManager> {


        [SerializeField] RuleTile dirtTile;
        [SerializeField] RuleTile grassTile;
        [SerializeField] RuleTile bushTile;
        public Tilemap groundMap;
        public Tilemap objectMap;
        public string logText = "MapRendererManager: ";
        private int rows = 0;
        private int columns = 0;
        private TileData[,] tileDatas = null;
        private int[,] AcMap = null;

        private void CrossDemo() {
            rows = 10; columns = 12;
            tileDatas = new TileData[rows, columns];
            AcMap = new int[rows, columns];
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    tileDatas[i, j] = TileData.Create(grassTile);
                }
            }
            for (int i = 3; i <= 5; i++) {
                for (int j = 0; j <= 9; j++) {
                    tileDatas[i, j] = TileData.Create(dirtTile);
                    tileDatas[j, i] = TileData.Create(dirtTile);
                }
            }
            for (int i = 2; i <= 8; i++) {
                tileDatas[i, 3].AddStruct(bushTile);
            }
            for (int j = 4; j <= 6; j++) {
                tileDatas[5, j].AddStruct(bushTile);
            }
        }

        private void ReadCsv(String filePath) {
            string[] lines = File.ReadAllLines(filePath);
            rows = lines.Length;
            columns = lines[0].Split(',').Length;
            tileDatas = new TileData[rows, columns];

            foreach (string line in lines) {
                string[] values = line.Split(',');
                foreach (string value in values) {

                }
            }

        }

        private void GenerateTileMap() {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    tileDatas[i, j].SetPosition(new Vector2Int(i, j));
                    groundMap.SetTile(new Vector3Int(i - rows / 2, j - columns / 2, 0), tileDatas[i, j].GetTile());
                    if (tileDatas[i, j].GetTile1() != null) { objectMap.SetTile(new Vector3Int(i - rows / 2, j - columns / 2, 0), tileDatas[i, j].GetTile1()); }
                }
            }

        }

        private void Start() {
            Debug.Log(Instance.logText);
            // 获取TileMap组件
            //groundMap = GetComponent<Tilemap>();
            CrossDemo();
            // 生成TileMap并放置瓦片
            GenerateTileMap();
        }

        public int[,] GetAcMap() {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    AcMap[i, j] = tileDatas[i, j].GetAc();
                }
            }
            return AcMap;
        }

        private void Update() {
            while (!MessageQueue.isEmpty) {
                Message currentMessage = MessageQueue.Pop();
                if (currentMessage != null) {
                    //Debug.Log(logText + currentMessage.ID);
                }
            }
            CrossDemo();
            GenerateTileMap();
        }
    }
}
