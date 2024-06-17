using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bottom : MonoBehaviour
{
    public string objectTag;

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

        GameObject cloneObj = ObjectPoolManager.Instance.SpawnFromPool(objectTag, offSetPos, generationPos.rotation);

        if (cloneObj != null)
        {
            cloneObj.SetActive(true);
        }
    }
}
