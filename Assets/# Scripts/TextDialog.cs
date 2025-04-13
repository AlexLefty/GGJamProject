using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDialog : MonoBehaviour
{
	public TextMeshProUGUI Dialog1_1;
	public TextMeshProUGUI Dialog1_2;
	public TextMeshProUGUI Dialog1_3;
	public TextMeshProUGUI Dialog2;
	public TextMeshProUGUI Dialog3;
	public TextMeshProUGUI Dialog4;
	public TextMeshProUGUI Dialog5;
	public TextMeshProUGUI Dialog6;
	public float typingSpeed = 0.1f;
	
	private string fullTextDialog1_1;
	private string fullTextDialog1_2;
	private string fullTextDialog1_3;
	private string fullTextDialog2;
	private string fullTextDialog3;
	private string fullTextDialog4;
	private string fullTextDialog5;
	private string fullTextDialog6;

	private bool isCoroutineStopped = false;
	
    void Start()
    {	
        fullTextDialog1_1 = Dialog1_1.text;
		Dialog1_1.text = "";
		fullTextDialog1_2 = Dialog1_2.text;
		Dialog1_2.text = "";
		fullTextDialog1_3 = Dialog1_3.text;
		Dialog1_3.text = "";
		fullTextDialog2 = Dialog2.text;
		Dialog2.text = "";
		fullTextDialog3 = Dialog3.text;
		Dialog3.text = "";
		fullTextDialog4 = Dialog4.text;
		Dialog4.text = "";
		fullTextDialog5 = Dialog5.text;
		Dialog5.text = "";
		fullTextDialog6 = Dialog6.text;
		Dialog6.text = "";
		StartCoroutine(CorDialog1_1());
	}
	
	private IEnumerator CorDialog1_1()
	{
		for(int i = 0; i < fullTextDialog1_1.Length && !isCoroutineStopped; i++)
		{
			Dialog1_1.text += fullTextDialog1_1[i];
			yield return new WaitForSeconds(typingSpeed);
		}
		Dialog1_1.enabled = false;
		StartCoroutine(CorDialog1_2());
	}
	
	private IEnumerator CorDialog1_2()
	{
		for(int i = 0; i < fullTextDialog1_2.Length && !isCoroutineStopped; i++)
		{
			Dialog1_2.text += fullTextDialog1_2[i];
			yield return new WaitForSeconds(typingSpeed);
		}
		Dialog1_2.enabled = false;
		StartCoroutine(CorDialog1_3());
	}
	
	private IEnumerator CorDialog1_3()
	{
		for(int i = 0; i < fullTextDialog1_3.Length && !isCoroutineStopped; i++)
		{
			Dialog1_3.text += fullTextDialog1_3[i];
			yield return new WaitForSeconds(typingSpeed);
		}
		Dialog1_3.enabled = false;
		
		//StopCoroutine(CorDialog1_3());
	}
	
	public IEnumerator CorDialog2()
	{
		for(int i = 0; i < fullTextDialog2.Length && !isCoroutineStopped; i++)
		{
			Dialog2.text += fullTextDialog2[i];
			yield return new WaitForSeconds(typingSpeed);
		}
		Dialog2.enabled = false;
		
	}
	
	public IEnumerator CorDialog3()
	{
		for(int i = 0; i < fullTextDialog3.Length && !isCoroutineStopped; i++)
		{
			Dialog3.text += fullTextDialog3[i];
			yield return new WaitForSeconds(typingSpeed);
		}
		Dialog3.enabled = false;
		
	}
	
	public IEnumerator CorDialog4()
	{
		for(int i = 0; i < fullTextDialog4.Length && !isCoroutineStopped; i++)
		{
			Dialog4.text += fullTextDialog4[i];
			yield return new WaitForSeconds(typingSpeed);
		}
		Dialog4.enabled = false;
		
	}
	
	public IEnumerator CorDialog5()
	{
		for(int i = 0; i < fullTextDialog5.Length && !isCoroutineStopped; i++)
		{
			Dialog5.text += fullTextDialog5[i];
			yield return new WaitForSeconds(typingSpeed);
		}
		Dialog5.enabled = false;
		
	}
	
	public IEnumerator CorDialog6()
	{
		for(int i = 0; i < fullTextDialog6.Length && !isCoroutineStopped; i++)
		{
			Dialog6.text += fullTextDialog6[i];
			yield return new WaitForSeconds(typingSpeed);
		}
		Dialog6.enabled = false;
		
	}
	

    // Update is called once per frame
    void Update()
    {
		//Player = GameObject.FindGameObjectWithTag("Player");
		
		/*if(Player.GetComponent<Dialogs>().dialog2 = true)
		{
			Debug.Log("Da");
			//Player.GetComponent<Dialogs>().dialogUI2 = false;
			StartCoroutine(CorDialog2());
		}*/
    }
}
