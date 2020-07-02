using CoolBattleRoyaleZone;
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
    float randomSpeed;
    int range;

    float waitPeriod;
    float waitTimer;
    bool atDestination = false;

    [SerializeField]float playerRange;
    public List<GameObject> otherPlayers = new List<GameObject>();

    float rotationSpeed = 6;
    Vector3 direction;
    Quaternion rotation;
    Light light;

    bool fight = false;

    int nmbOfPlayers;

    // Start is called before the first frame update
    void Start()
    {
        buildings = GameObject.FindGameObjectsWithTag("Buildings");
        range = Random.Range(0, buildings.Length);
        closestBuilding = buildings[range].transform;
        collider = GetComponent<CapsuleCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        randomSpeed = Random.Range(10, 20);
        speed = randomSpeed;
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
        otherPlayers.Clear();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (!go.Equals(this.gameObject))
            {
                otherPlayers.Add(go);
            }
        }

        if(!fight)
        {
            speed = randomSpeed;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, closestBuilding.transform.position, Time.deltaTime * speed);
            direction = closestBuilding.transform.position - transform.position;
            rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.localPosition,closestBuilding.transform.position) < 0.1f)
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
                    randomSpeed = Random.Range(10, 20);
                    atDestination = false;
                }
            }
        }

        //Check for others players in range
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            if (Vector3.Distance(transform.localPosition, otherPlayers[i].transform.position) <= playerRange)
            {
                fight = true;
                speed = 0;
                direction = otherPlayers[i].transform.position - transform.position;
                rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
        }


       Vector3 target = RandomSpotLightCirclePoint(light);
       Debug.DrawLine(transform.position, target, Color.red, 0.05f);
       RaycastHit []hit = Physics.RaycastAll(transform.position, target - transform.position, Vector3.Distance(transform.position, target));
       foreach(RaycastHit rayHit in hit)
        {
            if (rayHit.transform.gameObject != gameObject)
            {
                for(int i = 0; i < otherPlayers.Count; i++)
                {
                    if(otherPlayers[i] == rayHit.transform.gameObject)
                    {
                        otherPlayers[i].GetComponent<SimpleHealth>().Health -= 1;
                        if (otherPlayers[i].GetComponent<SimpleHealth>().Health <= 0)
                        {
                            fight = false;
                        }
                    }
                }
            }
            else
            {

            }
        }

       
    }

    Vector3 RandomSpotLightCirclePoint(Light spot)
    {
        float radius = Mathf.Tan(Mathf.Deg2Rad * spot.spotAngle / 2) * spot.range;
        Vector2 circle = Random.insideUnitCircle * radius;
        Vector3 target = spot.transform.position + spot.transform.forward * spot.range + spot.transform.rotation * new Vector3(circle.x, circle.y);
        return target;
    }
}
