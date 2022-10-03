using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private List<GameObject> persons = new List<GameObject>();
    private GameObject closestPrson;
    private GameObject currentPerson;
    private Spawner spawner;

    private void Start()
    {
        spawner = GetComponent<Spawner>();
        persons.AddRange(spawner.Persons);
        foreach (GameObject item in persons)
        {
            item.GetComponent<Person>().FindNew += FindNewOrAddToList;
        }
        SetCurrentPerson();
    }

    private GameObject FindClosestPerson(GameObject currentGO)
    {
        float distance = Mathf.Infinity;
        Vector3 position = currentGO.transform.position;
        if(persons.Count == 0)
        {
            return null;
        }
        foreach (GameObject go in persons)
        {
            if (go == null) continue;
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestPrson = go;
                distance = curDistance;
            }
        }
        closestPrson.GetComponent<Person>().Target = currentGO;
        persons.Remove(closestPrson);
        return closestPrson;
    }

    public void SetCurrentPerson()
    {
        for (int i = 0; i < persons.Count;)
        {
            if (persons[i] == null) break;
            currentPerson = persons[i];
            persons.Remove(currentPerson);
            currentPerson.GetComponent<Person>().Target = FindClosestPerson(currentPerson);
        }
    }

    public void FindNewOrAddToList(GameObject currentGO)
    {
        currentGO.GetComponent<Person>().Target = FindClosestPerson(currentGO);
        if(currentGO.GetComponent<Person>().Target == null)
        {
            persons.Add(currentGO);
        }
    }
}
