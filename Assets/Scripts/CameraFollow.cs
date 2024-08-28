using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public float yAdjustment = 1f;
    [SerializeField] private Transform target;
    public bool fallBehindCam = false;

    void Update()
    {
        if (target && !fallBehindCam) {
            Vector3 newPos = new Vector3(target.position.x + 1.25f, (target.position.y / 8) + yAdjustment, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        } else {
            transform.Translate(Vector3.right * Time.deltaTime * (followSpeed / 2));
        }
    }
}
