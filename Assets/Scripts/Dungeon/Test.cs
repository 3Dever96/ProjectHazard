using UnityEngine;

namespace ProjectHazard.Dungeon
{
    public class Test : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                DungeonManager.instance.OnCreateNewLevel();
            }
        }
    }
}
