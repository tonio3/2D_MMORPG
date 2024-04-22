using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
   [SerializeField] PolygonCollider2D polyCollider;

    private void OnEnable()
    {
   
        Bounds bounds = polyCollider.bounds;

        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minY = bounds.min.y;
        float maxY = bounds.max.y;

        Vector2 randomPoint = new Vector2(
            UnityEngine.Random.Range(minX, maxX),
            UnityEngine.Random.Range(minY, maxY));

        int maxAttempts = 100;
        int attemptCount = 0;

        while (!polyCollider.OverlapPoint(randomPoint) && attemptCount < maxAttempts)
        {
            randomPoint = new Vector2(
                UnityEngine.Random.Range(minX, maxX),
                UnityEngine.Random.Range(minY, maxY));

            attemptCount++;
        }

        transform.position = randomPoint;
    }
}
  
 
 