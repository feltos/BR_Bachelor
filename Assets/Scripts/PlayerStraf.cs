using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerStraf : MonoBehaviour
{
    [SerializeField]Transform posA;
    [SerializeField]Transform posB;
    [SerializeField]Transform posC;

    List<Transform> allPos = new List<Transform>();

    Transform newPoint;
    Transform oldPoint;
    float speed = 1;

    bool atDestination = false;
    void Start()
    {
        newPoint = posB;
        allPos.Add(posA);
        allPos.Add(posB);
        allPos.Add(posC);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPoint.position, Time.deltaTime * speed);

        if (Vector3.Distance(transform.localPosition, newPoint.position) < 0.1f)
        {
            var index = Random.Range(0, allPos.Count);
            newPoint = allPos[index];
            Debug.Log(newPoint);
            speed = Random.Range(1, 3);
        }
    }
}
