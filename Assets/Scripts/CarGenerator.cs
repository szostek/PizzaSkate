using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    [SerializeField] private Car carPrefab;
    [SerializeField] private Player player;

    void Start()
    {
        InvokeRepeating("GenerateCar", 30f, Random.Range(8f, 24f));
    }

    private void GenerateCar()
    {
        if (!player.hasStartedGame) return;
        Vector3 playerPosition = player.GetPosition();
        Vector3 carPosition = new Vector3(playerPosition.x + 10f, 1.5f, playerPosition.z);
        Instantiate(carPrefab, carPosition, Quaternion.identity);
    }

}
