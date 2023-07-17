using UnityEngine;

namespace TachGame {
    public class CharacterManager : UnitySingleton<CharacterManager> {
        private CharacterData character;
        private void Update() {
            while (!MessageQueue.isEmpty) {
                Message currentMessage = MessageQueue.Pop();
                if (currentMessage != null) {
                    if (currentMessage.ID == MessagesCode.READ_CHARA_FROM_SAVEDATA) {
                        string path = (string)currentMessage.Obj1;
                        character = CharacterData.LoadData<CharacterData>(path);
                        Debug.Log(character.STR);
                    }
                }
            }
        }
    }
}
