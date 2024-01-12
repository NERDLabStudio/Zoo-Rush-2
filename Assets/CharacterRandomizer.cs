using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterRandomizer : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private int selectedCharacterIndex = 0;
    public PlayerInputManager manager;
    public FollowPlayers follower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerJoined()
    {
        Debug.Log("NEW PLAYER JOINED");
        
        selectedCharacterIndex++;
        if(selectedCharacterIndex > characterPrefabs.Length)
        {
            selectedCharacterIndex = 0;
        }
        manager.playerPrefab = characterPrefabs[selectedCharacterIndex];

        follower.players = new List<GameObject>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < gos.Length; i++)
            {
                follower.players.Add(gos[i]);
            }

    }
}
