using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    private const float CAMERA_DISTANCE_SPAWN_BUILDING = 100f;

    [SerializeField] private Transform startPoint;
    [SerializeField] private List<Transform> buildingPrefabList;
    [SerializeField] private Player player;
    [SerializeField] private float parallaxSpeed = 0.5f; // Speed of the parallax effect

    private Camera mainCamera; // Reference to the main camera
    private Vector3 lastEndPosition;

    private void Awake() 
    {
        mainCamera = Camera.main;
        lastEndPosition = startPoint.position;

        int startSpawnBuildings = 3;
        for (int i = 0; i < startSpawnBuildings; i++) {
            SpawnBuilding();
        }    
    }

    private void Update() 
    {
        if (player != null && !player.hasStartedGame) return;

        // Use the camera's position instead of the player's position
        Vector3 cameraPosition = mainCamera.transform.position;

        if (Vector3.Distance(cameraPosition, lastEndPosition) < CAMERA_DISTANCE_SPAWN_BUILDING) {
            SpawnBuilding();
        }

        // Move buildings for parallax effect
        foreach (Transform building in buildingPrefabList)
        {
            building.position += Vector3.left * parallaxSpeed * Time.deltaTime;
        }
    }

    private void SpawnBuilding()
    {
        Transform chosenBuilding = buildingPrefabList[Random.Range(0, buildingPrefabList.Count)];
        Transform lastBuildingTransform = SpawnBuildingTransform(chosenBuilding, lastEndPosition);
        lastEndPosition = lastBuildingTransform.Find("EndPoint").position;
    }

    private Transform SpawnBuildingTransform(Transform buildingPrefab, Vector3 spawnPosition)
    {
        Transform buildingTransform = Instantiate(buildingPrefab, spawnPosition, Quaternion.identity);
        buildingPrefabList.Add(buildingTransform); // Add the new building to the list for parallax movement
        return buildingTransform;
    }
}
