using UnityEngine;


public class TeamZone : MonoBehaviour
{
    public TeamType _teamZone;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {

        }    
    }
}