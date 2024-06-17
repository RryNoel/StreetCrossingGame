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

    void GeneratorEnvironment()
    {
        int randomindex;
        int randomval;

        GameObject tempClone;
        Vector3 offSetPos = Vector3.zero;

        for (int i = StartMinVal; i < StartMaxVal; i++)
        {
            randomval = Random.Range(0, 100);
            if(randomval < SpawnCreateRandom)
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
}
