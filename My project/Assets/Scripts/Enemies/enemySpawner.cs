using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject spawnTrigger;
    public enemyBase[] enemyPrefab;
    public int[] enemyAmount;
    public Vector3[] enemyLocation;
    public LayerMask layerMask;
    bool activated = false;

    void Update()
    {
        if (!activated && Physics.OverlapBox(spawnTrigger.transform.position, spawnTrigger.transform.lossyScale, spawnTrigger.transform.rotation, layerMask).Length > 0)
        {
            int spawnIndex = 0;
            activated = true;
            for (int i = 0; i < enemyAmount.Length; i++)
            {
                for(int j = 0; j < enemyAmount[i]; j++)
                {
                    Instantiate(enemyPrefab[i], enemyLocation[spawnIndex], transform.rotation);
                    spawnIndex++;
                }
            }
        }
    }
}
