using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject HeartPreFab;
    public void DropItems()
    {
        Instantiate(HeartPreFab, transform.position, Quaternion.identity);
    }
}
