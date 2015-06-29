using UnityEngine;
using System.Collections;

public class StickToObject : MonoBehaviour 
{
    private Collider m_collider = null;
    private Rigidbody m_rigidbody = null;

	private Timer m_timer = new Timer();

    void Start()
    {
        m_collider = GetComponent<Collider>();
        m_rigidbody = GetComponent<Rigidbody>();

		m_timer.Start(5.0f);
    }

	void Update()
	{
		if (m_timer.Finished())
		{
			Destroy(this.gameObject);
		}
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
