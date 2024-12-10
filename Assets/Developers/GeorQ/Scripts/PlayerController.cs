using UnityEngine;


public class PlayerController : MonoBehaviour 
{
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;
    private bool _isInitialized = false;


    public void Initialize(PlayerMovement playerMovement, PlayerLook playerLook)
    {
        _playerMovement = playerMovement;
        _playerLook = playerLook;
        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isInitialized) { return; }
        
        _playerMovement.Tick();
        _playerLook.Tick();
    }

    private void FixedUpdate()
    {
        if (!_isInitialized) { return; }

        _playerMovement.PhysicsTick();
    }
}