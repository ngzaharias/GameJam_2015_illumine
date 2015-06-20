using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSpawner : MonoBehaviour 
{
	[SerializeField]
	private uint m_lightsMax = 3;
    [SerializeField]
	private FadeLight m_lightToSpawn = null;
    [SerializeField]
    private float m_forceAmount = 100.0f;

	void Update () 
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{ 
            Spawn();
		}
	}

    void Spawn()
    {
		// spawn a light
		FadeLight light = Instantiate<FadeLight>(m_lightToSpawn);
		light.transform.position = Camera.main.transform.position;
        light.GetComponent<Rigidbody>().AddForce(transform.forward * m_forceAmount);
		light.Fade(4.0f, 1.0f);
		LightManager.Instance.RegisterPointLight(light);

		// remove a light
		List<FadeLight> lights = LightManager.Instance.PointLights;
		while (lights.Count > m_lightsMax)
		{
			if (lights[0] != null)
			{
				lights[0].Fade(0.0f, 1.0f);
				Destroy(lights[0].gameObject, 1.0f);
			}
			lights.RemoveAt(0);
		}
    }
}
