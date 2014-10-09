using UnityEngine;
using System.Collections;
using XboxCtrlrInput;


[RequireComponent (typeof(PlayerController))]

public class PlayerAttack : MonoBehaviour
{
		/// <summary>
		/// Attack cooldown, to avoid spamming.
		/// </summary>
		public float cooldown = 1.5f;
		private float cooldownTimer;
		/// <summary>
		/// Useful for blocking attacks during the cooldown.
		/// </summary>
		private bool canAttack;
		/// <summary>
		/// Amount of speed gained instantly when attacking.
		/// </summary>
		public float attackStrength = 7.5f;
		/// <summary>
		/// The duration of the actual attack, in seconds.
		/// </summary>
		public float attackDuration = 0.3f; 
		private float attackTimer;
		/// <summary>
		/// The minimum probability of attacking.
		/// </summary>
		public float minimumAttackPercent = 5f;

		/// <summary>
		/// The sound played on successful attacks.
		/// </summary>
		public AudioClip attackSound;

		private PlayerController myController;

		void Awake ()
		{
				myController = GetComponent<PlayerController> ();
				canAttack = true;
				cooldownTimer = 0.0f;
				attackTimer = 0.0f;
		}


		void Update ()
		{
				CheckEnd ();
				Cool ();
				CheckInput ();
		}

		/// <summary>
		/// Checks the end of the attack.
		/// </summary>
		void CheckEnd ()
		{
				attackTimer += Time.deltaTime;
				if (attackTimer >= attackDuration) {
						myController.EndAttack ();
				}
		}

		/// <summary>
		/// Manages cooldown.
		/// </summary>
		void Cool ()
		{
				if (!canAttack) {
						cooldownTimer += Time.deltaTime;
						if (cooldownTimer >= cooldown) {
								canAttack = true;
								cooldownTimer = 0.0f;
						}
				}
		}

		/// <summary>
		/// Checks if the attack input has been pressed.
		/// </summary>
		void CheckInput ()
		{
				if (canAttack && XCI.GetButtonDown (XboxButton.X, myController.GetID ())) {
						canAttack = false;
						myController.StartAttack ();
						attackTimer = 0.0f;
						// giving a "push"
			myController.AddSpeed (attackStrength);
						//rigidbody.AddForce(transform.forward * attackStrength, ForceMode.VelocityChange);
				}
		}

		void OnCollisionEnter (Collision col)
		{
				if (col.transform.tag == "Player") {
						// compute if attack is a success
						float mySpeed = myController.GetSpeed();
						PlayerController otherController = col.gameObject.GetComponent<PlayerController> ();
						float otherSpeed = otherController.GetSpeed();
						float speedProportion = ((mySpeed / otherSpeed) - 1.0f);
						// applying force to opponent
						float newOpponentSpeed = otherSpeed - mySpeed;
						otherController.SetSpeed(newOpponentSpeed);			
						// TODO: add a force in contact direction
						// if attack is a success
						float r = Random.Range (0, 100);
						// TODO: consider strength and resistance for random
						float ceiling = speedProportion * 40f;
						// always a minimum chance to steal;
						if (ceiling < minimumAttackPercent)
								ceiling = minimumAttackPercent;
						//Debug.Log ("stun opponent ? " + r + "/" + ceiling + "(" + speedProportion);
						if (r < ceiling) {
								// make opponent lose the puck
								audio.PlayOneShot (attackSound);
								col.gameObject.GetComponent<PlayerController> ().Stun ();
						}
				} else if (col.transform.tag == "Wall") {
						if (myController.IsAttacking ()) {	
								audio.PlayOneShot (attackSound);	
								GetComponent<PlayerController> ().Stun ();
						}
				} else if (col.transform.tag == "GoalKeeper") {
						// if we touch a goalkeeper, he steals the puck (only if current player has it
						if (myController.HasPuck ()) {
								audio.PlayOneShot (attackSound);
								myController.Stun ();
								col.gameObject.GetComponent<PlayerController> ().GivePuck ();
						}
				}
		}
	
	
}