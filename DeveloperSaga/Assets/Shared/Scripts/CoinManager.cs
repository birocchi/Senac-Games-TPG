using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour
{
		public int numberOfCoins;
		public int maximumCoins;

		// Use this for initialization
		void Start ()
		{
				GameObject[] coins = GameObject.FindGameObjectsWithTag ("Coin");
				maximumCoins = coins.Length;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
