using UnityEngine;
using Mirror;


public class PlayerNetworkSetup : NetworkBehaviour
{
    [SerializeField] private GameObject _playerLocalPrefab;


    public override void OnStartLocalPlayer()
    {
        Instantiate(_playerLocalPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
    }
}