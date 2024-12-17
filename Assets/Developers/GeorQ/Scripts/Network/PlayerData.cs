using Mirror;


public class PlayerData : NetworkBehaviour
{
    [SyncVar]
    public int PlayerID;

    [SyncVar]
    public string PlayerNickname = "";

    [SyncVar]
    public TeamType TeamType = TeamType.None;
    
    [SyncVar]
    public ReloadType ReloadType = ReloadType.None;

    [SyncVar]
    public RoughnessType RoughnessType = RoughnessType.BloodyHell;


    public void UpdateInfo(string nickName, TeamType teamType, ReloadType reloadType, RoughnessType roughnessType)
    {
        PlayerNickname = nickName;
        TeamType = teamType;
        ReloadType = reloadType;
        RoughnessType = roughnessType;
    }
}