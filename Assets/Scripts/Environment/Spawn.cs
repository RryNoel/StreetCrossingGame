using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> EnvironmentObjectList = new List<GameObject>();
    public int StartMinVal = -12;
    public int StartMaxVal = 12;

    public int SpawnCreateRandom = 50;

    private void Start()
    {
        GeneratorEnvironment();
    }

    void GeneratorRoundBlock()
    {
        int randomindex;
        GameObject tempClone;
        Vector3 offSetPos = Vector3.zero;

        for (int i = StartMinVal; i < StartMaxVal; i++)
        {
            if(i < -5 || i > 5)
            {
                randomindex = Random.Range(0, EnvironmentObjectList.Count);
                tempClone = Instantiate(EnvironmentObjectList[randomindex]);
                offSetPos.Set(i, 0f, 0f);

                tempClone.transform.SetParent(transform);
                tempClone.transform.localPosition = offSetPos;
            }
        }
    }

    void GeneratorBackBlock()
    {
        int randomindex;
        GameObject tempClone;
        Vector3 offSetPos = Vector3.zero;

        for (int i = StartMinVal; i < StartMaxVal; i++)
        {
            randomindex = Random.Range(0, EnvironmentObjectList.Count);
            tempClone = Instantiate(EnvironmentObjectList[randomindex]);
            tempClone.SetActive(true);
            offSetPos.Set(i, 0f, 0f);

            tempClone.transform.SetParent(transform);
            tempClone.transform.localPosition = offSetPos;
        }
    }

    void GeneratorTree()
    {
        int randomindex;
        int randomval;

        GameObject tempClone;
        Vector3 offSetPos = Vector3.zero;

        for (int i = StartMinVal; i < StartMaxVal; i++)
        {
            randomval = Random.Range(0, 100);
            if (randomval < SpawnCreateRandom)
            {
                randomindex = Random.Range(0, EnvironmentObjectList.Count);
                tempClone = Instantiate(EnvironmentObjectList[randomindex]);
                tempClone.SetActive(true);
                offSetPos.Set(i, 0f, 0f);

                tempClone.transform.SetParent(transform);
                tempClone.transform.localPosition = offSetPos;
            }
        }
    }

    void GeneratorEnvironment()
    {
        if(transform.position.z <= -4)
        {
            GeneratorBackBlock();
        }
        else if(transform.position.z <= 0)
        {
            GeneratorRoundBlock();
        }
        else
        {
            GeneratorTree();
        }
    }
}
