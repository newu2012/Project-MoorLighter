using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
   private Vector3 startingPosition;

   private void Start()
   {
      startingPosition = transform.position;
   }

   private Vector3 GetRoamingPosition()
   {
      //return startingPosition + GetRandomDir() * Random.Range(0f, 70f);
      return new Vector3();
   }
}
