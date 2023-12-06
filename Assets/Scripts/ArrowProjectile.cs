using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    //[SerializeField] private Transform vfxHitArrow;
    //[SerializeField] private Transform vfxHitTarget;
    [SerializeField] private float speed = 50f;

    private Rigidbody arrowRigidBody;

    private void Awake()
    {
        arrowRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        arrowRigidBody.velocity = transform.forward * speed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HitTarget>() != null)
        {
            // hit target n blood effect
            //  Instantiate(vfxHitTarget, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            //hit something else
            //Instantiate(vfxHitArrow, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
