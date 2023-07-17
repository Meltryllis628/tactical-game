using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace TachGame {
    public class TestMap: MonoBehaviour {
        private void Start() {
            string path = "C:\\Users\\10252\\Documents\\test_map1.xml";
            Message message = new Message(ManagerCode.MAP_MANAGER, MessagesCode.READ_MAP_FROM_SAVEDATA);
            message.Obj2 = path;message.Arg1 = 1;
            MessageDistributionManager.Instance.SendMessage(message);
        }
    }
}
