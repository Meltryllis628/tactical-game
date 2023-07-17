using Assets.TachGame;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TachGame {
    public class MapManager : UnitySingleton<MapManager> {


        private string logText = "MapManager";
        private int rows = 0;
        private int columns = 0;
        private int[] acMap;
        public int[] ACMap { get { return acMap; } }
        private int[] obstMap;
        private MapSaveData map;
        private TileDict dict = new TileDict();

        private void Start() {
            Debug.Log(Instance.logText);

        }

        private void GenerateAcMap() { 
            acMap= new int[rows*columns];
            for(int i = 0; i < rows*columns; i++) {
                int ac0 = dict.ac[map.GroundTiles[i]];
                int ac1 = dict.ac[map.ObjectTiles[i]];
                acMap[i] = ac0 + ac1;
            }
            map.AcMap = (int[])acMap.Clone();
        }


        private void Update() {
            while (!MessageQueue.isEmpty) {
                Message currentMessage = MessageQueue.Pop();
                if (currentMessage != null) {
                    if(currentMessage.ID == MessagesCode.READ_MAP_FROM_SAVEDATA) {
                        if (currentMessage.Arg1 == 0) {
                            map = (MapSaveData)currentMessage.Obj1;
                        }
                        if (currentMessage.Arg1 == 1) {
                            string path = (string)currentMessage.Obj2;
                            map = MapSaveData.LoadData<MapSaveData>(path);
                        }
                    }
                    rows = map.Row;
                    columns= map.Column;
                    try {
                        GenerateAcMap();
                        map.SaveData();
                    } catch (Exception e) {
                        Debug.Log(e);
                    }
                    
                }
                Message message = new Message(ManagerCode.MAP_RENDERER, MessagesCode.RENDER_NEW_MAP);
                message.Obj1 = map;
                MessageDistributionManager.Instance.SendMessage(message);
            }
            
        }


    }
}
