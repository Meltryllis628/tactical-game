using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using Assets.Script;
using Assets.Scripts.SaveLoad;

namespace Assets.Scripts {
    internal class MessageDistributionManager: UnitySingleton<MessageDistributionManager> {
        public string logText = "MessageManager";
        private void Update() {
            Message test = new Message(ManagerCode.MAP_RENDERER, MessagesCode.UPDATE_MAP);
            MessageQueue.Push(test);
            Message currentMessage = MessageQueue.Pop();
            if (currentMessage != null) {
                switch (currentMessage.Target) {
                    case ManagerCode.MAP_RENDERER:
                        MapRendererManager.Instance.MessageQueue.Push(currentMessage);
                        break;
                }
            }
        }
        private void Start() {
            Debug.Log(Instance.logText);
            Save test = new TestSave();
            test.SaveData("test1");
            string path = test.SavePath;
            TestSave loadedData = TestSave.LoadData<TestSave>(path);
            Debug.Log(loadedData.Aa);
        }
    }
}
