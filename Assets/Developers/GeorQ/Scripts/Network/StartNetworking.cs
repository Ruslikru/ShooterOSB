using Mirror;
using Mirror.Discovery;
using UnityEngine;


[RequireComponent(typeof(NetworkDiscovery))]
public class StartNetworking : MonoBehaviour
{
    //public GameConfig GameConfig;
    public bool IsAdminBuild;

    private NetworkDiscovery _networkDiscovery;
    private CustomNetworkManager _networkManager;


    void Start()
    {
        _networkManager = NetworkManager.singleton as CustomNetworkManager;
        _networkDiscovery = transform.GetComponent<NetworkDiscovery>();

        if (!_networkManager) { return; }

        if (IsAdminBuild)
        {
            StartHost();
            return;
        }

        StartClient();
    }

    private void StartHost()
    {
        Debug.Log("start host");
        _networkManager.StartHost();
        _networkDiscovery.AdvertiseServer();
    }

    private void StartClient()
    {
        Debug.Log("start client");
        _networkDiscovery.StartDiscovery();
        _networkDiscovery.OnServerFound.AddListener(OnServerFound);
    }


    private void OnServerFound(ServerResponse response)
    {
        Debug.Log($"Сервер найден: {response.EndPoint.Address}");

        _networkManager.networkAddress = response.EndPoint.Address.ToString();

        _networkManager.StartClient();
    }
}