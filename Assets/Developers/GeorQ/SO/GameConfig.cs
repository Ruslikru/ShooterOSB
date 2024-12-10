using UnityEngine;


[CreateAssetMenu(menuName = "Config/New game config", fileName = "NewGameConfig")]
public class GameConfig : ScriptableObject
{
    public bool IsAdminBuild = false;
}