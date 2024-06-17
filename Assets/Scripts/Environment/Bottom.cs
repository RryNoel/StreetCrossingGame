using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    public GameObject cloneTarget;
    public Transform generationPos;
    public int generationPersent = 50;

    public float cloneDelaySec = 1f;

    protected float nextSecToClone;

    private void Update()
    {
        float currentSec = Time.time;

        if(nextSecToClone <= currentSec)
        {
            int randomVal = Random.Range(0, 100);
            if( randomVal <= generationPersent)
            {
                CloneCar();
            }

            nextSecToClone = currentSec + cloneDelaySec;
        }
    }

    void CloneCar()
    {
        Transform clonePos = generationPos;
        Vector3 offSetPos = clonePos.position;
        offSetPos.y = 0f;

        GameObject cloneObj = Instantiate(cloneTarget.gameObject, offSetPos, generationPos.rotation, transform);

        cloneObj.SetActive(true);
    }
}
