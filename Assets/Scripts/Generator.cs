using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int levelID;
    public int roomCount;

    public Dictionary<Vector2Int, RoomData> dungeon = new Dictionary<Vector2Int, RoomData>();
    public List<Vector2Int> positions = new List<Vector2Int>();
    List<Vector2Int> validBossPositions = new List<Vector2Int>();
    RoomData startRoom;
    RoomData mBossRoom;
    RoomData bossRoom;

    System.Random sequence;

    public GameObject roomObject;

    int biome;

    void Start()
    {
        GenerateLevel(levelID);
    }

    public void GenerateLevel(int seed)
    {
        sequence = new System.Random(seed);
        dungeon.Clear();
        positions.Clear();

        biome = sequence.Next(8);

        Vector2Int startPos = new Vector2Int(0, 0);
        PlaceRoom(startPos);

        startRoom = dungeon[startPos];
        mBossRoom = dungeon[startPos];
        bossRoom = dungeon[startPos];

        while (dungeon.Count < roomCount && positions.Count > 0)
        {
            int index = sequence.Next(positions.Count);
            Vector2Int candidate = positions[index];
            positions.RemoveAt(index);

            validBossPositions.Add(candidate);
            PlaceRoom(candidate);
        }

        FinalizeDungeon();
    }

    void PlaceRoom(Vector2Int pos)
    {
        RoomData newRoom = new RoomData(pos);

        float growthRate = (float)dungeon.Count / roomCount;
        double doorChance = 0.8f - (growthRate * 0.6f);

        CheckNeighbor(newRoom, pos + Vector2Int.up, 0, 2, doorChance);
        CheckNeighbor(newRoom, pos + Vector2Int.right, 1, 3, doorChance);
        CheckNeighbor(newRoom, pos + Vector2Int.down, 2,0, doorChance);
        CheckNeighbor(newRoom, pos + Vector2Int.left, 3, 1, doorChance);

        dungeon.Add(pos, newRoom);
    }

    void CheckNeighbor(RoomData room, Vector2Int neighborPosition, int myDoor, int otherDoor, double chance)
    {
        if (dungeon.ContainsKey(neighborPosition))
        {
            room.door[myDoor] = dungeon[neighborPosition].door[otherDoor];
            room.doorType[myDoor] = dungeon[neighborPosition].doorType[otherDoor];
        }
        else
        {
            if (sequence.NextDouble() < chance)
            {
                room.door[myDoor] = true;

                if (!positions.Contains(neighborPosition))
                {
                    positions.Add(neighborPosition);
                }
            }

            if (room.door[myDoor])
            {
                if (room.position != Vector2Int.zero)
                {
                    room.doorType[myDoor] = sequence.Next(5);
                }
                else
                {
                    room.doorType[myDoor] = 0;
                }
            }
        }
    }

    void FinalizeDungeon()
    {
        while (mBossRoom == dungeon[Vector2Int.zero])
        {
            int index = sequence.Next(validBossPositions.Count);

            mBossRoom = dungeon[validBossPositions[index]];

            validBossPositions.RemoveAt(index);
        }

        while (bossRoom == dungeon[Vector2Int.zero])
        {
            int index = sequence.Next(validBossPositions.Count);

            bossRoom = dungeon[validBossPositions[index]];

            validBossPositions.RemoveAt(index);
        }

        startRoom.roomType = 1;
        mBossRoom.roomType = 2;
        bossRoom.roomType = 3;

        foreach(RoomData data in dungeon.Values)
        {
            if (!HasNeighbor(data.position + Vector2Int.up)) data.door[0] = false;
            if (!HasNeighbor(data.position + Vector2Int.right)) data.door[1] = false;
            if (!HasNeighbor(data.position + Vector2Int.down)) data.door[2] = false;
            if (!HasNeighbor(data.position + Vector2Int.left)) data.door[3] = false;

            RoomObject room = Instantiate(roomObject).GetComponent<RoomObject>();
            room.InitializeRoom(data, biome);
        }
    }

    bool HasNeighbor(Vector2Int neighborPos)
    {
        return dungeon.ContainsKey(neighborPos);
    }
}
