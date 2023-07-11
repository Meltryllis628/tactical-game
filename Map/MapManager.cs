using Assets.Scripts;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script {
    public class MapManager : UnitySingleton<MapManager> {


        public string logText = "MapManager: ";
        private int rows = 0;
        private int columns = 0;
        private GroundTileTypes[,] groundTilesMap;
        private ObjectTileTypes[,] objectTilesMap;
        private int[,] acMap;
        private int[,] obstMap;

        
        private void Start() {
            Debug.Log(Instance.logText);

        }


        private void Update() {
            while (!MessageQueue.isEmpty) {
                Message currentMessage = MessageQueue.Pop();
                if (currentMessage != null) {
                    Debug.Log(logText + currentMessage.ID);
                }
            }
        }


    }
}
