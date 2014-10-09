using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
public class CreatePublic2 : MonoBehaviour
{

		public int nb;
		public Transform puck;
		public GameObject guyPrefab;
		private Vector2 corner;
		private bool[][] north;
		private bool[][] south;
		private bool[][] east;
		private bool[][] west;
		private int ranges = 5;
		private Queue<Vector2> alreadyHere;

		void Awake ()
		{
				corner = new Vector2 (-32, 17);
				north = new bool[32][5];
				south = new bool[32][5];
				east = new bool[16][5];
				west = new bool[16][5];
				alreadyHere = new Queue<Vector2> ();
		}

		void Start ()
		{
				PlaceAll ();
		}

		void PlaceAll ()
		{
				int n = 0;
				while (n < nb) {
						switch (Random.Range (0, 4)) {
						case 0:
								Place (north);
								break;
						case 1:
								Place (south);
								break;
						case 2:
								Place (east);
								break;
						default :
								Place (west);
								break;
						}
						n++;
				}
		}

		void Place (bool[][] tribune)
		{
				Debug.Log ("length : " + tribune.Length);
				int x = Random.Range (0, tribune.Length);
				int y = Random.Range (0, 2);
				if (IsFull (tribune [y])) {
						y++;
				}
				tribune [x, y] = true;
		}

		bool IsFull (bool[] range)
		{
				for (int i = 0; i < range.Length; i++) {
						if (!range [i])
								return false;
				}
				return true;
		}

		void Create (bool[][] tribune, int facing)
		{
				for (int i = 0; i < tribune.Length; i++) {
						for (int j = 0; j < tribune[i].Length; j++) {
								if (tribune [i] [j]) {
										Vector2 position = ComputePosition (i, j, facing, j % 2);
										CreateGameObject (position.x, position.y);
								}
						}
				}
		}

		void CreateGameObject (int x, int y)
		{
				GameObject guy = Instantiate (guyPrefab) as GameObject;
				guy.transform.parent = transform;
				guy.transform.localPosition += new Vector3 (x, 0, y);
				float sc = Random.Range (0.75f, 1.2f);
				guy.transform.localScale *= sc;
				guy.renderer.materials [0].color = PossibleColors.GetRandom ();
				Stare t = guy.AddComponent<Stare> ();
				t.target = puck;
		}

		Vector3 ComputePosition (int x, int y, int direction, int evenRange)
		{
				Vector3 position = Vector3.zero;
				switch (direction) {
				case 0:
						position.x = corner.x + 2 * x - evenRange;
						position.z = corner.y + 2 * y;
						break;
				case 1:
						position.x = -corner.x + 2 * y;
						position.z = corner.y - 2 * x + evenRange;
						break;
				case 2:
						position.x = corner.x + 2 * x - evenRange;
						position.z = - corner.y - 2 * y;
						break;
				case 3:
						position.x = corner.x - 2 * y;
						position.z = -corner.y + 2 * x + evenRange;
						break;
				}
				return position;
		}


}
*/