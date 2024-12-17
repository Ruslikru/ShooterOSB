using Mirror;
using UnityEngine;


public class AdminRaycastLogic : NetworkBehaviour
{
    [SerializeField] private AdminUserTab _adminUserTab;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _targetMask;
    private float _rayDistance = 50;
    private CustomNetworkManager _networkManager;


    public override void OnStartServer()
    {
        _networkManager = NetworkManager.singleton as CustomNetworkManager;
    }

    private void OnEnable()
    {
        _adminUserTab.OnTabStateChanged += SetPauseState;
    }

    private void OnDisable()
    {
        _adminUserTab.OnTabStateChanged -= SetPauseState;
    }

    private void SetPauseState(bool newState)
    {
        _playerController.SetPauseState(newState);
    }

    private void Update()
    {
        if (!isServer) { return; }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    GetPlayer();
        //}

        if (Input.GetKeyDown(KeyCode.Tab) && _networkManager)
        {
            Debug.Log($"There are {_networkManager.GetActiveClients().Count} active clients!");
        }
    }

    private void GetPlayer()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = _camera.ScreenPointToRay(screenCenter);
     
        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _targetMask))
        {
            if (hit.transform.root.TryGetComponent(out PlayerData playerData))
            {
                if (_adminUserTab.gameObject.activeSelf) { return; }

                _adminUserTab.gameObject.SetActive(true);
                _adminUserTab.OpenPlayerInfo(playerData);
            }
        }
    }
}