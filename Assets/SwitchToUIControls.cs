using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwitchToUIControls : MonoBehaviour
{
    public FollowPlayers follower;
    public Button defaultButton;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < follower.players.Count; i++)
        {
            PlayerInput pi = follower.players[i].GetComponent<PlayerInput>();
            pi.DeactivateInput();

            for (int j = 0; j < pi.actionEvents.Count; j++)
            {
                //                pi.DeactivateInput();
            }
        }
        defaultButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
