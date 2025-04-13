using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogs : MonoBehaviour
{
    public bool dialog1;
	public bool dialog2;
	public bool dialog3;
	public bool dialog4;
	public bool dialog5;
	public bool dialog6;
	
	public GameObject UIDialogs;
	
	public AudioClip[] musicClips;
	public AudioSource source;
	
	
    void Awake()
    {
		UIDialogs = GameObject.FindGameObjectWithTag("UIDialogs");
		source = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        dialog1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(dialog1)
		{
			dialog1 = false;
			source.clip = musicClips[0];
			source.Play();
		}
		
		if(dialog2)
		{
			dialog2 = false;
			source.Stop();
			source.clip = musicClips[1];
			source.Play();
		}
		
		if(dialog3)
		{
			dialog3 = false;
			source.Stop();
			source.clip = musicClips[2];
			source.Play();
		}
		
		if(dialog4)
		{
			dialog4 = false;
			source.Stop();
			source.clip = musicClips[3];
			source.Play();
		}
		
		if(dialog5)
		{
			dialog5 = false;
			source.Stop();
			source.clip = musicClips[4];
			source.Play();
		}
		
		if(dialog6)
		{
			dialog6 = false;
			source.Stop();
			source.clip = musicClips[5];
			source.Play();
		}
    }
	
	void OnTriggerEnter(Collider coll)
		{
			if(coll.tag == ("Dialog2"))
			{
				dialog2 = true;
				StartCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog2());
				Destroy(coll);
			}
			if(coll.tag == ("Dialog3"))
			{
				dialog3 = true;
				StartCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog3());
				Destroy(coll);
			}
			if(coll.tag == ("Dialog4"))
			{
				dialog4 = true;
				StartCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog4());
				Destroy(coll);
			}
			if(coll.tag == ("Dialog5"))
			{
				dialog5 = true;
				StartCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog5());
				Destroy(coll);
			}
			if(coll.tag == ("Dialog6"))
			{
				dialog6 = true;
				StartCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog6());
				Destroy(coll);
			}
            
		}
}
