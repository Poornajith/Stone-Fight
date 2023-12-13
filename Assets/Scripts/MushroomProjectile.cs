using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitFloor;
    //[SerializeField] private Transform vfxHitTarget;

    private Rigidbody mushroomRigidBody;

    private void Awake()
    {
        mushroomRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 10f;
        mushroomRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("hit");
        if (other.GetComponent<HitTarget>() != null)
        {
            // hit target
            //Instantiate(vfxHitTarget, transform.position, Quaternion.identity);
        }
        if (other.GetComponent<HitFloor>() != null)
        {
            // hit floor
            Instantiate(vfxHitFloor, transform.position, Quaternion.identity); 
            Destroy(gameObject);
            Debug.Log("hit on floor");
        }
        else
        {
            //hit something else
            Debug.Log("hit on something");
        }

    }
}
