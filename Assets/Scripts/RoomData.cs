using UnityEngine;

[System.Serializable]
public class RoomData
{
    public Vector2Int position;

    public bool[] door = new bool[4];

    public int[] doorType = new int[4];
    // 0 = normal;
    // 1 = knight;
    // 2 = mage;
    // 3 = ranger;
    // 4 = barbarian

    public int roomType;

    public RoomData(Vector2Int pos)
    {
        position = pos;
    }
}
