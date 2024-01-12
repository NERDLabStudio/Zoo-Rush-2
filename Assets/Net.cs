using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public float velocity;
    public float timePeriodBeforePhysics;
    public float tossUpTime;
    public Sprite netReadyToCatch;
    public Sprite caught;
    public GameObject particleSystem;
    private bool catching;
    private float stopTossUpTime;
    private Rigidbody2D rb2D;
    private float animationTurnOffTime;
    private bool usingPhysics;
    private Vector3 original_scale;
    public Player thrownBy;
    // Start is called before the first frame update
    void Start()
    {
        original_scale = transform.localScale;
        rb2D = GetComponent<Rigidbody2D>();
        stopTossUpTime = Time.time + tossUpTime;
        transform.localScale = new Vector3(velocity * original_scale.x, original_scale.y, original_scale.z);
        catching = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (catching)
        {

        }
        else
        {
            if (stopTossUpTime >= Time.time)
            {
                rb2D.AddForce(new Vector2(velocity, 20));
            }
            else
            {
                rb2D.AddForce(new Vector2(velocity, 0));
                if (GetComponent<SpriteRenderer>().sprite != netReadyToCatch)
                {
                    GetComponent<SpriteRenderer>().sprite = netReadyToCatch;
                }
            }
        }
        if(transform.position.y < -20f)
        {
            Destroy(this.gameObject);
        }
    }

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        
        if (collision.gameObject.tag.Contains("Boundary"))
        {
            //            Destroy(this.gameObject) or turn on fade away animation, delayed instantiation?
            Debug.Log("Colliding with boundary...");
        }

        if (collision.gameObject.tag.Contains("Animal"))
        {
            Instantiate(particleSystem, collision.gameObject.transform);
            GetComponent<CapsuleCollider2D>().enabled = false;
            rb2D.simulated = false;
            Debug.Log("Caught an animal! " + collision.gameObject);
            collision.gameObject.GetComponent<Animal>().Catch();
            GetComponent<SpriteRenderer>().sprite = caught;
            catching = true;
            transform.position = Vector3.zero;
            transform.SetParent(collision.transform, false);
            transform.parent = collision.gameObject.transform;
            if (thrownBy.isPlayerTrackingPoints())
            {
                thrownBy.score.SetScore(1);
                Destroy(this.gameObject, 1);
            }
            else
            {
                //player is in chase mode. they have won.
                thrownBy.CaughtOtherPlayer();
            }

        }


    }

}
