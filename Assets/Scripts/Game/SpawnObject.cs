using UnityEngine;
using System.Collections;

public class SpawnObject : MonoBehaviour 
{
    [SerializeField]
    protected GameObject objectToSpawn = null;
    [SerializeField]
    protected float forceAmount = 100.0f;

	void Update () 
    {
	    if (Input.GetKeyDown(KeyCode.Space))
            Spawn();
	}

    void Spawn()
    {
        GameObject obj = Instantiate(objectToSpawn, transform.position, transform.rotation) as GameObject;
        obj.GetComponent<Rigidbody>().AddForce(transform.forward * forceAmount);
    }
}
