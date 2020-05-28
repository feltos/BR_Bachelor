using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    GameObject[] buildings;
    Transform closestBuilding;
    Collider collider;
    Collider sphereCollider;

    Vector3 direction;
    float speed;
    int range;

    float waitPeriod;
    float waitTimer;
    bool atDestination = false;

    float playerRange = 10;
    public List<GameObject> otherPlayers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        buildings = GameObject.FindGameObjectsWithTag("Buildings");
        range = Random.Range(0, buildings.Length);
        closestBuilding = buildings[range].transform;
        collider = GetComponent<CapsuleCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (!go.Equals(this.gameObject))
            {
                otherPlayers.Add(go);
            }    
        }
    }

    // Update is called once per frame
    void Update()
    {
        speed = Random.Range(5, 8);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, closestBuilding.transform.position, Time.deltaTime * speed);
        if(Vector3.Distance(transform.localPosition,closestBuilding.transform.position) < 0.1f)
        {
            waitTimer = Random.Range(2, 7);
            atDestination = true;

            if (atDestination)
            {
                waitPeriod += Time.deltaTime;
                if (waitPeriod >= waitTimer)
                {
                    range = Random.Range(0, buildings.Length);
                    closestBuilding = buildings[range].transform;
                    waitPeriod = 0;
                    atDestination = false;
                }
            }
        }
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            if (Vector3.Distance(transform.localPosition, otherPlayers[i].transform.position) <= playerRange)
            {
                Debug.Log("IN RANGE");
            }

        }
    }
}
