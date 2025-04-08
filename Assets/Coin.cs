using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public float coinValue = 5f;

    void Update()
    {
        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
    }
}
