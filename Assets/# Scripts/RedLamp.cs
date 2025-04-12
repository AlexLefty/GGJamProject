using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLamp : MonoBehaviour
{
    public Light myLight;
	public float interval = 1;
	float timer;
	public bool OnOff;
	
	//private Renderer renderer;
	
	void Awake()
    {
        //renderer = GetComponent<Renderer>();
    }

	void Update() {
		timer += Time.deltaTime;
		if (timer > interval) {
			//renderer.material.DisableKeyword("_EMISSION");
			OnOff = !OnOff;
			timer -= interval;
		}
		
		if(OnOff)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = true;
			myLight.enabled = !myLight.enabled;
		}else
		{
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			myLight.enabled = !myLight.enabled;
		}
	}
}
