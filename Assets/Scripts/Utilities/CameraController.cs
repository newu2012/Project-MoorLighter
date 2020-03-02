using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private void FixedUpdate()
    {
        var position = player.position;
        transform.position = new Vector3(position.x, position.y, position.z - 10);
    }
}
