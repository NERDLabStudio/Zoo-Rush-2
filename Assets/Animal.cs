using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float timeBetweenChangesInDirection;
    public float speed;
    public int direction;
    private float nextChangeInDirection;
    public EmitterRandomizer emitter;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Release()
    {
        nextChangeInDirection = Time.time + timeBetweenChangesInDirection;
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            Debug.Log("Starting Direction Left");
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            Debug.Log("Starting Direction Right");
        }

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        Vector3 newPosition = transform.position;
        newPosition.x += (speed * direction);
        transform.position = newPosition;
        //        GetComponent<Rigidbody2D>().velocity = direction;
        if (nextChangeInDirection < Time.time)
        {
            Debug.Log("Flip Direction");
            direction *= -1;
            transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
            nextChangeInDirection = Time.time + timeBetweenChangesInDirection;

            //            SetDirection();
        }

        if (!GetComponent<CapsuleCollider2D>().enabled)
        {
            Debug.Log("Animal Collider Disabled, Removing...");
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {


    }

    void SetDirection()
    {
        Debug.Log("Changing Directions");


    }
    public void Catch()
    {
        //        GetComponent<Animator>().SetTrigger("caught");
        Destroy(this.gameObject, 1f);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Boundary"))
        {
            //            Destroy(this.gameObject) or turn on fade away animation, delayed instantiation?
            Destroy(this.gameObject, 1f);
        }

    }
}