using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float mouseX;
	private float mouseY;
	
	[Header ("Чувствительность мыши")]
	public float sensitivityMouse = 50;
	
	public Transform Player;
	
    void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
		mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;
		
		Player.Rotate(mouseX * new Vector3(0, 1, 0));
		transform.Rotate(mouseY * new Vector3(-1, 0, 0));
    }
}
