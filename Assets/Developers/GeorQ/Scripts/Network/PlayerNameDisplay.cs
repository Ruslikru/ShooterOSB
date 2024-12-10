using TMPro;
using UnityEngine;
using Mirror;


public class PlayerNameDisplay : NetworkBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Transform textTransform;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerHealth _playerHealth;

    private Camera localCamera;


    private void Start()
    {
        if (isLocalPlayer)
        {
            localCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        if (localCamera == null)
        {
            localCamera = Camera.main;
            if (localCamera == null) return;
        }

        nameText.text = $"Health:{_playerHealth.Health}\nID:{_playerData.PlayerID}";

        textTransform.rotation = Quaternion.LookRotation(localCamera.transform.forward);
    }
}