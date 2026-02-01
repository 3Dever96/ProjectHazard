using UnityEngine;

public class RoomObject : MonoBehaviour
{
    [Header("Room Parts")]
    [SerializeField] 
    SpriteRenderer roomCenter;
    
    [SerializeField] 
    SpriteRenderer[] doors;
    
    [SerializeField] 
    SpriteRenderer roomLayout;

    [Header("Room Information")]
    [SerializeField] 
    Sprite[] layout;
    
    [SerializeField]
    Color[] biomeColors;

    [SerializeField]
    Color[] roomTypeColors;
    
    [SerializeField] 
    Color[] doorColors;

    public void InitializeRoom(RoomData data, int biome)
    {
        transform.localPosition = Vector3.zero + new Vector3(data.position.x, data.position.y, 0f);

        roomCenter.color = roomTypeColors[data.roomType];

        int index = 0;

        if (data.door[0]) index += 1;
        if (data.door[1]) index += 2;
        if (data.door[2]) index += 4;
        if (data.door[3]) index += 8;

        roomLayout.sprite = layout[index];
        roomLayout.color = biomeColors[biome];

        for (var i = 0; i < data.door.Length; i++)
        {
            if (data.door[i])
            {
                doors[i].color = doorColors[data.doorType[i]];
            }
            else
            {
                doors[i].gameObject.SetActive(false);
            }
        }
    }
}
