using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(Collider2D))]

/// <summary>
/// Manages when a goal occurs.
/// </summary>
public class Goal : MonoBehaviour
{

		/// <summary>
		/// Sound played when a goal occurs.
		/// </summary>
		public AudioClip hornClip;
		/// <summary>
		/// Sound played by crowd.
		/// </summary>
		public AudioClip crowdSound;
		/// <summary>
		/// The opponent team's ID, useful to add score.
		/// </summary>
		public int opponentTeam;
		private MatchInfo matchInfo;
		private FaceOff faceOff;

		// Use this for initialization
		void Start ()
		{
				GameObject gc = GameObject.Find ("GameController");
				matchInfo = gc.GetComponent<MatchInfo> ();
				faceOff = gc.GetComponent<FaceOff> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				// TODO: find a cleaner way to prevent goal from marking itself
				if (other.transform.tag == "Puck") {
						if (other.transform.parent == null || (other.transform.parent.parent != null && other.transform.parent.parent.tag != "GoalKeeper")) {
								matchInfo.AddScore (1, opponentTeam);
								audio.PlayOneShot (hornClip);
								// TODO: optimization here
								if (Random.Range (0, 4) == 0) {
										GameObject.Find ("Level/Public").audio.PlayOneShot (crowdSound);
								}
								other.rigidbody2D.isKinematic = true;
								faceOff.MakeFaceOff ();
						}
				}
		}

}
