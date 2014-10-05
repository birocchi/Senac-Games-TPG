using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public static int energiaTotal = Constants.ENERGIA_TOTAL_INICIAL;
	public static int moedasTotal = Constants.MOEDAS_TOTAL_INICIAL;
	public static float energia = energiaTotal;
	public static int moedas = Constants.MOEDAS_INICIAL;
	public static List<Ability> listaHabilidades = new List<Ability>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
