using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

[RequireComponent (typeof(PlayerController))]
[RequireComponent (typeof(AudioSource))]

public class PlayerShoot : MonoBehaviour
{
		/// <summary>
		/// The minimum strength of a shot.
		/// </summary>
		public float minStrength = 10.0f;
		/// <summary>
		/// The maximum strength of a shot.
		/// </summary>
		public float maxStrength = 30.0f;
		/// <summary>
		/// Time required to reach from minimum strength to maximum strength.
		/// </summary>
		public float timeFromMinToMax = 0.7f;
		/// <summary>
		/// The actual strength.
		/// </summary>
		private float strength;
		private bool shoot;
		/// <summary>
		/// The strength earned per seconds.
		/// </summary>
		private float strengthPerSeconds;
		/// <summary>
		/// The sound played when shooting.
		/// </summary>
		public AudioClip shotSound;

		public Color eyesColor = Color.black;
		public Color furyColor = Color.white; // eyes color when at maximum strength
		private Transform eyes; // reference to the eyes (1st child) to color them while charging

		private PlayerController myController;
		
		void Awake ()
		{
				strengthPerSeconds = (maxStrength - minStrength) / timeFromMinToMax;
				strength = minStrength;
				eyes = transform.Find ("Eyes");
				eyes.renderer.material.color = eyesColor;
				myController = GetComponent<PlayerController> ();
		}

		void Update ()
		{
				if (myController.HasPuck ()) {
						CheckInputs ();
						if (myController.IsCharging ()) {
								AddStrength (strengthPerSeconds * Time.deltaTime);
						}
						ChangeEyes ();
				}
		}
	

		public void ChangeEyes ()
		{
				eyes.renderer.material.color = Color.Lerp (eyesColor, furyColor, (strength - minStrength) / (maxStrength - minStrength));
		}

		void FixedUpdate ()
		{
				if (myController.HasPuck () && shoot) {
						shoot = false;
						Shoot ();
				}
		}

		/// <summary>
		/// Checks if shoot inputs have been pressed / released. Sets the animator accordingly. 
		/// </summary>
		void CheckInputs ()
		{
				if (XCI.GetButtonDown (XboxButton.X, myController.GetID ()) || Input.GetKeyDown (KeyCode.F)) {
						myController.SetCharging(true);			
						strength = minStrength;
						// TODO: shot feints in pass
				} else if (XCI.GetButtonUp (XboxButton.X, myController.GetID ()) || Input.GetKeyUp (KeyCode.F)) {
						myController.SetCharging(false);
						shoot = true;
				}
				
		}

		/// <summary>
		/// Adds the given amount of strength. Filters in case of overflow. 
		/// </summary>
		/// <param name="amount">Amount of strength added (can be negative).</param>
		void AddStrength (float amount)
		{
				strength += amount;
				if (strength > maxStrength) {
						strength = maxStrength;
				} else if (strength < minStrength) {
						strength = minStrength;
				}
		}

		/// <summary>
		/// Shoot the puck. Adds a forward force to it. 
		/// </summary>
		public void Shoot ()
		{
				myController.SetPickEnabled (false);
				float speed = myController.GetSpeed ();
				Vector2 force = transform.right;
				force *= (strength + speed);
				strength = minStrength;
				audio.PlayOneShot (shotSound);
				myController.LosePuck (force);
		}


}