using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum EnvironmentType
{
    Tree = 0,
    Road,
    River,

    Max
}

public enum LastRoadType
{
    Grass = 0,
    Road,

    Max
}

public class EnvironmentManager : Singleton<EnvironmentManager>
{
    // public GameObject[] EnvironmentObjectArray;
    [Header("복제용길")]
    public Bottom defaultRoad;
    public Bottom waterRoad;
    public Spawn grassRoad;

    public Transform ParentTransform;

    private LastRoadType lastRoadType = LastRoadType.Max;
    private List<Transform> lineMapList = new List<Transform>();
    private Dictionary<int, Transform> lineMapDic = new Dictionary<int, Transform>();
    private int lastLinePos = 0;

    private int minLine = 0;
    public int deleteLine = 5;
    public int backOffSetLineCount = 30;

    public int minPosZ = -20;
    public int maxPosZ = 20;

    public int frontOffSetPosZ = 20;
    public int backOffSetPosZ = 10;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        defaultRoad.gameObject.SetActive(false);
        waterRoad.gameObject.SetActive(false);
        grassRoad.gameObject.SetActive(false);
    }

    public void ResetEnvironment()
    {
        // 모든 라인 제거
        foreach (var line in lineMapList)
        {
            Destroy(line.gameObject);
        }

        lineMapList.Clear();
        lineMapDic.Clear();
        lastLinePos = 0;
        lastRoadType = LastRoadType.Max;
        minLine = 0;

        // 초기 환경 생성 (예: 기본 라인 설정)
        UpdateForwardMap(0);
    }

    public int GroupRandomRoadLine(int posZ)
    {
        int randomCount = Random.Range(1, 4);
        for (int i = 0; i < randomCount; i++)
        {
            GeneratorRoadLine(posZ + i);
        }

        return randomCount;
    }

    public int GroupRandomWaterLine(int posZ)
    {
        int randomCount = Random.Range(1, 3);
        for (int i = 0; i < randomCount; i++)
        {
            GeneratorWaterLine(posZ + i);
        }

        return randomCount;
    }

    public int GroupRandomGrassLine(int posZ)
    {
        int randomCount = Random.Range(1, 4);
        for (int i = 0; i < randomCount; i++)
        {
            GeneratorGrassLine(posZ + i);
        }

        return randomCount;
    }

    public void GeneratorRoadLine(int posZ)
    {
        GameObject cloneObj = Instantiate(defaultRoad.gameObject);

        cloneObj.SetActive(true);

        Vector3 offSetPos = Vector3.zero;
        offSetPos.z = (float)posZ;
        cloneObj.transform.SetParent(ParentTransform);
        cloneObj.transform.position = offSetPos;

        int randomRot = Random.Range(0, 2);
        if (randomRot == 1)
        {
            cloneObj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        cloneObj.name = "DefaultRoad_" + posZ.ToString();

        lineMapList.Add(cloneObj.transform);
        lineMapDic.Add(posZ, cloneObj.transform);
    }
    public void GeneratorWaterLine(int posZ)
    {
        GameObject cloneObj = Instantiate(waterRoad.gameObject);

        cloneObj.SetActive(true);

        Vector3 offSetPos = Vector3.zero;
        offSetPos.z = (float)posZ;
        cloneObj.transform.SetParent(ParentTransform);
        cloneObj.transform.position = offSetPos;

        int randomRot = Random.Range(0, 2);
        if (randomRot == 1)
        {
            cloneObj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        cloneObj.name = "WaterRoad_" + posZ.ToString();

        lineMapList.Add(cloneObj.transform);
        lineMapDic.Add(posZ, cloneObj.transform);
    }

    public void GeneratorGrassLine(int posZ)
    {
        GameObject cloneObj = Instantiate(grassRoad.gameObject);

        cloneObj.SetActive(true);

        Vector3 offSetPos = Vector3.zero;
        offSetPos.z = (float)posZ;
        cloneObj.transform.SetParent(ParentTransform);
        cloneObj.transform.position = offSetPos;

        cloneObj.name = "GrassRoad_" + posZ.ToString();

        lineMapList.Add(cloneObj.transform);
        lineMapDic.Add(posZ, cloneObj.transform);
    }

    public void UpdateForwardMap(int posZ)
    {
        if (lineMapList.Count <= 0)
        {
            lastRoadType = LastRoadType.Grass;
            minLine = minPosZ;
            int i = 0;

            for (i = minPosZ; i < maxPosZ; i++)
            {
                int offSetVal = 0;
                if(i < 0)
                {
                    GeneratorGrassLine(i);
                }
                else
                {
                    if(lastRoadType == LastRoadType.Grass)
                    {
                        int randomVal = Random.Range(0, 2);
                        if(randomVal == 0)
                        {
                            offSetVal = GroupRandomWaterLine(i);
                        }
                        else
                        {
                            offSetVal = GroupRandomRoadLine(i);
                        }

                        lastRoadType = LastRoadType.Road;
                    }
                    else
                    {
                        offSetVal = GroupRandomGrassLine(i);

                        lastRoadType = LastRoadType.Grass;
                    }
                    i += offSetVal - 1;
                }
            }
            lastLinePos = i;
        }

        // 새롭게 생성
        if(lastLinePos < posZ + frontOffSetPosZ)
        {
            int offSetVal = 0;
            if (lastRoadType == LastRoadType.Grass)
            {
                int randomVal = Random.Range(0, 2);
                if (randomVal == 0)
                {
                    offSetVal = GroupRandomWaterLine(lastLinePos);
                }
                else
                {
                    offSetVal = GroupRandomRoadLine(lastLinePos);
                }

                lastRoadType = LastRoadType.Road;
            }
            else
            {
                offSetVal = GroupRandomGrassLine(lastLinePos);

                lastRoadType = LastRoadType.Grass;
            }
            lastLinePos += offSetVal;
        }


        // 많이 지나갔으면 지우기
        if(posZ - backOffSetLineCount > minLine - deleteLine)
        {
            int count = minLine + deleteLine;
            for(int i = minLine; i < count; ++i)
            {
                RemoveLine(i);
            }

            minLine += deleteLine;
        }
    }

    void RemoveLine(int posZ)
    {
        if (lineMapDic.ContainsKey(posZ))
        {
            Transform transObj = lineMapDic[posZ];
            Destroy(transObj.gameObject);

            lineMapList.Remove(transObj);
            lineMapDic.Remove(posZ);
        }
        else
        {
            Debug.Log("error");
        }
    }
}
