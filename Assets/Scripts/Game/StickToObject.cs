using UnityEngine;
using System.Collections;

public class StickToObject : MonoBehaviour 
{
    private Collider m_collider = null;
    private Rigidbody m_rigidbody = null;

    void Start()
    {
        m_collider = GetComponent<Collider>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

	void OnCollisionEnter(Collision collision)
    {
        transform.parent = collision.transform;

        if (m_rigidbody != null)
		{
            Destroy(m_rigidbody);
		}

        Destroy(m_collider);
        Destroy(this);
    }
}
