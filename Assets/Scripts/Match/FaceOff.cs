using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(MatchTimer))]

/// <summary>
/// FaceOff manages the face-off event : placing players, puck, and whistling.
/// </summary>
public class FaceOff : MonoBehaviour
{

		/// <summary>
		/// Time between beginning of face-off and it's end.
		/// </summary>
		public float waitingTime = 3.0f;
		/// <summary>
		/// Sound played at face-off beginning and end.
		/// </summary>
		public AudioClip whistleSound;

		private Transform puck;
		/// <summary>
		/// Reference to the player 1.
		/// </summary>
		public Transform player1;
		/// <summary>
		/// Reference to the player 2.
		/// </summary>
		public Transform player2;
		/// <summary>
		/// The distance from center where players should be placed.
		/// </summary>
		public float distanceToCenter = 4.5f;

		private PlayerController p1Controller;
		private PlayerController p2Controller;
		private Vector2 distanceVector;
		private MatchTimer matchTimer;

		void Start ()
		{
				GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
				puck = gameController.GetComponent<PuckHolder> ().GetPuck ();
				p1Controller = player1.gameObject.GetComponent<PlayerController> ();
				p2Controller = player2.gameObject.GetComponent<PlayerController> ();
				distanceVector = new Vector2 (distanceToCenter, 0);
				matchTimer = GetComponent<MatchTimer> ();

		}

		public void MakeFaceOff (bool final = false)
		{
				StartCoroutine (Process (final));
		}

		public IEnumerator Process (bool final)
		{
				audio.PlayOneShot (whistleSound);
				matchTimer.StopTimer ();
				if (!final) {
						PlacePuck ();
						PlacePlayers ();
						yield return new WaitForSeconds (waitingTime);
						Whistle ();
						matchTimer.ResumeTimer ();
				} else {
						p1Controller.SetSpeed (0.0f);
						p2Controller.SetSpeed (0.0f);
				}
		}

		void PlacePlayers ()
		{
				p1Controller.SetSpeed (0.0f);
				p2Controller.SetSpeed (0.0f);
				player2.transform.position = distanceVector;
				player1.transform.position = -distanceVector;
				player1.rigidbody2D.Sleep ();
				player2.rigidbody2D.Sleep ();
				player1.rigidbody2D.velocity = Vector2.zero;
				player2.rigidbody2D.velocity = Vector2.zero;
				player1.rigidbody2D.angularVelocity = 0f;
				player2.rigidbody2D.angularVelocity = 0f;
				player1.rigidbody2D.rotation = 0f;
				player2.rigidbody2D.rotation = 180f;
				p1Controller.Reset ();
				p2Controller.Reset ();
				p1Controller.SetBlocked (true);
				p2Controller.SetBlocked (true);
		}

		void PlacePuck ()
		{
				puck.parent = null;
				puck.rigidbody2D.position = Vector3.zero;
				//puck.rigidbody2D.Sleep ();
				if (!puck.rigidbody2D.isKinematic) {
						puck.rigidbody2D.velocity = Vector2.zero;
						puck.rigidbody2D.angularVelocity = 0f;
				}
		}

		void Whistle ()
		{
				audio.PlayOneShot (whistleSound);
				puck.rigidbody2D.isKinematic = false;
				player1.rigidbody2D.WakeUp ();
				player2.rigidbody2D.WakeUp ();
				//puck.rigidbody2D.WakeUp ();
				p1Controller.SetBlocked (false);
				p2Controller.SetBlocked (false);
		}
	

}
