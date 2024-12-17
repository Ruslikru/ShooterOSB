using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Mirror;
using System.Collections.Generic;


public class AdminUserTab : MonoBehaviour
{
    public event Action<bool> OnTabStateChanged;

    [SerializeField] private TMP_Text _playerID;
    [SerializeField] private TMP_InputField _playerNickname;

    [SerializeField] private TMP_Dropdown _teamDropdown;
    [SerializeField] private TMP_Dropdown _reloadDropdown;
    [SerializeField] private TMP_Dropdown _roughnessDropdown;

    [SerializeField] private Button _updateButton;
    
    private PlayerData _lastPlayerData;
    private Dictionary<NetworkConnectionToClient, PlayerData> _playersData = new();
    

    private void OnEnable()
    {
        _updateButton.onClick.AddListener(UpdatePlayerInfo);
        SetCursorState(true);
        OnTabStateChanged?.Invoke(true);
    }

    private void OnDisable()
    {
        _updateButton.onClick.RemoveListener(UpdatePlayerInfo);
        SetCursorState(false);
        OnTabStateChanged?.Invoke(false);

        _lastPlayerData = null;
    }

    private void Awake()
    {
        InitTab();
    }

    private void InitTab()
    {
        PopulateDropdown(_teamDropdown, typeof(TeamType));
        PopulateDropdown(_reloadDropdown, typeof(ReloadType));
        PopulateDropdown(_roughnessDropdown, typeof(RoughnessType));
    }

    private void PopulateDropdown(TMP_Dropdown dropdown, Type enumType)
    {
        string[] enumNames = Enum.GetNames(enumType);
        dropdown.ClearOptions();
        dropdown.AddOptions(new System.Collections.Generic.List<string>(enumNames));
    }

    private void UpdatePlayerInfo()
    {
        _lastPlayerData.UpdateInfo(_playerNickname.text, (TeamType)_teamDropdown.value, (ReloadType) _reloadDropdown.value, (RoughnessType) _roughnessDropdown.value);
    }

    public void OpenPlayerInfo(PlayerData playerData)
    {
        _playerID.text = playerData.PlayerID.ToString();
        _playerNickname.text = playerData.PlayerNickname;

        _teamDropdown.value = (int) playerData.TeamType;
        _reloadDropdown.value = (int) playerData.ReloadType;
        _roughnessDropdown.value = (int) playerData.RoughnessType;

        _lastPlayerData = playerData;
    }

    private void SetCursorState(bool isOpen)
    {
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
    }

    private void InitPlayersTab()
    {
        NetworkConnectionToClient[] clients = new NetworkConnectionToClient[4];

        for (int i = 0; i < clients.Length; i++)
        {
            if (!_playersData.ContainsKey(clients[i]))
            {
                PlayerData temp = clients[0].identity.GetComponent<PlayerData>();
                _playersData.Add(clients[i], temp);
                CreateButton();
            }
        }
    }

    private void CreateButton()
    {

    }
}