using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISManager : MonoBehaviour
{
    public List<GameObject> pois = new List<GameObject>();
    
    void Start()
    {
        pois.AddRange(GameObject.FindGameObjectsWithTag("POI"));
    }

    void Update()
    {
        
    }
    
    public GameObject GetRandomPOI(POI differentFrom)
    {
        if (pois.Count == 0)
        {
            return null;
        }
        GameObject poi = pois[Random.Range(0, pois.Count)];
        while (poi.GetComponent<POI>() == differentFrom)
        {
            poi = pois[Random.Range(0, pois.Count)];
        }
        return poi;
    }
}
