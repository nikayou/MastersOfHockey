using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class SimpleMover : MonoBehaviour
{

		private Vector2 direction;
		private Vector2 trajectory;
		public int playerID = 1;
		public float speed = 10.0f;
		public float rotationSmoothing = 15f;


		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				Vector2 axis = GetAxis ();
				direction = axis;
		}

		void FixedUpdate ()
		{
				if (direction != Vector2.zero)
						Rotate ();
				MoveForward ();
		}

		void Rotate ()
		{
				float targetAngle = Vector2.Angle (Vector2.right, direction);
				if (direction.y < 0f)
						targetAngle *= -1;
				if (Mathf.Abs (rigidbody2D.rotation - targetAngle) > 180f) {
						if (rigidbody2D.rotation > 0f) {
								rigidbody2D.rotation -= 360f;
						} else {
								rigidbody2D.rotation += 360f;
						}
				}
				float targetRotation = Mathf.Lerp (rigidbody2D.rotation, targetAngle, rotationSmoothing * Time.fixedDeltaTime);
				rigidbody2D.MoveRotation (targetRotation);
		}

		void MoveForward ()
		{
				Vector2 forward = transform.right;
				rigidbody2D.MovePosition (rigidbody2D.position + forward * speed * Time.fixedDeltaTime);
		}

		Vector2 GetAxis ()
		{
				float x = XCI.GetAxis (XboxAxis.LeftStickX, playerID);
				float y = XCI.GetAxis (XboxAxis.LeftStickY, playerID);
				return new Vector2 (x, y).normalized;
		}



}
