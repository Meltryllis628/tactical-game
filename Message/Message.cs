using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts {
    public class Message {
        private ManagerCode target;
        public ManagerCode Target { get { return target; } set { target = value; } }
        private MessagesCode what;
        public MessagesCode ID { get { return what; } set { what = value; } }
        private int arg1;
        private int arg2;
        private int arg3;
        private object obj1;
        private object obj2;
        private object obj3;
        public Message(ManagerCode target, MessagesCode what) {
            Target = target;
            ID = what;
        }
    }
}
