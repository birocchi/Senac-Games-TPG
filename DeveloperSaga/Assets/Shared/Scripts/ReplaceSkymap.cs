using UnityEngine;
using System.Collections;

public class ReplaceSkymap : MonoBehaviour
{
		Texture originalMaterial;
		Color originalColor;

		public Material[] materialsToRestore;
		Color[] originalColors;


		// Use this for initialization
		void Start ()
		{
				originalColors = new Color[materialsToRestore.Length];
				int i = 0;
				originalColor = RenderSettings.skybox.GetColor ("_Tint");
				foreach (Material mat in materialsToRestore) {
						originalColors [i] = mat.GetColor ("_Color");
						i++;
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnApplicationQuit ()
		{
				int i = 0;
				RenderSettings.skybox.SetColor ("_Tint", originalColor);
				foreach (Material mat in materialsToRestore) {
						mat.SetColor ("_Color", originalColors [i++]);
						i++;
				}
		}
}
