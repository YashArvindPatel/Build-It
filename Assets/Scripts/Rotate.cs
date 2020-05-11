using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform building;
    public float speed;

    void Update()
    {
        transform.RotateAround(building.position, Vector3.up, speed);
    }
}
