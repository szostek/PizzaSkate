using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float CAMERA_DISTANCE_SPAWN_LEVEL_PART = 100f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Player player;

    private Camera mainCamera;
    private Vector3 lastEndPosition;

    private void Awake() 
    {
        mainCamera = Camera.main;
        lastEndPosition = levelPart_Start.Find("EndPoint").position;

        int startSpawnLevelParts = 3;
        for (int i = 0; i < startSpawnLevelParts; i++) {
            SpawnLevelPart();
        }    
    }

    private void Update() 
    {
        if (player != null && !player.hasStartedGame) return;

        // Use the camera's position instead of the player's position
        Vector3 cameraPosition = mainCamera.transform.position;

        if (Vector3.Distance(cameraPosition, lastEndPosition) < CAMERA_DISTANCE_SPAWN_LEVEL_PART) {
            SpawnLevelPart();
        }

    }

    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPartTransform(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPoint").position;
    }

    private Transform SpawnLevelPartTransform(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
