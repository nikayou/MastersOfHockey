using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class PuckController : MonoBehaviour
{

		public float attractionSmoothness = 1.5f;
		public float attractionCeiling = 0.1f;
		private Transform attraction;

		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
				FollowParent ();
		}

		void FollowParent ()
		{
				if (attraction != null) {
						if (Vector2.Distance ((Vector2)attraction.position, (Vector2)transform.position) > attractionCeiling) {
								Vector2 newPosition = Vector2.Lerp (transform.position, attraction.position, attractionSmoothness * Time.deltaTime);
								transform.position = newPosition;
						} else {
								transform.position = attraction.position;
						}
				}
		}

		public void SetParent (Transform who)
		{
				if (who == null) {
						UnBound ();
						attraction = null;
				} else {
						Bound (who);
						/*attraction = who.Find ("Stick");
						transform.parent = attraction;
						rigidbody2D.MovePosition (attraction.position);
						transform.position = attraction.position;
						transform.localPosition = Vector3.zero;*/
				}
		}

		void Bound (Transform who)
		{
				attraction = who;
				rigidbody2D.isKinematic = true;
				collider2D.enabled = false;
		}

		void UnBound ()
		{
				rigidbody2D.isKinematic = false;
				attraction = null;
				collider2D.enabled = true;
		}

		public bool IsFree ()
		{
				return (attraction == null);
		}

}
