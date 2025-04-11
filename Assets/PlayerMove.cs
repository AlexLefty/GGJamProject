using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //public VariableJoystick joystick;
	public float speedMove;
	public float sensivity;
	public float jumpForce;
	public bool ground;
	
	public float airGravity;
	public float groundGravity;
	
	//public AudioClip _JumpSound;
	//public AudioClip _MoveSound;
	//public AudioClip _BegSound;
	public bool Beg;
	
	public Vector2 clampAngle;
	
	//public FixedTouchField _FixedTouchField;
	//public Touch _Touch;
	
	//public GameObject RyukzakPanel;
	
	//private bool Ryukzak;
	
	//private Transform _transform;
	
	private Vector2 _angle;
	private Vector3 _dir;
	
	private float _mouseX;
	private float _mouseY;
	
	private float _vertical;
	private float _horizontal;
	
	private float _gravity;
	
	public CharacterController _controller;
	public Vector3 controller;
	//[SerializeField] private AudioClip[] m_FootstepSounds;
	//public AudioSource _source;	
	
    void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
		if(_controller.isGrounded)
		{
			_vertical = Input.GetAxis("Vertical");
			_horizontal = Input.GetAxis("Horizontal");

			
			_gravity = groundGravity;
			
			_dir = transform.TransformDirection(_horizontal, 0f, _vertical);
			_dir *= speedMove;
			
			/*if(_vertical != 0 || _horizontal != 0 && !Beg)
			{
				if(!_source.isPlaying)
				{
					_source.clip = _MoveSound;
					_source.Play();
				}
			}*/
			
			/*if(Input.GetButtonDown("Jump"))
			{
				_dir.y = jump;

					_source.clip = _JumpSound;
					_source.Play();
				
			}*/
		}else _gravity = airGravity;

		_dir.y -= _gravity * Time.deltaTime;
		_controller.Move(_dir * Time.deltaTime);
		
		if(Input.GetKey("left shift") && Input.GetAxis("Vertical") != 0 )
			{
				Beg = true;
				speedMove = 10f;
				/*if(!_source.isPlaying && Beg)
					{
						_source.clip = _BegSound;
						_source.Play();
					}*/
				
			}else
			{
				Beg = false;
				speedMove = 5f;
			}
			
			 if (Input.GetKeyDown(KeyCode.Space))
				{
					if (ground == true)
					{
						_dir.y = jumpForce;
						_gravity = airGravity;
					}
				}else 
				{
					_gravity = groundGravity;
				}
    }
	
	void LateUpdate()
    {
		//_Touch.LockAxis = _FixedTouchField.TouchDist;
		_mouseX = Input.GetAxis("Mouse X");
		_mouseY = Input.GetAxis("Mouse Y");
		
		_angle.x -= _mouseY * sensivity;
		_angle.y += _mouseX * sensivity;
		
		_angle.x = Mathf.Clamp(_angle.x, -clampAngle.x, clampAngle.y);
		
		Quaternion rot = Quaternion.Euler(_angle.x, _angle.y, 0f);
		transform.rotation = rot;
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
	
	/*public void OnOffRyukzak()
	{
		Ryukzak = !Ryukzak;
		
		if(Ryukzak)
		{
			RyukzakPanel.SetActive(true);
		}else
		{
			RyukzakPanel.SetActive(false);
		}
	}*/

}
