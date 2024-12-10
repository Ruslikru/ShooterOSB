using UnityEngine;
using Mirror;


public class CustomNetworkManager : NetworkManager
{
    [SerializeField] private GameObject _adminPrefab;
    private int _playerID = 0;


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        //Do not call base method please, it  was fully ovverided
        bool isAdmin = conn == NetworkServer.localConnection;
        GameObject player = InstantiatePlayerPrefab(isAdmin);

        if (!isAdmin)
        {
            InitClient(player);
        }

        NetworkServer.AddPlayerForConnection(conn, player);
    }

    private GameObject InstantiatePlayerPrefab(bool isAdmin)
    {
        Transform startPos = GetStartPosition();

        GameObject player;

        if (isAdmin)
        {
            player = startPos != null
                ? Instantiate(_adminPrefab, startPos.position, startPos.rotation)
                : Instantiate(_adminPrefab);
        }
        else
        {
            player = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab);
        }

        return player;
    }

    private void InitClient(GameObject player)
    {
        PlayerData playerData = player.GetComponent<PlayerData>();
        playerData.PlayerID = _playerID++;
    }
}