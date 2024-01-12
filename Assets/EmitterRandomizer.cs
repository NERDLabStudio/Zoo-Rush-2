using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterRandomizer : MonoBehaviour
{
    public Emitter[] emitters;
    public float timeBetweenEmits;
    public int maxNumberOfEmitted;
    private float timeForNextEmit;
    // Start is called before the first frame update
    void Start()
    {
        timeForNextEmit = timeBetweenEmits + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeForNextEmit && transform.childCount <= maxNumberOfEmitted)
        {
                int selectedEmitter = Random.Range(0, emitters.Length);
                GameObject e = emitters[selectedEmitter].Emit();
                e.transform.parent = transform;
                timeForNextEmit = timeBetweenEmits + Time.time;
        }
    }

}
