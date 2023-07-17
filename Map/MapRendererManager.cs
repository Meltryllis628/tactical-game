using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TachGame {
    public class MapRendererManager : UnitySingleton<MapRendererManager> {


        public Tilemap groundMap = null;
        private bool Test;
        //public Tilemap objectMap;
        private string logText = "MapRendererManager";
        private int rows = 0;
        private int columns = 0;
        private MapSaveData map;
        string workingDirectory = "";

        private RuleTile getTile(TileTypes tileName) {
            string assetName = tileName.ToString();
            string assetPath = "Assets/Tiles/" + assetName + ".asset";
            RuleTile tile = AssetDatabase.LoadAssetAtPath<RuleTile>(assetPath);
            return tile;
        }

        private void GenerateTileMap() {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    groundMap.SetTile(new Vector3Int(j - columns / 2, -rows / 2 + i, 0), getTile(map.GroundTiles[i * columns + j]));
                    if (getTile(map.ObjectTiles[i * columns + j]) != null) { groundMap.SetTile(new Vector3Int(j - columns / 2, -rows / 2 + i, 1), getTile(map.ObjectTiles[i * columns + j])); }
                }
            }
        }


        private void Start() {
            //Debug.Log(workingDirectory);
            Debug.Log(Instance.logText);
            //workingDirectory = Application.dataPath;
            // 获取TileMap组件
            groundMap = GetComponent<Tilemap>();
        }




        private void Update() {
            while (!MessageQueue.isEmpty) {
                Message currentMessage = MessageQueue.Pop();
                if (currentMessage != null) {
                    //Debug.Log(logText + currentMessage.ID);
                    if (currentMessage.ID == MessagesCode.RENDER_NEW_MAP) {
                        map = (MapSaveData)currentMessage.Obj1;
                        Debug.Log(logText + ": Rendering map " + map.Name);
                        rows = map.Row;
                        columns = map.Column;
                        Debug.Log(logText + ": Row " + rows + ", Column " + columns);
                        continue;
                    }
                }
            }
            GenerateTileMap();
        }
    }
}
