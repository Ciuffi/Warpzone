using Random = UnityEngine.Random;

public class Floor : LevelObject{

    public Floor() {

        float pos = RandomPosition();
        for (int i = 0; i < Random.Range(8, 15); i++) {
            _positions.Add(pos);
        }
        _long = true;
    }
}
