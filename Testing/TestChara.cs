using System;
using System.IO;
using UnityEngine;

namespace TachGame {
    [Serializable]
    public class TestChara : MonoBehaviour {
        public CharacterData Character = new CharacterData() {
            STR = 18,
            DEX = 18,
            CON = 18,
            INT = 18,
            WIS = 18,
            CHA = 18,
            Position = new Vector2Int(5, 5),
            HP = 22,
            MP = 22,
        };
         private void Start () {
            string path = "C:\\Users\\10252\\Documents\\TestChara1.xml";
            Message message = new Message(ManagerCode.CHARA, MessagesCode.READ_CHARA_FROM_SAVEDATA);
            message.Obj1 = path; 
            MessageDistributionManager.Instance.SendMessage(message);
        }
    }
}
