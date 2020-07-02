using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    GameObject[] buildings;
    Transform closestBuilding;
    Collider collider;
    Collider sphereCollider;

    float speed;
    int range;

    float waitPeriod;
    float waitTimer;
    bool atDestination = false;

    float playerRange = 10;
    public List<GameObject> otherPlayers = new List<GameObject>();

    float rotationSpeed = 3;
    Vector3 direction;
    Quaternion rotation;
    Light light;

    // Start is called before the first frame update
    void Start()
    {
        buildings = GameObject.FindGameObjectsWithTag("Buildings");
        range = Random.Range(0, buildings.Length);
        closestBuilding = buildings[range].transform;
        collider = GetComponent<CapsuleCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        speed = Random.Range(10, 20);
        light = GetComponent<Light>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
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
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, closestBuilding.transform.position, Time.deltaTime * speed);
        direction = closestBuilding.transform.position - transform.position;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.localPosition,closestBuilding.transform.position) < 0.1f)
        {
            waitTimer = Random.Range(2, 7);
            atDestination = true;

            if (atDestination)
            {
                speed = 0;
                waitPeriod += Time.deltaTime;
                if (waitPeriod >= waitTimer)
                {
                    range = Random.Range(0, buildings.Length);
                    closestBuilding = buildings[range].transform;
                    waitPeriod = 0;
                    speed = Random.Range(5, 8);
                    atDestination = false;
                }
            }
        }
        //for (int i = 0; i < otherPlayers.Count; i++)
        //{
        //    if (Vector3.Distance(transform.localPosition, otherPlayers[i].transform.position) <= playerRange)
        //    {
        //        speed = 0;
        //        direction = otherPlayers[i].transform.position - transform.position;
        //        rotation = Quaternion.LookRotation(direction);
        //        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);               
        //    }
        //}
        Vector3 target = RandomSpotLightCirclePoint(light);
        Debug.DrawLine(target, target + Vector3.one * 0.01f, Color.red, 0.5f);
    }

    Vector3 RandomSpotLightCirclePoint(Light spot)
    {
        float radius = Mathf.Tan(Mathf.Deg2Rad * spot.spotAngle / 2) * spot.range;
        Vector2 circle = Random.insideUnitCircle * radius;
        Vector3 target = spot.transform.position + spot.transform.forward * spot.range + spot.transform.rotation * new Vector3(circle.x, circle.y);
        return target;
    }
}
