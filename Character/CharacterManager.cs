using UnityEngine;

namespace TachGame {
    public class CharacterManager : UnitySingleton<CharacterManager> {
        private CharacterData character;

        public override void UpdateElse() {
            throw new System.NotImplementedException();
        }

        public override void UpdateMessage(Message currentMessage) {
            if (currentMessage.ID == MessagesCode.READ_CHARA_FROM_SAVEDATA) {
                string path = (string)currentMessage.Obj1;
                character = CharacterData.LoadData<CharacterData>(path);
                Debug.Log(character.STR);
            }
        }
    }
}
