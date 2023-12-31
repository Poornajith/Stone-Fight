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
    public int numberOfWeapons = 3;

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
        // switch weapon via mouse scroll
        if (Input.mouseScrollDelta.y > 0f)
        {
          //  Debug.Log("scroll up");
            if(selectedWeapon < numberOfWeapons)
            {
                selectedWeapon +=1;
            }
            else
            {
                selectedWeapon = 0;
            }
        }
        if (Input.mouseScrollDelta.y < 0f)
        {
           // Debug.Log("scroll down");
            if (selectedWeapon > 0)
            {
                selectedWeapon -=1;
            }
            else
            {
                selectedWeapon = numberOfWeapons - 1;
            }
        }

        //switch weapon via alpha keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0; // melee 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1; // bow            
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2; // throwable
        }

        // activate selected weapon UI image and prefab in hand
        ActivateSelectedWeapon(selectedWeapon);
    }

    public int GetSelectedWeapon()
    {
        return selectedWeapon;
    } 

    void ActivateSelectedWeapon(int selectedWeapon)
    {
        if (selectedWeapon == 0)
        {
            melee.SetActive(true);
            bow.SetActive(false);
            throwable.SetActive(false);

            meleeImg.SetActive(true);
            bowImg.SetActive(false);
            throwableImg.SetActive(false);
        }
        if(selectedWeapon == 1)
        {
            melee.SetActive(false);
            bow.SetActive(true);
            throwable.SetActive(false);

            meleeImg.SetActive(false);
            bowImg.SetActive(true);
            throwableImg.SetActive(false);
        }
        if(selectedWeapon == 2)
        {
            melee.SetActive(false);
            bow.SetActive(false);
            throwable.SetActive(true);

            meleeImg.SetActive(false);
            bowImg.SetActive(false);
            throwableImg.SetActive(true);
        }
    }
}
