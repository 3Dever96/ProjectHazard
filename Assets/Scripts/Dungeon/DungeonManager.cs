using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectHazard.Dungeon
{
    [RequireComponent(typeof(NetworkObject))]
    public class DungeonManager : NetworkBehaviour
    {
        public static DungeonManager instance;

        public int levelID;
        public NetworkVariable<int> netLevelID;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(gameObject);
        }

        public override void OnNetworkSpawn()
        {
            netLevelID.OnValueChanged += SetLevelID;
        }

        public override void OnNetworkDespawn()
        {
            netLevelID.OnValueChanged -= SetLevelID;
        }

        public void OnCreateNewLevel()
        {
            if (IsClient)
            {
                if (IsServer)
                {
                    netLevelID.Value = (int)(System.DateTime.Now.Ticks & 0x7FFFFFFF);

                    NetworkManager.Singleton.SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
                }
            }
            else
            {
                levelID = (int)(System.DateTime.Now.Ticks & 0x7FFFFFFF);
            }
        }

        void SetLevelID(int oldVal, int newVal)
        {
            levelID = newVal;
        }
    }
}
