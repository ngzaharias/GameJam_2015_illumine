using UnityEngine;
using System.Collections;

public class StickToObject : MonoBehaviour 
{
    new private Collider collider = null;
    new private Rigidbody rigidbody = null;

    void Start()
    {
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

	void OnCollisionEnter(Collision collision)
    {
        transform.parent = collision.transform;

        if (rigidbody != null)
            Destroy(rigidbody);

        Destroy(collider);
        Destroy(this);
    }
}
