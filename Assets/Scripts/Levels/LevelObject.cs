using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelObject {
        protected const float Pos0 = 0f;
        protected const float Pos1 = 0.8f;
        protected const float Pos2 = 1.8f;
        protected const float Pos3 = 3.2f;
        protected const float Pos4 = 4.4f;
        protected List<float> _positions;
        protected bool _long;
        
        protected float RandomPosition() {
                int rand = UnityEngine.Random.Range(0, 6);
                switch (rand) {
                        default: return Pos3;
                        case 0: return Pos1;
                        case 1: return Pos2;
                        case 2: return Pos3;
                        case 3: return Pos4;
                }
        }
}
