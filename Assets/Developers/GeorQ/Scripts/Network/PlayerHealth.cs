using Mirror;
using System;
using UnityEngine;


public class PlayerHealth : NetworkBehaviour
{
    public event Action OnPlayerDied;
    public event Action OnPlayerRespawn;

    [SyncVar(hook = nameof(OnHealthChanged))]
    public int Health;

    public int MaxHealth = 100;
    public bool IsAlive;


    public override void OnStartServer()
    {
        Health = MaxHealth;
        IsAlive = true;
    }

    private void OnHealthChanged(int oldHealth, int newHealth)
    {

    }

    public void DealDamage(int damageAmmount)
    {
        CmdDealDamage(damageAmmount);
    }

    [Command(requiresAuthority = false)]
    private void CmdDealDamage(int damageAmmount)
    {
        if (damageAmmount < 0)
        {
            Debug.LogWarning("Damage can not be neggative!");
        }

        Health = Mathf.Clamp(Health - damageAmmount, 0, MaxHealth);

        if (Health <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        IsAlive = false;
        OnPlayerDied?.Invoke();
    }
}