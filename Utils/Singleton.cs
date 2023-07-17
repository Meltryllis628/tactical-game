using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

namespace TachGame {

    public abstract class UnitySingleton<T> : MonoBehaviour
            where T : Component {
        private static T instance;
        private MessageQueue messageQueue = new MessageQueue();
        public MessageQueue MessageQueue { get { return messageQueue; } }
        public static T Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType(typeof(T)) as T;
                    if (instance == null) {
                        GameObject obj = new GameObject();
                        //obj.hideFlags = HideFlags.DontSave;
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        instance = (T)obj.AddComponent(typeof(T));
                    }
                }
                return instance;
            }
        }

        public virtual void Awake() {
            DontDestroyOnLoad(this.gameObject);
            if (instance == null) {
                instance = this as T;
            } else {
                Destroy(gameObject);
            }
        }
        public void Update() {
            while (!MessageQueue.isEmpty) {
                Message currentMessage = MessageQueue.Pop();
                if (currentMessage != null) {
                    UpdateMessage(currentMessage); 
                }
            }
            UpdateElse();
        }
        public abstract void UpdateMessage(Message currentMessage);


        public abstract void UpdateElse();
    }
}
