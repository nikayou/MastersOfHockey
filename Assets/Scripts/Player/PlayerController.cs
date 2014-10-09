using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
		PlayerAttack attackScript;
		PlayerShoot shootScript;
		PlayerPick pickScript;
		PlayerMove moveScript;

		PolygonCollider2D catchingZone;

		Transform puck;
		PuckController puckScript;

		/// <summary>
		/// Are inputs allowed for this player? (Pause doesn't count here).
		/// </summary>
		bool canInput = true;

		/// <summary>
		/// Total duration of stunning, in seconds.
		/// </summary>
		public float stunTime = 3.0f;
		/// <summary>
		/// Actual speed.
		/// </summary>
		float speed = 0f;
		/// <summary>
		/// When the player is stun, he loses the puck and cannot move for a while
		/// </summary>
		bool stun = false;
		/// <summary>
		/// Is the player charging a shot?
		/// </summary>
		bool charging = false;
		/// <summary>
		/// Does the player have the puck?
		/// </summary>
		bool hasPuck = false;
		/// <summary>
		/// Is the player in an attack?
		/// </summary>
		bool attacking = false;
		/// <summary>
		/// PlayerID, in the team.
		/// </summary>
		public int playerID = 1;

		void Awake ()
		{
				attackScript = GetComponent<PlayerAttack> ();	
				shootScript = GetComponent<PlayerShoot> ();
				pickScript = GetComponent<PlayerPick> ();
				moveScript = GetComponent<PlayerMove> ();
				catchingZone = GetComponent<PolygonCollider2D> ();
		}
	
		void Start ()
		{
				
				puck = GameObject.FindGameObjectWithTag ("Puck").transform;
				puckScript = puck.GetComponent<PuckController> ();
		}

		void Update ()
		{
				if (hasPuck) {
						SetControls (true);
						SetPickEnabled (false);
				} else {
						SetControls (false);
						SetPickEnabled (true);
				}

		}

		public void Reset ()
		{
				hasPuck = false;
				attacking = false;
				charging = false;
				stun = false;
				speed = 0.0f;
				canInput = true;
		}

		/// <summary>
		/// Change the controls according to the possession of the puck.
		/// </summary>
		/// <param name="hasPuck">If true, settings controls for puck possession</param>
		void SetControls (bool hasPuck)
		{
				// cannot attack if has puck
				if (attackScript != null)
						attackScript.enabled = !hasPuck;
				// can shoot if has puck
				if (shootScript != null)
						shootScript.enabled = hasPuck;
				// cannot pick if already has puck
				if (pickScript != null)
						pickScript.enabled = !hasPuck;
		}

		public void SetColor (Color newColor)
		{
				ColorChange.SetColor (transform, newColor);
		}

		public bool CanInput ()
		{
				return canInput;
		}
	
		/// <summary>
		/// Stun the player and launches coroutine to un-stun him "stunTime" seconds later.
		/// </summary>
		public IEnumerator Stun ()
		{
				// TODO: walls should stun you (or even bump you)
				stun = true;
				canInput = false;
				LosePuck ();
				yield return new WaitForSeconds (stunTime);
				canInput = true;
				stun = false;
		}
	
		public void StartAttack ()
		{
				canInput = false;
				attacking = true;
		}
	
		public void EndAttack ()
		{
				canInput = true;
				attacking = false;
		}
	
		public void SetBlocked (bool val = true)
		{
				bool trueVal = !val;
				if (attackScript != null)
						attackScript.enabled = trueVal;
				shootScript.enabled = trueVal;
				pickScript.enabled = trueVal;
				moveScript.enabled = trueVal;
		}
	
		public void SetPickEnabled (bool val = true)
		{
				catchingZone.enabled = val;
				pickScript.enabled = val;
		}
	
		public void GivePuck ()
		{
				hasPuck = true;
				SetPickEnabled (false);
				puckScript.SetParent (transform.Find ("Stick"));
				// goalkeepers are center-oriented when they obtain the puck
				// TODO: doesn't work
				//if (transform.tag == "GoalKeeper")
				//		transform.LookAt (Vector3.zero);
		}
	
		public void LosePuck (Vector2 force)
		{      
				shootScript.ChangeEyes ();
				hasPuck = false;
				charging = false;
				puckScript.SetParent (null);
				puck.rigidbody2D.AddForceAtPosition (force, (Vector2)transform.position, ForceMode2D.Impulse);
		}
	
		public void LosePuck ()
		{
				LosePuck (Vector2.zero);
		}

		public bool IsCharging ()
		{
				return charging;
		}

		public bool HasPuck ()
		{
				return hasPuck;
		}

		public bool IsStun ()
		{
				return stun;
		}

		public bool IsAttacking ()
		{
				return attacking;
		}

		public int GetID ()
		{
				return playerID;
		}

		public float SetSpeed (float val)
		{
				return (speed = val);
		}

		public float GetSpeed ()
		{
				return speed;
		}

		public float AddSpeed (float amount)
		{
				return (speed += amount);
		}

		public void SetCharging (bool val = true)
		{
				charging = val;
		}


}

