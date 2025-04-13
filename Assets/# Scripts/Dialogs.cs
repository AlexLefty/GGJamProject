using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogs : MonoBehaviour
{
    public bool dialog1;
	public bool dialog2;
	public bool dialog3;
	public bool dialog4;
	public bool dialog5;
	public bool dialog6;
	public bool dialog7;
	public bool dialog8;
	public bool dialog9;
	public bool Finishbool;
	
	public float Timer;
	
	public GameObject UIDialogs;
	
	public AudioClip[] musicClips;
	public AudioSource source;
	
	public GameObject Finish;
	
	
    void Awake()
    {
		UIDialogs = GameObject.FindGameObjectWithTag("UIDialogs");
		Finish = GameObject.FindGameObjectWithTag("Finish");
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
		
		if(dialog7)
		{
			dialog7 = false;
			source.Stop();
			source.clip = musicClips[6];
			source.Play();
		}
		
		if(dialog8)
		{
			dialog8 = false;
			source.Stop();
			source.clip = musicClips[7];
			source.Play();
		}
		
		if(dialog9)
		{
			dialog9 = false;
			Finishbool = true;
			source.Stop();
			source.clip = musicClips[8];
			source.Play();
		}
		
		if(Finishbool)
		{
			//Finishbool = false;
			Timer += 1 * Time.deltaTime;
			if(Timer >= 5)
			{
				
				Finish.GetComponent<Image>().color =  Color.Lerp(Finish.GetComponent<Image>().color, new Color(0, 0, 0, 1), Time.deltaTime);
				Invoke("LoadMenu", 10);
			}
		}
		
		
		
		
    }
	
	public void LoadMenu()
		{
			SceneManager.LoadScene(0);
		}
	
	void OnTriggerEnter(Collider coll)
		{
			if(coll.tag == ("Dialog2"))
			{
				dialog2 = true;
				StopCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog1_1());
				StopCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog1_2());
				StopCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog1_3());
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
			if(coll.tag == ("Dialog7"))
			{
				dialog7 = true;
				StartCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog7());
				Destroy(coll);
			}
			if(coll.tag == ("Dialog8"))
			{
				dialog8 = true;
				StartCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog8());
				Destroy(coll);
			}
			if(coll.tag == ("Dialog9"))
			{
				dialog9 = true;
				StartCoroutine(UIDialogs.GetComponent<TextDialog>().CorDialog9());
				Destroy(coll);
			}
            
		}
}
