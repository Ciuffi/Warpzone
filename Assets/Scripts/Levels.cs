using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;


public class Levels {
    public LevelObject Beginner;
    public LevelObject Beginnerdown;
    public Levels() {
        CreateBeginner();
    }

    private void CreateBeginner() {
        Beginner = new LevelObject();
        Beginnerdown = new LevelObject();
        Beginner.SetDelay(3);
        Beginner.AddBottomBlock();
        Beginner.AddHighJump();
        Beginner.AddTightSqueeze();
        Beginner.AddWall();
        Beginnerdown.SetDelay(3);
        Beginnerdown.AddBottomBlock();
        Beginnerdown.AddHighTightSqueeze();
        Beginnerdown.AddWall();
        Beginnerdown.AddBottomBlock();
    }

    private void buildRandomLine(LevelObject l) {
        int rand = Random.Range(0, 2);
        int randlength = Random.Range(0, 8);
        int randobstacle = Random.Range(1, 3);
        for (int i = 0; i < randlength / randobstacle; i++) {
            switch (rand) {
                case 0:
                    l.AddBottomBlockLine(randobstacle);
                    break;
                case 1:
                    l.AddMiddleBlockLine(randobstacle);
                    break;
                case 2:
                    l.AddTopBlockLine(randobstacle);
                    break;
            }

            l.AddRandomToLast();
        }

    }

    private void PickRandomBlock(LevelObject l) {
        int rand = Random.Range(0, 6);
        switch (rand) {
            case 0:
                l.AddBottomBlock();
                break;
            case 1:
                l.AddHighJump();
                break;
            case 2:
                l.AddHighTightSqueeze();
                break;
            case 3:
                l.AddMiddleBlock();
                break;
            case 4:
                l.AddTightSqueeze();
                break;
            case 5:
                l.AddTopBlock();
                break;
            case 6:
                l.AddWall();
                break;
        }
    }

    public LevelObject BuildRandomLevel(int length, int delay = 0) {
        LevelObject l = new LevelObject();
        l.SetDelay(delay);
        for (int i = 0; i < length; i++) {
            int rand = Random.Range(0, 16);
            if (rand < 13) {
                PickRandomBlock(l);
            }
            else {
                l.SetDelay(0);
                buildRandomLine(l);
                buildRandomLine(l);
                l.SetDelay(delay);
            }
        }
        return l;
    }
}