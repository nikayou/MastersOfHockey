using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create the public.
/// </summary>
public class CreatePublic : MonoBehaviour
{
		/// <summary>
		/// The nb of spectators.
		/// </summary>
		public int nb;
		/// <summary>
		/// Reference to the puck. Spectators stares at it
		/// </summary>
		private Transform puck;
		/// <summary>
		/// The guy prefab, each spectator is created from this.
		/// </summary>
		public GameObject prefab;
		/// <summary>
		/// Upper-left corner, kept for coordinates.
		/// </summary>
		private Vector2 corner;
		private int[,] ranges; // first is direction (n, e, s, w), second is index of the line
		private Queue<Vector2> alreadyHere;

		void Awake ()
		{
				corner = new Vector2 (-32, 17);
				ranges = new int[4, 5];
				alreadyHere = new Queue<Vector2> ();
		}

		void Start ()
		{
		        puck = GameObject.FindGameObjectWithTag("Puck").transform;
				Process ();
		}

		void Process ()
		{
				int n = 0;
				while (n < nb) {
						Create (n / 100);
						n++;
				}
		}

		bool IsAlreadyHere (int x, int y)
		{
				foreach (Vector2 v in alreadyHere) {
						//Debug.Log ("testing " + v + "/" + x + "," + y);
						if (v.x == x && v.y == y)
								return true;
				}
				return false;
				/*
				Debug.Log ("nb : " + n);
				for (int i = 0; i < n; i++) {
						if (array [i].x == x && array [i].y == y)
								return true;
						//if (v.x == x && v.y == y)
						//		return true;
				}
				return false;*/

		}

		bool IsAlreadyHere (Vector2 v)
		{
				return IsAlreadyHere ((int)v.x, (int)v.y);
		}

		void Create (int range)
		{
				//int direction = GetMinDirection (range);
				int direction = Random.Range (0, 4);
				Vector2 vector = new Vector2 (range, range);
				if (direction == 1 || direction == 3) {
						vector.y = Random.Range (0, 18);
				} else {
						vector.x = Random.Range (0, 34);
				}
				//Debug.Log ("got vector : " + vector);
						
				if (IsAlreadyHere (vector))
						return;
				alreadyHere.Enqueue (vector);
				
				GameObject guy = Instantiate (prefab) as GameObject;
				Vector2 pos = ComputePosition ((int)vector.x, (int)vector.y, direction, range % 2);
				//pos.y = 0.1f + ((5.0f - range) / 10.0f);
				guy.transform.parent = transform;
				guy.transform.localPosition = pos;
				float sc = Random.Range (0.75f, 1.2f);
				guy.transform.localScale *= sc;
				guy.renderer.materials [0].color = PossibleColors.GetRandom ();
				Stare t = guy.AddComponent<Stare> ();
				t.target = puck;
				ranges [direction, range]++;
		}

		int GetMinDirection (int range)
		{
				int dir = 0;
				for (int i = 1; i <= 3; i++) {
						if (ranges [i, range] < ranges [i - 1, range])
								dir = i;
				}
				return dir;
		}

		Vector2 ComputePosition (int x, int y, int direction, int evenRange)
		{
				Vector2 position = Vector2.zero;
				switch (direction) {
				case 0:
						position.x = corner.x + 2 * x - evenRange;
						position.y = corner.y + 2 * y;
						break;
				case 1:
						position.x = -corner.x + 2 * x;
						position.y = corner.y - 2 * y + evenRange;
						break;
				case 2:
						position.x = corner.x + 2 * x - evenRange;
						position.y = - corner.y - 2 * y;
						break;
				case 3:
						position.x = corner.x - 2 * x;
						position.y = -corner.y + 2 * y + evenRange;
						break;
				}
				//Debug.Log ("spawning "+x+","+y+"("+direction+") at "+position);
				return position;
		}


}
