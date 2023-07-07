using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using System.Xml.Schema;
using static UnityEditor.PlayerSettings;

namespace Assets.Script {
    public class MapManager : MonoBehaviour {
        private static MapManager instance; // 单例实例
        public static MapManager Instance {
            get {
                return instance;
            }
        }

        [SerializeField] RuleTile dirtTile;
        [SerializeField] RuleTile grassTile;
        [SerializeField] RuleTile bushTile;
        public Tilemap groundMap;
        private TilemapRenderer groundMapRenderer;
        public Tilemap objectMap;
        private TilemapRenderer objectMapRenderer;
        private int rows = 0;
        private int columns = 0;
        private TileData[,] tileDatas = null;
        private int[,] AcMap = null;

        private void Awake() {
            GenerateTileMap();
        }


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
            instance = this;
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
            GenerateTileMap();
        }

        static MapManager() {
            // 私有构造函数，防止外部创建实例
            // 进行初始化操作
        }
        private MapManager() {
            // 私有构造函数，防止外部创建实例
            // 进行初始化操作
        }
    }
}
