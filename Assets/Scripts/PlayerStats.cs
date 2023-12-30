using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour
{
    public static PlayerStats Instance;
    [Networked(OnChanged = nameof(UpdatePlayerName))] public NetworkString<_32> PlayerName { get; set; }
    [Networked(OnChanged = nameof(UpdateHealth))] public float Health { get; set; }

    [SerializeField] TextMeshPro playerNameLabel;
    [SerializeField] public Image healthBar;

    private IEnumerator poisonFieldEffect;

    private void Start()
    {
        if (this.HasStateAuthority)
        {
            PlayerName = FusionConnection.instance._playerName;
            Health = 100;
        }
    }

    protected static void UpdatePlayerName(Changed<PlayerStats> changed)
    {
        changed.Behaviour.playerNameLabel.text = changed.Behaviour.PlayerName.ToString();
    }

    protected static void UpdateHealth(Changed<PlayerStats> changed)
    {
        if(changed.Behaviour.Health >= 0)
        {
            changed.Behaviour.healthBar.transform.localScale = new Vector3(changed.Behaviour.Health/100, 1, 1);
        }
    }

    private void Update()
    {
        if(Health <= 0)
        {
            FusionConnection.instance.OnPlayerDead();
            FusionConnection.instance.runner.Shutdown();
            Debug.Log("you died");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PoissonField>() != null)
        {
            poisonFieldEffect = HealthDecreaseOverTime(2.0f);
            StartCoroutine(poisonFieldEffect);
        }
        if (other.GetComponent<ArrowProjectile>() != null)
        {
            Health -= 20;
        }
        if (other.GetComponent<MeleWeapon>() != null)
        {
            Health -= 50;
        }
        /*if (other.GetComponent<MushroomProjectile>() != null)
        {
            Health -= 30;
        }*/
        /*if (other.GetComponent<NetworkObject>() != null)
        {
            Health -= 50;
        }*/
    }

    private IEnumerator HealthDecreaseOverTime(float effectTime)
    {
        while (true)
        {
            Health -= 10;
            yield return new WaitForSeconds(effectTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PoissonField>() != null)
        {
            StopCoroutine(poisonFieldEffect);
        }
    }
}
