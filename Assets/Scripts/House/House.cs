using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class House : MonoBehaviour
{
    /*[SerializeField]*/ float minSpawnTime = 10.0f;
    /*[SerializeField]*/ float maxSpawnTime = 60.0f;
    
    private float nextSpawnTime = 0.0f;
    private float spawnProgression = 0.0f;
    
    private GameObject visitor = null;
    private UIManager uiManager = null;
    
    void Start()
    {
        visitor = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TmpPerson.prefab");
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        nextSpawnTime = Random.Range(0, minSpawnTime);
    }

    void Update()
    {
        spawnProgression += Time.deltaTime;
        if (spawnProgression >= nextSpawnTime)
        {
            spawnProgression = 0.0f;
            nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            Instantiate(visitor, transform.position, Quaternion.identity);
            uiManager.addVisitor(1);
        }
    }
}
