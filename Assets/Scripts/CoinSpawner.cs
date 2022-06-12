using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public bool coinSpawned = false;
    public float maxX, maxY;
    public GameObject coin;

    public void SpawnCoin()
    {
        coinSpawned = true;
        Vector2 coinPosition = new Vector2(Random.Range(-maxX, maxX), Random.Range(-maxY+0.7f, maxY));
        Instantiate(coin, coinPosition, Quaternion.identity);
    }

}
