using UnityEngine;
using Unity.Cinemachine;
using ProjectHazard.PlayerFeatures;

public class RoomObject : MonoBehaviour
{
    [Header("Room Parts")]
    [SerializeField]
    Vector2 roomSize;
    [SerializeField]
    GameObject[] wall;

    CinemachineCamera cam;

    [Header("Visuals")]
    [SerializeField]
    Color[] biomeColors;

    void Awake()
    {
        cam = GetComponentInChildren<CinemachineCamera>();
    }

    public void InitializeRoom(RoomData data, int biome)
    {
        transform.localPosition = Vector3.zero + new Vector3(data.position.x * roomSize.x, 0f, data.position.y * roomSize.y);

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            r.material.color = biomeColors[biome];
        }

        for (var i = 0; i < wall.Length; i++)
        {
            wall[i].SetActive(!data.door[i]);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.IsOwner)
            {
                cam.Prioritize();
            }
        }
    }
}