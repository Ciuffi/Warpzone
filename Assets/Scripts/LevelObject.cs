using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelObject {
        private const float Pos0 = 0f;
        private const float Pos1 = 0.8f;
        private const float Pos2 = 1.8f;
        private const float Pos3 = 3.2f;
        private const float Pos4 = 4.4f;
        private const float Pos5 = 5.6f;
        private readonly List<List<float>> _level;
        private int _addDelay;
        private readonly SpawnHandler _spawner;

        public void SetDelay(int delay) {
                _addDelay = delay;
        }
        private void AddDelay() {
                if (_addDelay == 0) return;
                AddWhiteSpace(_addDelay);
        }

        private float RandomPosition() {
                int rand = UnityEngine.Random.Range(0, 6);
                switch (rand) {
                        default: return Pos3;
                        case 0: return Pos1;
                        case 1: return Pos2;
                        case 2: return Pos3;
                        case 3: return Pos4;
                        case 4: return Pos5;
                }
        }

        public void AddRandomToLast() {
                _level[_level.Count - 1].Add(RandomPosition());
        }
        public void AddFloorBlockToLast(float seconds) {
                for (int i = 1; i < seconds / 0.3f; i++) {
                        _level[_level.Count - i].Add(Pos0);
                }
        }
        public LevelObject() {
                _level = new List<List<float>>();
        }

        public void AddBlocks(List<float> blocks) {
                _level.Add(new List<float>(blocks));
                AddDelay();
        }



        public List<List<float>> GetLevel() {
                return _level;
        }

        public void AddWall() {
                _level.Add(new List<float>(new float[] {
                        Pos1,
                        Pos2,
                        Pos3,
                        Pos4,
                        Pos5                        
                }));
                AddDelay();
        }
        public void AddTightSqueeze() {
                _level.Add(new List<float>(new float[] {
                        Pos1,
                        Pos3,
                        Pos4,
                        Pos5                       
                }));
                AddDelay();
        }
        
        public void AddHighTightSqueeze() {
                _level.Add(new List<float>(new float[] {
                        Pos1,
                        Pos2,
                        Pos4,
                        Pos5                        
                }));
                AddDelay();
        }
        public void AddHighJump() {
                _level.Add(new List<float>(new float[] {
                        Pos1,
                        Pos2,
                        Pos5                        
                }));
                AddDelay();
        }

        public void AddBottomBlock() {
                _level.Add(new List<float>(new float[] {
                        Pos1                       
                }));
                AddDelay();
        }

        public void AddMiddleBlock() {
                _level.Add(new List<float>(new float[] {
                        Pos2
                }));
                AddDelay();
        }
        public void AddTopBlock() {
                _level.Add(new List<float>(new float[] {
                        Pos3
                }));
                AddDelay();
        }

        public void AddBottomBlockLine(float seconds) {
                for (int i = 0; i < seconds / 0.3; i++) {
                        AddBottomBlock();
                }
                AddDelay();
        }
        public void AddMiddleBlockLine(float seconds) {
                for (int i = 0; i < seconds / 0.3; i++) {
                        AddMiddleBlock();
                }
                AddDelay();
        }
        public void AddTopBlockLine(float seconds) {
                for (int i = 0; i < seconds / 0.3; i++) {
                        AddTopBlock();
                }
                AddDelay();
        }

        private void AddWhiteSpace(float seconds) {
                for (int i = 0; i < seconds / 0.3; i++) {
                        _level.Add(new List<float>());
                }
        }


}
