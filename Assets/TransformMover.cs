using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMover : MonoBehaviour
{
    public float speed;
    public bool moving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            Vector3 newPosition;
            newPosition = gameObject.transform.position;
            newPosition.x += speed;
            gameObject.transform.position = newPosition;
        }
    }
}
