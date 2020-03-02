﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightingManager))]
public class CarController : MonoBehaviour
{
	public InputManager im;

	public LightingManager lm;

	public List<WheelCollider> throttleWheels;
	public List<GameObject> steeringWheels;
	public List<GameObject> meshes;

	public float strengthCoefficient = 20000f;
	public float maxTurn = 20f;
	public float brakeStrength;

	public Transform CM;
	public Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{
		im = GetComponent<InputManager>();
		rb = GetComponent<Rigidbody>();

		if (CM)
		{
			rb.centerOfMass = CM.localPosition;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (im.lightToggle)
		{
			lm.ToggleHeadlights();
		}
	}

	void FixedUpdate()
	{
		foreach (WheelCollider wheel in throttleWheels)
		{
			
			if (im.brake)
			{
				wheel.motorTorque = 0f;
				wheel.brakeTorque = brakeStrength * Time.deltaTime;
			}
			else
			{
				wheel.motorTorque = strengthCoefficient * Time.deltaTime * im.throttle;
				wheel.brakeTorque = 0f;
			}
		}

		foreach (GameObject wheel in steeringWheels)
		{
			wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
			wheel.transform.localEulerAngles = new Vector3(0f, im.steer * maxTurn, 0f);
		}
		foreach (GameObject mesh in meshes)
		{
			mesh.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.3075f), 0f, 0f);
		}
	}
}
