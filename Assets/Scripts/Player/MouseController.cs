using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	public float speed = 33f;
	public float angularSmoothing = 0.5f;
	public float strength = 10.0f;
	private Vector3 clickCoordinates;
	private Vector3 direction;
	private Transform puck;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetMouseButtonDown(0)) {
			clickCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			clickCoordinates.y = 0;
			direction = clickCoordinates - transform.position;
			//Debug.Log ("vector is "+direction);
		}
		if (Input.GetMouseButtonDown(1)) {
			Shoot();
		}
	}

	void FixedUpdate () {
		Rotate (direction);
		//rigidbody.AddForce(transform.forward * speed * Time.fixedDeltaTime);
		//rigidbody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
		rigidbody.MovePosition(transform.position + (transform.forward * speed * Time.fixedDeltaTime));
	}

	void Rotate (Vector3 direction) {
		Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
		//Debug.Log ("targetRotation is "+targetRotation);
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, angularSmoothing * Time.deltaTime);
		rigidbody.MoveRotation(new Quaternion(0, newRotation.y, 0, newRotation.w));
	}

	void Shoot () {
		Debug.Log ("SHOOT");
		puck = transform.Find("Puck");
		//puck = GameObject.FindGameObjectWithTag("Puck").transform;
		if (puck) {
			puck.parent = null;
			puck.rigidbody.isKinematic = false;
			Debug.Log ("force : "+transform.forward*strength);
			Vector3 shootDirection = puck.position - transform.position;
			puck.rigidbody.AddForceAtPosition(shootDirection.normalized * strength, transform.position, ForceMode.VelocityChange);
		}
	}

}
