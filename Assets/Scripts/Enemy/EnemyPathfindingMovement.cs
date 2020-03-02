using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfindingMovement : MonoBehaviour
{
    private Enemy _enemy;

    private List<Vector3> pathVectorList;
    private int currentPathIndex;
    private float pathfindingTimer;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        pathfindingTimer -= Time.deltaTime;
        HandleMovement();
    }

    private void FixedUpdate()
    {
        _enemy.rb.velocity = _enemy.movement * _enemy.moveSpeed;
    }

    private void HandleMovement()
    {
//        PrintPathfindingPath();
//        if (pathVectorList != null)
//        {
//            Vector3 targetPosition = pathVectorList[currentPathIndex];
//            float reachedTargetedDistance = 5f;
//            if (Vector3.Distance(GetPosition(), targetPosition > reachedTargetedDistance)
//            {
//                _enemy.movement = (targetPosition - GetPosition()).normalized;
//                _enemy.lastDir = _enemy.movement;
//            }
//        }
    }
}