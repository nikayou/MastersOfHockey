using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(PlayerController))]

public class PlayerMove : MonoBehaviour
{
		/// <summary>
		/// The minimum speed at which the player goes.
		/// </summary>
		public float minSpeed = 3f;
		/// <summary>
		/// The maximum speed at which the player can go (attack not included).
		/// </summary>
		public float maxSpeed = 10f;
		/// <summary>
		/// Acceleration and deceleration.
		/// </summary>
		public float speedSmoothing = 1.5f; 
		/// <summary>
		/// Speed of orientation change.
		/// </summary>
		public float rotationSmoothing = 8f;
		/// <summary>
		/// Time before starting to decelerate when stopped accelerating, in seconds.
		/// </summary>
		public float timeBeforeDeceleration = 0.5f;
		private float decelerationTimer;
	
		/// <summary>
		/// Is the player accelerating?
		/// </summary>
		public bool accelerating = true;
	
		/// <summary>
		/// Is the player drifting?
		/// When drifting, speed decreases faster.
		/// </summary>
		public bool drifting = false;
		/// <summary>
		/// Speed loss factor while drifting.
		/// </summary>
		public float driftLoss = 3.0f;
		private float driftFactor = 1f;
		/// <summary>
		/// Tells if the player moves in the direction he faces, or the previous direction.
		/// Direction always takes the stick orientation, but trajectory takes it only if 
		/// "moveForward" is true.
		/// </summary>
		private bool moveForward;
		/// <summary>
		/// Direction the player is facing.
		/// </summary>
		private Vector2 direction;
		/// <summary>
		/// Trajectory of the player.
		/// </summary>
		private Vector2 trajectory;
		private Vector2 forward;

		private PlayerController myController;

		void Awake ()
		{
				myController = GetComponent<PlayerController> ();
		}

		void Start ()
		{
				myController.SetSpeed(minSpeed);
				direction = forward;
				trajectory = forward;
		}

		void Update ()
		{
				forward = transform.right;
				decelerationTimer -= Time.deltaTime;
				if (myController.CanInput ()) {
						accelerating = false;
						// getting direction for movement only if not charging
						if (!myController.IsStun ()) {
								float x = XCI.GetAxis (XboxAxis.LeftStickX, myController.GetID ());
								float y = XCI.GetAxis (XboxAxis.LeftStickY, myController.GetID ());
								if (x != 0 || y != 0) {
										direction = (new Vector2 (x, y)).normalized;
										// changing trajectory and accelerating only if not locking or charging
										moveForward = !CheckDrift() && !myController.IsCharging ();
										if (moveForward) {
												trajectory = direction;
												accelerating = true;
										} 
								}
						}
						CheckSpeed ();
				} else {
						direction = forward;
						trajectory = forward;
				}
		}

		void FixedUpdate ()
		{
				// TODO: when an close opponent has the puck, look at it
				rigidbody2D.velocity = Vector2.zero;
				rigidbody2D.angularVelocity = 0f;
				Rotate (direction);
				if (moveForward) {
						rigidbody2D.MovePosition (rigidbody2D.position + forward * myController.GetSpeed() * Time.fixedDeltaTime);
				} else {
						rigidbody2D.MovePosition (rigidbody2D.position + (trajectory * myController.GetSpeed() * Time.fixedDeltaTime));
				}
				
		}


		/// <summary>
		/// Checks if the player is locking. Sets the animator accordingly. 
		/// </summary>
		/// <returns><c>true</c>, if locking, <c>false</c> otherwise.</returns>
		bool CheckDrift ()
		{
				// TODO: bugs under linux
				// Debug.Log ("drifting ? " + XCI.GetAxis (XboxAxis.LeftTrigger, myController.GetID ()) + "/" + XCI.GetButton (XboxButton.LeftBumper, myController.GetID ()));
				// return false;
				driftFactor = 1f;
				if (XCI.GetAxis (XboxAxis.LeftTrigger, myController.GetID ()) != 0 || XCI.GetButton (XboxButton.LeftBumper, myController.GetID ())) {
						drifting = true;
						driftFactor = driftLoss;
						return true;
				} else {
						drifting = false;
						return false;
				}
		}

		/// <summary>
		/// Computes the speed with "Accelerate" and "Decelerate". Sets the animator accordingly.
		/// </summary>
		void CheckSpeed ()
		{
				// always decelerating, unless player is moving	
				if (accelerating) {
						decelerationTimer = timeBeforeDeceleration;
						Accelerate ();
				} else if (drifting || decelerationTimer <= 0) {
						Decelerate ();
				}
		}

		/// <summary>
		/// Changes the speed smoothly to the maximum speed.
		/// </summary>
		void Accelerate ()
		{
				myController.SetSpeed(Mathf.Lerp (myController.GetSpeed(), maxSpeed, speedSmoothing * Time.deltaTime));
		}

		/// <summary>
		/// Changes the speed smoothly to the minimum speed.
		/// </summary>
		void Decelerate ()
		{
				myController.SetSpeed(Mathf.Lerp (myController.GetSpeed(), minSpeed, speedSmoothing * driftFactor * Time.deltaTime));
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
