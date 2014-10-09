using UnityEngine;
using System.Collections;

public class PuckHolder : MonoBehaviour
{

		/// <summary>
		/// The puck that will be instantiated.
		/// </summary>
		public GameObject puckPrefab;
		/// <summary>
		/// Reference to the puck.
		/// </summary>
		private GameObject puck;

		void Awake ()
		{
				puck = Instantiate (puckPrefab) as GameObject;
		}

		void Start ()
		{
				Camera.main.GetComponent<Track> ().target = puck.transform;
		}

		public GameObject GetPuckObject ()
		{
				return puck;
		}

		public Transform GetPuck ()
		{
				return puck.transform;
		}

}