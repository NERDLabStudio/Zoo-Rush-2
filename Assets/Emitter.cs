using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public GameObject objectToEmit;
    public int direction;
    public Transform targetLocationToRunTo;
    public GameObject master;
    

    public GameObject Emit()
    {
        GameObject go = Instantiate(objectToEmit);
        go.GetComponent<Animal>().direction = direction;
        Vector3 animalPosition = transform.position;
        animalPosition.z = go.transform.position.z;
        go.transform.position = animalPosition;
        go.GetComponent<Animal>().Release();
        return go;
    }
}
