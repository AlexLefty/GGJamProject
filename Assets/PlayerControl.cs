using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Скорость перемещения персонажа")]
    public float speed = 7f;
	
	[Header("Скорость бега персонажа")]
    public float runspeed = 14f;

    [Header("Сила прыжка")]
    public float jumpPower = 200f;

    [Header("Мы на земле?")]
    public bool ground;

    public Rigidbody rb;

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
			if(Input.GetKey(KeyCode.LeftShift))
			{
				transform.localPosition += transform.forward * runspeed * Time.deltaTime;
			}else
			{
				transform.localPosition += transform.forward * speed * Time.deltaTime;
			}            
        }

        if (Input.GetKey(KeyCode.S))
        {
			if(Input.GetKey(KeyCode.LeftShift))
			{
				transform.localPosition += -transform.forward * runspeed * Time.deltaTime;
			}else
			{
				transform.localPosition += -transform.forward * speed * Time.deltaTime;
			}           
        }

        if (Input.GetKey(KeyCode.A))
        {
			if(Input.GetKey(KeyCode.LeftShift))
			{
				transform.localPosition += -transform.right * runspeed * Time.deltaTime;
			}else
			{
				transform.localPosition += -transform.right * speed * Time.deltaTime;
			} 
        }

        if (Input.GetKey(KeyCode.D))
        {
			if(Input.GetKey(KeyCode.LeftShift))
			{
				transform.localPosition += transform.right * runspeed * Time.deltaTime;
			}else
			{
				transform.localPosition += transform.right * speed * Time.deltaTime;
			}            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ground == true)
            {
                rb.AddForce(transform.up * jumpPower);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = false;
        }
    }
}