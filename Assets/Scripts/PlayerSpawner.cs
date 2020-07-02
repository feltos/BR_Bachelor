using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField]List<GameObject> buildings;
    [SerializeField]int nmbOfPlayers;
    [SerializeField] GameObject playerPrefab;
    List<int> usedBuildings = new List<int>();
    void Awake()
    {
        for(int i = 0; i < nmbOfPlayers; i++)
        {
            int randomBuilding = Random.Range(0, buildings.Count);
            for(int j = 0; j < usedBuildings.Count; j++)
            {
                if(usedBuildings[j] == randomBuilding)
                {
                    randomBuilding = randomBuilding + 1;
                    break;
                }
            }
            Instantiate(playerPrefab, buildings[randomBuilding].transform.position, buildings[randomBuilding].transform.rotation);
            usedBuildings.Add(randomBuilding);
        }
    }

    void Update()
    {
        
    }
}
