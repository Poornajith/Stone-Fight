using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] Slider healthBarSlider;

    GameObject healthBarobject;

    private IEnumerator poisonFieldEffect;
   
    // Start is called before the first frame update
    void Start()
    {
        NetworkObject thisObject = GetComponent<NetworkObject>();

        if (thisObject.HasStateAuthority)
        {
            healthBarobject = GameObject.Find("HealthBar");
            healthBarSlider = healthBarobject.GetComponent<Slider>();
            healthBarSlider.value = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.K))
        {
            currentHealth -= 10;
        }
        healthBarSlider.value = currentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PoissonField>() != null)
        {
            poisonFieldEffect = HealthDecreaseOverTime(2.0f);
            StartCoroutine(poisonFieldEffect);
        }
        if(other.GetComponent<ArrowProjectile>() != null) 
        {
            currentHealth -= 20;
        }
        if (other.GetComponent<MeleWeapon>() != null)
        {
            currentHealth -= 50;
        }
        if (other.GetComponent<MushroomProjectile>() != null)
        {
            currentHealth -= 30;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PoissonField>() != null)
        {
            StopCoroutine(poisonFieldEffect);
        }
    }

    private IEnumerator HealthDecreaseOverTime(float effectTime)
    {
        while(true)
        {
            currentHealth -= 10;
            yield return new WaitForSeconds(effectTime);
        }
    }
}
