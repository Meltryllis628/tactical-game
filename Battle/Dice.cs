using System;
using UnityEngine;
using Random = System.Random;

namespace TachGame {

    internal class Dice {
        int maxValue = 20;
        Random rnd = new Random();
        public Dice(int n) {
            maxValue = n;
        }
        public int pure() {
            return rnd.Next(maxValue) + 1;
        }
        public int advantage() {
            int a = rnd.Next(maxValue) + 1;
            int b = rnd.Next(maxValue) + 1;
            return a > b ? a : b;
        }
        public int disadvantage() {
            int a = rnd.Next(maxValue) + 1;
            int b = rnd.Next(maxValue) + 1;
            return a < b ? a : b;
        }
    }
    
}