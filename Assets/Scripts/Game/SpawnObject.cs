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
        GameObject obj = Instantiate(objectToSpawn) as GameObject;
		obj.transform.position = Camera.main.transform.position;
        obj.GetComponent<Rigidbody>().AddForce(transform.forward * forceAmount);
    }
}
