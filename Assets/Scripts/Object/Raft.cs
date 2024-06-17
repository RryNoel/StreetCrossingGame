using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : MonoBehaviour
{
    public float moveSpeed;

    private void Update()
    {
        float x = moveSpeed * Time.deltaTime;
        transform.Translate(x, 0f, 0f);

        if (transform.localPosition.x > 12f) Destroy(this.gameObject);
    }
}
