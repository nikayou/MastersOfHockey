using UnityEngine;
using System.Collections;


public class MatchTimer : MonoBehaviour
{

		public GUISkin skin;
		private float matchTime = 60.0f;
		private float timer;
		private int status = -1; // 0 for no winner, otherwise, id of the winner
		private bool counting; // is the timer enabled ?
		private bool waitQuit; // are we waiting for someone to press a key?

		void Awake ()
		{
				timer = 0f;
				waitQuit = false;	
		}

		void Start ()
		{
				matchTime = GetComponent<MatchSettings> ().matchTime;
				counting = false;
				GetComponent<FaceOff>().MakeFaceOff();
		}

		void Update ()
		{
				if (counting) {
						timer += Time.deltaTime;
						if (timer >= matchTime) {
								StartCoroutine (EndMatch ());
						}
				}
				if (waitQuit && Input.anyKeyDown) {
						Application.LoadLevel (0);

				}

		}

		IEnumerator EndMatch ()
		{
				GetComponent<FaceOff> ().MakeFaceOff (true);
				yield return new WaitForSeconds (1.0f);
				MatchInfo mi = GetComponent<MatchInfo> ();
				status = 0;
				if (mi.GetScore (1) > mi.GetScore (2))
						status = 1;
				else if (mi.GetScore (2) > mi.GetScore (1))
						status = 2;
				yield return new WaitForSeconds (4.0f);
				waitQuit = true;

		}


		void OnGUI ()
		{
				if (status > -1) {
						string msg = "Draw";
						if (status > 0) {
								// TODO: replace by the team's name
								msg = "Player " + status + " wins";
			}
			GUI.backgroundColor = Color.black;
						GUI.Box (GUIUtils.CenteredNormal (0.5f, 0.5f, 0.4f, 0.1f), msg, skin.box);
				}
		}

		public void StopTimer ()
		{
				counting = false;
		}

		public void ResumeTimer ()
		{
				counting = true;
		}

		public float GetTime ()
		{
				return timer;
		}

		public float GetRemainingTime ()
		{
				return matchTime - timer;
		}
}
