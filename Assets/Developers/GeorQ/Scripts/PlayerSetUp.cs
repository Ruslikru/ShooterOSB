using BNG;
using Mirror;
using System;
using UnityEngine;
using UnityEngine.XR.OpenXR.NativeTypes;


public class PlayerSetUp : NetworkBehaviour
{
    [SerializeField] private Rigidbody _playerRB;
    [SerializeField] private Transform _orientation;
    [SerializeField] private LayerMask _maskToAvoid;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private GameObject _sunglasses;
    [SerializeField] private GameObject _hud;


    public override void OnStartLocalPlayer()
    {
        if (isServer)
        {
            DisableClientObjects();
        }

        if (_hud)
        {
            _hud.SetActive(true);
            
        }

        _sunglasses.SetActive(false);
        _playerCamera.SetActive(true);
        PlayerMovement playerMovement = new PlayerMovement(_playerRB, _orientation, _maskToAvoid);
        PlayerLook playerLook = new PlayerLook(_orientation, Camera.main.transform);
        _playerController.Initialize(playerMovement, playerLook);
    }

    private void DisableClientObjects()
    {
        XRLocalRig.Instance.transform.root.gameObject.SetActive(false);
    }
}