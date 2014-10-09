using UnityEngine;
using System.Collections;

/// <summary>
/// Stare makes a Transform constantly look at an other Transform.
/// </summary>
public class Stare : MonoBehaviour {

	/// <summary>
	/// Transform to stare.
	/// </summary>
	public Transform target;
	/// <summary>
	/// Rotation smoothness.
	/// </summary>
	public float smoothness = 5f;
	
	// Use this for initialization
	void Start () {
		//target = GameObject.FindGameObjectWithTag("Puck").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Rotate (target.position - transform.position);
		// Debug.Log ("staring at ");
		// Debug.Log (""+target);
		//transform.Rotate(0, 0, Vector2.Angle(transform.position, target.position));
	}

	void Rotate (Vector2 direction)
	{
		if (direction == Vector2.zero)
			return;
		float targetAngle = Vector2.Angle (Vector2.right, direction);
		if (direction.y < 0f)
			targetAngle *= -1;
		//rigidbody2D.MoveRotation (targetAngle);
		transform.eulerAngles = new Vector3(0, 0, targetAngle);
	}

}
