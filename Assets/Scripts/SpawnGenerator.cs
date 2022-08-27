using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] probPrefabs;
    public int count = 100;

    private BoxCollider area;
    private List<GameObject> probs = new List<GameObject>();

    
    void Start()
    {
        area = gameObject.GetComponent<BoxCollider>();
        for(int i = 0; i< count; ++i)
        {
            Spawn();
        }
        area.enabled = false;
        
    }

    private void Spawn()
    {
        int selection = Random.Range(0, probPrefabs.Length);
        GameObject selectedPrefab = probPrefabs[selection];
        Vector3 spanwPos = GetRandomPosition();

        GameObject instance = Instantiate(selectedPrefab, spanwPos, Quaternion.identity);
        probs.Add(instance);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        Vector3 position = new Vector3();
        position.x = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        position.y = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        position.z = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);
        return position;
    }

    public void ResetProbs()
    {
        for(int i = 0; i < probs.Count; ++i)
        {
            probs[i].transform.position = GetRandomPosition();
            probs[i].SetActive(true);
        }
    }
}
