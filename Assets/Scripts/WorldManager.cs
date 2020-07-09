using CoolBattleRoyaleZone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> buildings = new List<GameObject>();
    float radius;  
    
    void Start()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {           
            players.Add(go);           
        }

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Buildings"))
        {
            buildings.Add(go);       
        }
    }

    void Update()
    {
        players.Clear();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(go);         
        }
        if(players.Count == 1)
        {
            Time.timeScale = 0;
            Debug.Log("END GAME");
        }
    }
}
