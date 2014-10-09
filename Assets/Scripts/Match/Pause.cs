using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class Pause : MonoBehaviour
{

		private bool onPause = true;

		// Update is called once per frame
		void Update ()
		{
				if (XCI.GetButtonUp (XboxButton.Start)) {
						onPause = !onPause;			
				}
				if (onPause) {
						Time.timeScale = 0;
				} else {
						Time.timeScale = 1;
				}
		}
}
