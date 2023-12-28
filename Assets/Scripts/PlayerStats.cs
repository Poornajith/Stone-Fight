using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour
{
    [Networked(OnChanged = nameof(UpdatePlayerName))] public NetworkString<_32> PlayerName { get; set; }
    [Networked(OnChanged = nameof(UpdateHealth))] public float Health { get; set; }

    [SerializeField] TextMeshPro playerNameLabel;
    [SerializeField] public Image healthBar;

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
        changed.Behaviour.healthBar.transform.localScale = new Vector3(changed.Behaviour.Health/100, 1, 1);
    }

    private void Update()
    {
        if(Health <= 0)
        {
            FusionConnection.instance.OnPlayerDead();
            Debug.Log("you died");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Health -= 10;
        }
    }
}
