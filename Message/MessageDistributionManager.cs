using UnityEngine;

namespace TachGame {
    internal class MessageDistributionManager : UnitySingleton<MessageDistributionManager> {
        public string logText = "MessageManager";
        public void SendMessage(Message message) { MessageQueue.Push(message); }
        private void Start() {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string SavePath = path + "\\acmap.xml";
            TileDict a = new TileDict();
            //a.output(SavePath);
            Debug.Log(Instance.logText);
        }

        public override void UpdateMessage(Message currentMessage) {
            Debug.Log(logText + ": Send message to " + currentMessage.Target);
            switch (currentMessage.Target) {
                case ManagerCode.MAP_RENDERER:
                    MapRendererManager.Instance.MessageQueue.Push(currentMessage);
                    break;
                case ManagerCode.MAP_MANAGER:
                    MapManager.Instance.MessageQueue.Push(currentMessage);
                    break;
                case ManagerCode.CHARA:
                    CharacterManager.Instance.MessageQueue.Push(currentMessage);
                    break;
            }
        }

        public override void UpdateElse() {
            return;
        }
    }
}
