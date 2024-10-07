using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{

    [SerializeField] private GameObject heartPreFab, staminaPrefab;
    public void DropItems()
    {
        int randomNum = Random.Range(1, 5);

        if(randomNum == 1)
        {
            Instantiate(heartPreFab, transform.position, Quaternion.identity);
        }
        if (randomNum == 2)
        {
            int randomNumOfStamina=Random.Range(1, 4);
            for (int i = 0; i < randomNumOfStamina; i++)
            {
                Instantiate(staminaPrefab, transform.position, Quaternion.identity);
            }            
        }
    }
}
