using UnityEngine;

namespace TachGame {

    public class UnitySingleton<T> : MonoBehaviour
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
    }
}
