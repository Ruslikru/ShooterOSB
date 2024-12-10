using Mirror;
using UnityEngine;


public class Shooting : NetworkBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _targetMask;
    
    private float rayDistance = 100;


    private void Update()
    {
        if (!isLocalPlayer) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = _playerCamera.ScreenPointToRay(screenCenter);
        Debug.Log("1");
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, _targetMask))
        {
            Debug.Log("2");
            if (hit.transform.root.TryGetComponent(out PlayerHealth playerHealth))
            {
                Debug.Log("3");
                playerHealth.DealDamage(30);
            }
        }
    }
}