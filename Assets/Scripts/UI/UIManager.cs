using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : NetworkBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;
    [SerializeField] private Button startServerButton;
    [SerializeField] private Button startSpawnButton;
    [SerializeField] private TextMeshProUGUI inGameText;

    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        inGameText.text = $"Players in game: {PlayerManager.Instance.PlayersInGame} <br>" + 
          $"Spawning: {EnemySpawnManager.Instance.IsSpawning}";
    }

    private void Start()
    {
        startHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Host Started...");
            }
            else
            {
                Debug.Log("Host can not be started");
            }
        });

        startClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client Started...");
            }
            else
            {
                Debug.Log("Client can not be started");
            }
        });

        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Server Started...");
            }
            else
            {
                Debug.Log("Server can not be started");
            }
        });

        startSpawnButton.onClick.AddListener(() =>
        {
            if (IsServer)
            {
                EnemySpawnManager.Instance.SetIsSpawn((prev) => !prev);
            }
        });
    }
}
