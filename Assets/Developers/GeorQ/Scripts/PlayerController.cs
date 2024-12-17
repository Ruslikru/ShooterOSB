using UnityEngine;


public class PlayerController : MonoBehaviour 
{
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;
    private bool _isInitialized = false;
    private bool _isGamePaused = false;


    public void Initialize(PlayerMovement playerMovement, PlayerLook playerLook)
    {
        _playerMovement = playerMovement;
        _playerLook = playerLook;
        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isInitialized || _isGamePaused) { return; }
        
        _playerMovement.Tick();
        _playerLook.Tick();
    }

    private void FixedUpdate()
    {
        if (!_isInitialized || _isGamePaused) { return; }

        _playerMovement.PhysicsTick();
    }

    public void SetPauseState(bool newState)
    {
        _isGamePaused = newState;
    }
}