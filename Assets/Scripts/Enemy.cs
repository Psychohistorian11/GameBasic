using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float runningSpeed = 1.5f;

    Rigidbody2D enemyRigidbody;

    public bool facingRight = false;

    private Vector3 startPosition;
    
    public int enemyDamage = 10;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;

        if (facingRight)
        {
            //mirando hacia la derecha
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 100, 0);

        } else
        {
            //mirando hacia la izquierda
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            enemyRigidbody.velocity = new Vector2(currentRunningSpeed, enemyRigidbody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            return;
        }
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);
            return;
        }
        facingRight = !facingRight;
        
    }
}
