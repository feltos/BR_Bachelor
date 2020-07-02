using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField]List<GameObject> buildings;
    [SerializeField]int nmbOfPlayers;
    [SerializeField] GameObject playerPrefab;
    void Awake()
    {
        for(int i = 0; i < nmbOfPlayers; i++)
        {
            int randomBuilding = Random.Range(0, buildings.Count);
            Instantiate(playerPrefab, buildings[randomBuilding].transform.position, buildings[randomBuilding].transform.rotation);
        }
    }

    void Update()
    {
        
    }
}
