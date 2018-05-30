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
            if (Random.Range(0, 15) > 10) {
                l.AddFloorBlockToLast(randobstacle);
            }
            l.AddRandomToLast();
        }

    }

    private void PickRandomSimpleBlock(LevelObject l) {
        int rand = Random.Range(0, 3);
        switch (rand) {
            case 0:
                l.AddBottomBlock();
                break;
            case 1:
                l.AddMiddleBlock();
                break;
            case 2:
                l.AddTopBlock();
                break;
            case 3:
                l.AddWall();
                break;
        }
    }

    private void PickRandomComplexBlock(LevelObject l) {
        int rand = Random.Range(0, 2);
        switch (rand) {
            case 0:
                l.AddTightSqueeze();
                break;
            case 1:
                l.AddHighJump();
                break;
            case 2:
                l.AddHighTightSqueeze();
                break;

        }
    }

    public LevelObject BuildRandomLevel(int length, int delay = 0) {
        LevelObject l = new LevelObject();
        l.SetDelay(delay);
        for (int i = 0; i < length; i++) {
            int rand = Random.Range(0, 20);
            if (rand < 3) {
                PickRandomSimpleBlock(l);
            }
            else if (rand > 2 && rand < 14) {
                PickRandomComplexBlock(l);
            }
            else if (rand > 13 & rand < 16){
                l.SetDelay(0);
                buildRandomLine(l);
                l.SetDelay(delay);
            }
            else {
                l.AddFloorBlockToLast(0.6f);
            }
        }
        return l;
    }
}