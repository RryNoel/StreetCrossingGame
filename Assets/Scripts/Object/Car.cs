using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CarSO car;

    private void Update()
    {
        float x = car.moveSpeed * Time.deltaTime;
        transform.Translate(x, 0f, 0f);
    }
}
