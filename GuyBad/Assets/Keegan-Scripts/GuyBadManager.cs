using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

namespace GuyBad
{
    public class GuyBadManager : MonoBehaviour
    {
        private NetworkManager m_NetworkManager;
        public RelayConnector relayConnector;
        public GameObject soundManagerPrefab;

        private string joinCodeInput = "";

        void Awake()
        {
            m_NetworkManager = GetComponent<NetworkManager>();

            if (GameObject.FindObjectOfType<SoundManagerController>() == null && soundManagerPrefab != null )
            {
                GameObject sm = Instantiate(soundManagerPrefab);
                DontDestroyOnLoad(sm);
            }
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
            {
                StartButtons();
            }

            else
            {
                StatusLabels();
                SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        void StartButtons()
        {
            if (GUILayout.Button("Host (Relay)")) _ = StartHostWithRelay();
            joinCodeInput = GUILayout.TextField(joinCodeInput, GUILayout.Width(200));
            if (GUILayout.Button("Client (Relay)")) _ = StartClientWithRelay(joinCodeInput);
            if (GUILayout.Button("Server (No Relay)")) m_NetworkManager.StartServer();
        }

        async Task StartHostWithRelay()
        {
            if (relayConnector != null)
            {
                string joinCode = await relayConnector.SetupRelayHost(9);
                Debug.Log("Relay Join Code: " + joinCode);
                m_NetworkManager.StartHost();
            }

            else
            {
                Debug.LogWarning("RelayConnector is not assigned!");
            }
        }

        async Task StartClientWithRelay(string joinCode)
        {
            if (relayConnector != null && !string.IsNullOrEmpty(joinCode))
            {
                await relayConnector.SetupRelayClient(joinCode);
                m_NetworkManager.StartClient();
            }

            else
            {
                Debug.LogWarning("RelayConnector is not assigned or join code is empty!");
            }
        }

        void StatusLabels()
        {
            var mode = m_NetworkManager.IsHost ?
                "Host" : m_NetworkManager.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        void SubmitNewPosition()
        {
            if (GUILayout.Button(m_NetworkManager.IsServer ? "Move" : "Request Position Change"))
            {
                if (m_NetworkManager.IsServer && !m_NetworkManager.IsClient)
                {
                    foreach(ulong uid in m_NetworkManager.ConnectedClientsIds)
                    {
                        m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<Player>();
                    }
                }

                else
                {
                    var playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();
                    var player = playerObject.GetComponent<Player>();
                }
            }
        }
    }
}

