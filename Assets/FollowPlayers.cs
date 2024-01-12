using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayers : MonoBehaviour
{
    public List<GameObject> players;
    public GameObject scorePanel;
    private float averageX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count > 0)
        {
            averageX = 0;

            for (int i = 0; i < players.Count; i++)
            {
                averageX += players[i].transform.position.x;
            }
            averageX = averageX / players.Count;
            Vector3 newCameraPosition = transform.position;
            newCameraPosition.x = averageX;

            transform.position = newCameraPosition;
        }
    }

    public GameObject whoWon()
    {
        if(players.Count > 0)
        {
            for(int i = 0; i < players.Count; i++)
            {
                if(players[i].GetComponent<Player>().winner)
                {
                    Debug.Log("We found a winner!");
                    return players[i];
                }
            }
        }
        return null;
    }
}
