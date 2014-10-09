using UnityEngine;

public class ColorChange : MonoBehaviour
{

		public static void SetChildColor (Transform who, string pathToChild, Color newColor)
		{
				who.Find (pathToChild).renderer.material.color = newColor;
		}

		public static void SetColor (Transform who, Color newColor)
		{
				who.renderer.material.color = newColor;
		}

}
