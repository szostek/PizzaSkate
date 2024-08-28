using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleterBox : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize = new Vector2(5f, 20f);
    [SerializeField] private LayerMask layerMask; // Layer mask to filter the objects you want to detect

    private void Update()
    {
        Vector2 boxCenter = transform.position;

        // Check for colliders
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, layerMask);

        // Loop through the detected colliders and destroy them:
        foreach (Collider2D collider in colliders)
        {
            Destroy(collider.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
