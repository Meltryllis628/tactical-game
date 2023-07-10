using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts {
    public class MessageQueue {
        private ConcurrentQueue<Message> messageQueue = new ConcurrentQueue<Message>();
        public Message Peek() {
            Message result;
            if (!messageQueue.TryPeek(out result)) {
                return null;
            } else {
                return result;
            }
        }

        public Message Pop() {
            Message result;
            if (!messageQueue.TryDequeue(out result)) {
                return null;
            } else {
                return result;
            }
        }

        public void Push(Message message) {
            messageQueue.Enqueue(message);
            return;
        }

        public void Clear() {
            messageQueue.Clear();
        }
        public void Discard() {
            Message _;
            messageQueue.TryDequeue(out _);
            return;
        }

        public bool isEmpty { get { return messageQueue.IsEmpty; } }
        public int Count {get { return messageQueue.Count; } }
    }
}
