using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace GuyBad
{
    public class GuyBadManager : MonoBehaviour
    {
        private NetworkManager m_NetworkManager;
        public RelayConnector relayConnector;
        public GameObject soundManagerPrefab;

        public InputField joinCodeField;
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

        /*void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
            {
                StartButtons();
            }

            else
            {
                StatusLabels();
            }

            GUILayout.EndArea();
        }*/

        /*void StartButtons()
        {
            if (GUILayout.Button("Host (Relay)")) _ = StartHostWithRelay();
            joinCodeInput = GUILayout.TextField(joinCodeInput, GUILayout.Width(200));
            if (GUILayout.Button("Client (Relay)")) _ = StartClientWithRelay(joinCodeInput);
            if (GUILayout.Button("Server (No Relay)")) m_NetworkManager.StartServer();
        }*/

        async Task StartHostWithRelay()
        {
            if (relayConnector != null)
            {
                string joinCode = await relayConnector.SetupRelayHost(9);

                PlayerPrefs.SetString("RelayJoinCode", joinCode);
                PlayerPrefs.Save();

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

        public void OnClickHost()
        {
            _ = StartHostWithRelay();
        }

        public void OnClickServer()
        {
            m_NetworkManager.StartServer();
        }

        public void OnClickClientFromUI()
        {
            joinCodeInput = joinCodeField.text;
            _ = StartClientWithRelay(joinCodeInput);
        }

    }
}

