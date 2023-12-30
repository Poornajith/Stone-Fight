using Cinemachine;
using Fusion;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerUI : MonoBehaviour
{
    private void Start()
    {
        NetworkObject thisObject = GetComponent<NetworkObject>();

        if (thisObject.HasStateAuthority)
        {
            GetComponent<WeaponSwap>().enabled = true;          
        }
    }
}
