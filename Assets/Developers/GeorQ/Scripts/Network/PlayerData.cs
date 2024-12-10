using Mirror;
using UnityEngine;


public class PlayerData : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnPlayerIDChanged))]
    public int PlayerID;

    private CustomNetworkManager _networkManager;

    
    public void Initialize(int playerId)
    {
        PlayerID = playerId;
    }

    private void OnPlayerIDChanged(int oldHealth, int newHealth)
    {
        Debug.Log($"Health changed from {oldHealth} to {newHealth}");
    }
}