using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(PlayerController))]

/// <summary>
/// Handles Goalkeeper movement.
/// </summary>
public class GoalMove : MonoBehaviour
{
		/// <summary>
		/// Movement and rotation smoothness.
		/// </summary>
		public float smoothness = 3.0f;
		private Vector2 direction;
		private Vector2 trajectory;
		/// <summary>
		/// Speed of orientation change.
		/// </summary>
		public float rotationSmoothing = 8f;
		/// <summary>
		/// The center of the goal's area.
		/// </summary>
		public Transform center;
		/// <summary>
		/// Max distance from the center the goalkeeper is allowed to go.
		/// </summary>
		public float maxDistance = 3.0f;

		private PlayerController myController;

		void Start ()
		{
				myController = GetComponent<PlayerController> ();
		}

		void Update ()
		{
				if (myController.HasPuck ()) {
						// goal has the puck
						float x = XCI.GetAxis (XboxAxis.LeftStickX, myController.GetID ());
						float y = XCI.GetAxis (XboxAxis.LeftStickY, myController.GetID ());
						direction = (new Vector2 (x, y)).normalized;
				}
		}

		void FixedUpdate ()
		{
				if (myController.HasPuck ()) {
						if (!CheckLock () && !myController.IsCharging ()) {
								rigidbody2D.MovePosition (Vector2.Lerp (rigidbody2D.position, (Vector2)center.position + direction * maxDistance, smoothness * Time.fixedDeltaTime));
								Rotate (transform.right);
						} else {
								Rotate (direction);
						}
				}
		}

		bool CheckLock ()
		{
				if (XCI.GetAxis (XboxAxis.LeftTrigger, myController.GetID ()) != 0 || XCI.GetButton (XboxButton.LeftBumper, myController.GetID ())) {
						return true;
				} else {
						return false;
				}
		}

		/// <summary>
		/// Rotate smoothly to the specified direction.
		/// </summary>
		/// <param name="direction">Direction to rotate smoothly to.</param>
		void Rotate (Vector2 direction)
		{
				if (direction == Vector2.zero)
						return;
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


}
