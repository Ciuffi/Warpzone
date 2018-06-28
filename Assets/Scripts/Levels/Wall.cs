
using System.Collections.Generic;

public class Wall : LevelObject{

    public Wall() {
        _positions.Add(new List<float>(new float[] {
            Pos0,
            Pos1,
            Pos3,
            Pos4
        }));
    }
}
