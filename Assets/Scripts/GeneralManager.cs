using System;
using Unity.Netcode;
using UnityEngine;

public class GeneralManager : NetworkBehaviour
{
    private void Start()
    {
#if DEDICATED_SERVER
        Debug.Log("Running as DEDICATED SERVER");
        NetworkManager.Singleton.StartServer();
#endif
    }
}
