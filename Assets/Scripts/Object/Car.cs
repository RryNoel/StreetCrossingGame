using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CarSO car;

    private void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        float x = car.moveSpeed * Time.deltaTime;
        transform.Translate(x, 0f, 0f);

        if (transform.localPosition.x > 12f || transform.localPosition.x < -12f)
        {
            gameObject.SetActive(false);
        }
    }

    private void Initialize()
    {
        transform.localPosition = Vector3.zero;
    }
}
