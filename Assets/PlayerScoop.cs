using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoop : MonoBehaviour
{
    public bool lookingForPlayers = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        if(gos.Length > 0)
        {
            for (int i = 0; i < gos.Length; i++)
            {
                    gos[i].transform.SetParent(transform);
            }

        }

    }
}
