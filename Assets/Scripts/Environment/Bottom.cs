using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bottom : MonoBehaviour
{
    public string[] carTags; // 다양한 자동차 프리팹의 태그를 저장할 배열
    public Transform generationPos;
    public int generationPersent = 50;
    public float cloneDelaySec = 1f;

    protected float nextSecToClone;

    private void Update()
    {
        float currentSec = Time.time;

        if (nextSecToClone <= currentSec)
        {
            int randomVal = Random.Range(0, 100);
            if (randomVal <= generationPersent)
            {
                CloneObject();
            }

            nextSecToClone = currentSec + cloneDelaySec;
        }
    }

    void CloneObject()
    {
        Transform clonePos = generationPos;
        Vector3 offSetPos = clonePos.position;
        offSetPos.y = 0f;

        // 랜덤으로 자동차 태그 선택
        string selectedCarTag = carTags[Random.Range(0, carTags.Length)];
        GameObject cloneObj = ObjectPoolManager.Instance.SpawnFromPool(selectedCarTag, offSetPos, generationPos.rotation);

        if (cloneObj != null)
        {
            cloneObj.SetActive(true);
        }
    }
}
