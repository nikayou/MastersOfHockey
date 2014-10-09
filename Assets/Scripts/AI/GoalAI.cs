using UnityEngine;
using System.Collections;


/// <summary>
/// Basically, the goalkeeper places between the puck and the goal. 
/// Steps : 
/// - draw a line between the puck and the center of the goal (or limit)
/// - do something with the distance
/// - compute the target point : collision point of the limit (see distance) and the ray
/// - move toward this point
/// </summary>
public class GoalAI : MonoBehaviour
{
		/// <summary>
		/// The center of the goal's area.
		/// </summary>
		public Transform center;
		/// <summary>
		/// Distance at which the goalkeeper begins to move.
		/// </summary>
		public float ceilingDistance = 12.0f; // this corresponds to the zone line
		/// <summary>
		/// Movement smoothness.
		/// </summary>
		public float smoothness = 1.5f;
		/// <summary>
		/// The maximum distance at which the goalkeeper is allowed to move away.
		/// </summary>
		public float maxDistance = 2.0f;

		private Vector2 destination;
		//private GameObject gameController;
		private Transform puck;
	
		private PlayerController myController;

		void Awake ()
		{
				destination = transform.position;
		}

		void Start ()
		{
				myController = GetComponent<PlayerController> ();
				GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
				puck = gameController.GetComponent<PuckHolder> ().GetPuck ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				// TODO: make the goal go catch a close puck is it is free
				float distance = ComputeDistance (center, puck);
				if (distance < ceilingDistance) {
						Vector2 ray = puck.position - center.position;
						float angle = Vector2.Angle (Vector2.right, ray);
						if (puck.position.y < center.position.y)
								angle *= -1;
						destination.x = center.position.x + Mathf.Cos (Mathf.Deg2Rad * angle) * maxDistance;
						destination.y = center.position.y + Mathf.Sin (Mathf.Deg2Rad * angle) * maxDistance;
						transform.position = Vector2.Lerp (transform.position, destination, smoothness * Time.deltaTime);
				} else {
						transform.position = Vector2.Lerp (transform.position, center.position, smoothness * Time.deltaTime);
				}
				// looking the puck if don't have it
				if (!myController.HasPuck ()) {		
						Rotate (puck.position - transform.position);
				}
		}
	
		float ComputeDistance (Transform a, Transform b)
		{
				return Vector2.Distance ((Vector2)a.position, (Vector2)b.position);
		} 

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
				float targetRotation = Mathf.Lerp (rigidbody2D.rotation, targetAngle, smoothness * Time.fixedDeltaTime);
				rigidbody2D.MoveRotation (targetRotation);
		}

}
