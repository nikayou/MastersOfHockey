using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerPick is supposed to be attached to a trigger child object of the player, representing the area when the player
/// can take the pick when facing it.
/// </summary>
public class PlayerPick : MonoBehaviour
{
		/// <summary>
		/// Reference to the puck.
		/// </summary>
		private Transform puck;
		private PuckController puckScript;
		private PlayerController myController;

		void Start ()
		{
				GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
				myController = GetComponent<PlayerController> ();
				puck = gameController.GetComponent <PuckHolder> ().GetPuck ();
				puckScript = puck.GetComponent<PuckController> ();
		}
	
		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.transform == puck) {
						if (puckScript.IsFree () && !myController.IsStun ()) {
								// can pick only if not stun
								myController.GivePuck ();
						}
				}
		}
	


}
