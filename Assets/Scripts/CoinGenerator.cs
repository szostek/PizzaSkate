using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private Player player;
    [SerializeField] private float minY = 1.5f;
    [SerializeField] private float maxY = 5f;


    void Start()
    {
        InvokeRepeating("GenerateCoin", 2f, Random.Range(2f, 20f));
    }

    private void GenerateCoin()
    {
        Vector3 playerPosition = player.GetPosition();
        float randomY = Random.Range(minY, maxY);
        Vector3 coinPosition = new Vector3(playerPosition.x + 10f, randomY, playerPosition.z);
        Instantiate(coinPrefab, coinPosition, Quaternion.identity);
    }


}
