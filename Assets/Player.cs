using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject net;
    public float timeBetweenThrows;
    public GameObject scoreBox;
    public ScoreBox score;
    public Sprite avatar;
    private GameObject character;
    public Transform hand;
    public bool winner = false;
    private float netThrowTimer;
    private float previous_direction;
    private Vector3 original_scale;
    public float moveSpeed;
    private Vector2 movementInput;
    private float goal;
    private bool roundupMode;
    private bool timedRoundup;
    private bool chaseMode;
    private float chaseTimer;
    


    // Start is called before the first frame update
    void Start()
    {
        original_scale = transform.localScale;
        hand = transform.GetChild(0).GetComponent<Transform>();


        //ROUND UP
        if(PlayerPrefs.GetString("mode", "none").Contains("roundup"))
        {
            //SET MODE TO ROUND UP
            roundupMode = true;
            chaseMode = false;
            //CREATE SCORE BOX FOR EACH PLAYER
            GameObject sb = Instantiate(scoreBox, Camera.main.GetComponent<FollowPlayers>().scorePanel.transform);
            Debug.Log("SB: " + sb.name);
            score = sb.GetComponent<ScoreBox>();
            if (PlayerPrefs.GetString("type", "points").Contains("points"))
            {
                //NUMBER OF CAUGHT ANIMALS?
                //POINTS, default 5 if not assigned
                goal = PlayerPrefs.GetInt("goal", 5);
                timedRoundup = false;
            }
            else
            {
                //TIMED
                timedRoundup = true;
            }

        }
        else
        {
            //NOT PLAYING ROUND UP
            //CHASE
            roundupMode = false;
            chaseMode = true;
        }




    }

    public void CaughtOtherPlayer()
    {
        winner = true;
       
    }

    void Update()
    {
        if(roundupMode)
        {
            if(timedRoundup)
            {
                //nothing to do
            }
            else
            {
                //check to see if goal reached
                if(score.getPoints() >= goal)
                {
                    //PLAYER HAS REACHED GOAL, THEY WIN!
                    Debug.Log(gameObject.name + " has won the roundup!");
                    winner = true;
                }
            }
        }

        //ACCOUNTING FOR WEIRD PARALLAX ORDER BEHAVIOR
        if (transform.position.y < 0)
        {
            GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        }
        
        Move(movementInput);
    }

    public bool isPlayerTrackingPoints()
    {
        return roundupMode;
    }
    public void LetsMove(InputAction.CallbackContext context)
    {
        Debug.Log("LETS MOVE: " + context.ReadValue<Vector2>());
        if(context.ReadValue<Vector2>().x == 0)
        {
            GetComponent<Animator>().SetTrigger("stop");
            GetComponent<Animator>().SetBool("running", false);

        }
        else
        {
            //            GetComponent<Animator>().SetTrigger("run");
            GetComponent<Animator>().SetBool("running", true);

        }
        movementInput = context.ReadValue<Vector2>();
    }

    private void Move(Vector2 direction)
    {
        var scaledMoveSpeed = moveSpeed * .01f;

        // For simplicity's sake, we just keep movement in a single plane here. Rotate
        // direction according to world Y rotation of player.
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
//        pos.y = Mathf.Clamp01(pos.y);
        if (pos.x == 0 || pos.x == 1)
        {
            //GOING OFF SCREEN!
            Debug.Log("I'm off screen!");
            //Show Icon?
        }

        Vector3 move = Quaternion.Euler(0, 0, 0) * new Vector3(direction.x, direction.y, 0);
        Vector3 newPosition = transform.position;
        newPosition.x += (direction.x * scaledMoveSpeed);
        newPosition.y += (direction.y * scaledMoveSpeed);
        transform.position = newPosition;

        if (direction.x < 0)
        {
            if(previous_direction != -1)
            {
                transform.localScale = new Vector3(-1 * original_scale.x, original_scale.y, original_scale.z);
                previous_direction = -1;

            }
        }
        else if(direction.x > 0)
        {
            if(previous_direction != 1)
            {
                transform.localScale = new Vector3(1 * original_scale.x, original_scale.y, original_scale.z);
                previous_direction = 1;
            }

        }

    if(direction.x == 0)
        {
            Debug.Log("STOP!");
            GetComponent<Animator>().SetTrigger("stop");
        }

    }
    public void ThrowNet()
    {
        GetComponent<Animator>().SetTrigger("throw");
        if (netThrowTimer <= Time.time)
        {
            netThrowTimer = Time.time + timeBetweenThrows;
            GameObject thrownNet = (GameObject)Instantiate(net);
            thrownNet.transform.position = hand.position;
            //            thrownNet.transform.position = transform.position + transform.forward * .06f;
            thrownNet.GetComponent<Net>().velocity = previous_direction;
            thrownNet.GetComponent<Net>().thrownBy = this;
            thrownNet.GetComponent<Rigidbody2D>().AddForce(new Vector2(previous_direction * 20f, 0), ForceMode2D.Impulse);

        }

    }


}
