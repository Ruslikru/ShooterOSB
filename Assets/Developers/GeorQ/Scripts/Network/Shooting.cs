using Mirror;
using System;
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
            CmdPlayEffects(transform.position);
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdPlayEffects(Vector3 muzzleFlashPos)
    {
        RpcPlayEffects(muzzleFlashPos);
    }

    [ClientRpc]
    private void RpcPlayEffects(Vector3 muzzleFlashPos)
    {
        throw new NotImplementedException();
    }

    private void Shoot()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = _playerCamera.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, _targetMask))
        {
            if (hit.transform.root.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.DealDamage(30);
            }
        }
    }
}