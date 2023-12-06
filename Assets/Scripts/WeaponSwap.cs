using Cinemachine;
using Fusion;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwap : MonoBehaviour
{
    public int selectedWeapon = 0; // 0 - melee, 1 - Bow, 2 - Throwable

    [SerializeField] public GameObject melee;
    [SerializeField] public GameObject bow;
    [SerializeField] public GameObject throwable;    
    
    GameObject meleeImg;
    GameObject bowImg;
    GameObject throwableImg;

    // Start is called before the first frame update
    void Start()
    {
        NetworkObject thisObject = GetComponent<NetworkObject>();

        if (thisObject.HasStateAuthority)
        {
            meleeImg = GameObject.Find("Melee");
            bowImg = GameObject.Find("Bow");
            throwableImg = GameObject.Find("Throwable");
        }

        selectedWeapon = 0;

        melee.SetActive(true);
        bow.SetActive(false);
        throwable.SetActive(false);
        
        meleeImg.SetActive(true);
        bowImg.SetActive(false);
        throwableImg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0; // melee 

            melee.SetActive(true);
            bow.SetActive(false);
            throwable.SetActive(false);

            meleeImg.SetActive(true);
            bowImg.SetActive(false);
            throwableImg.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1; // bow 

            melee.SetActive(false);
            bow.SetActive(true);
            throwable.SetActive(false);

            meleeImg.SetActive(false);
            bowImg.SetActive(true);
            throwableImg.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2; // throwable

            melee.SetActive(false);
            bow.SetActive(false);
            throwable.SetActive(true);
            
            meleeImg.SetActive(false);
            bowImg.SetActive(false);
            throwableImg.SetActive(true);
        }
    }

    public int GetSelectedWeapon()
    {
        return selectedWeapon;
    } 
}
