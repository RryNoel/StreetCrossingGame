using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : Singleton<EnvironmentManager>
{
    public GameObject[] EnvironmentObjectArray;
    public Transform ParentTransform;

    public int MinPosZ = -20;
    public int MaxPosZ = 20;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        for(int i = MinPosZ; i < MaxPosZ; ++i)
        {
            CloneRoad(i);
        }
    }

    void CloneRoad(int posZ)
    {
        int randomindex = Random.Range(0, EnvironmentObjectArray.Length);
        GameObject cloneObj = Instantiate(EnvironmentObjectArray[randomindex]);

        Vector3 offSetPos = Vector3.zero;
        offSetPos.z = (float)posZ;
        cloneObj.transform.SetParent(ParentTransform);
        cloneObj.transform.position = offSetPos;

        int randomRot = Random.Range(0, 2);
        if (randomRot == 1)
        {
            cloneObj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}
