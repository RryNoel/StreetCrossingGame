using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CarSO car;

    private void OnEnable()
    {
        // 활성화될 때 초기화 작업
        Initialize();
    }

    private void Update()
    {
        float x = car.moveSpeed * Time.deltaTime;
        transform.Translate(x, 0f, 0f);

        if (transform.localPosition.x > 12f || transform.localPosition.x < -12f) gameObject.SetActive(false);
    }

    private void Initialize()
    {
        // 초기화 작업 (예: 위치 초기화 등)
        transform.localPosition = Vector3.zero;
    }
}
