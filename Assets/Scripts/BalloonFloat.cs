using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFloat : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1f;   // How fast it moves up & down
    [SerializeField] private float floatHeight = 0.05f; // How far it moves up & down

    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        startPos = transform.localPosition;
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed+randomOffset) * floatHeight;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }

}
