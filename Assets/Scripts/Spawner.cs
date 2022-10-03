using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject personPref;
    [SerializeField] private int personsCount;
    private List<GameObject> persons = new List<GameObject>();
    public List<GameObject> Persons { get => persons; set => persons = value; }
    private Creator creator;

    private void Awake()
    {
        creator = GetComponent<Creator>();
        for (int i = 0; i < personsCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-creator.PlaneSize.x, creator.PlaneSize.x), 0.5f, Random.Range(-creator.PlaneSize.y, creator.PlaneSize.y)) * 5;
            GameObject go = Instantiate(personPref, spawnPosition, Quaternion.identity);
            persons.Add(go);
        }
    }
}
