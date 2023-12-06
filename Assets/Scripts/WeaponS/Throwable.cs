using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    int selectedWeapon;
    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = GetComponent<WeaponSwap>().selectedWeapon;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
