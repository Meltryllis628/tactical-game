using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TachGame {
    public class Message {
        private ManagerCode target;
        public ManagerCode Target { get { return target; } set { target = value; } }
        private MessagesCode what;
        public MessagesCode ID { get { return what; } set { what = value; } }
        public int Arg1 { get; set; }
        public int Arg2 { get; set; }
        public int Arg3 { get; set; }
        public object Obj1 { get; set; }
        public object Obj2 { get; set; }
        public object Obj3 { get; set; }

        public Message(ManagerCode target, MessagesCode what) {
            Target = target;
            ID = what;
        }
    }
}
