using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public float throttle;
	public float steer;
	public bool brake;
	public bool lightToggle;

	void Update()
	{
		throttle = Input.GetAxis("Vertical");
		steer = Input.GetAxis("Horizontal");

		brake = Input.GetKey(KeyCode.Space);

		lightToggle = Input.GetKeyDown(KeyCode.L);
	}
}
