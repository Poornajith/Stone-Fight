using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoissonField : MonoBehaviour
{
    [SerializeField] float lifeTime = 15f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
