using UnityEngine;
using System.Collections;

public class CroshAir : MonoBehaviour {
	
	public Texture2D pricel;
	
	public void OnGUI ()
	{
		GUI.DrawTexture (new Rect (Screen.width / 2, Screen.height / 2, 10, 10), pricel);
	}
	
}