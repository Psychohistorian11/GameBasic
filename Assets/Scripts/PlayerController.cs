using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


  //Variables del movimiento del personaje

    public float jumpForce = 6f;
  //public float runningSpeed_right = 2f;
    public float runningSpeed = 4f;
    Vector3 startPosition;
    private bool looking_right = true;
    Rigidbody2D playerrigidbody;
    public LayerMask groundMask;
    Animator animator;

    private int healtPoints, manaPoints;

    private const string STATE_ALIVE = "IsALive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";

    public const int INITAL_HEALTH = 100, INITAL_MANA = 15,
        MAX_HEALTH = 200, MAX_MANA = 30, MIN_HEALTH = 10,
        MIN_MANA = 0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        //cargar componentes 
        playerrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        //Inicializar las variables al inicio del juego
        
        startPosition= this.transform.position;
    }

    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        healtPoints = INITAL_HEALTH;
        manaPoints = INITAL_MANA;
        
        Invoke("RestartPosition", 0.7f);
    }

    void RestartPosition()
    {
        this.transform.position = startPosition;
        this.playerrigidbody.velocity = Vector2.zero;

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //entrada del teclas para realizar una acci�n
        
        if (Input.GetButtonDown("Jump")) //Barra Espaciadora - Clicl Izquierdo
            {
            /*se llama al metodo saltar(Jump) que es la acci�n respectiva que realizaran esas
            dos teclas*/
            Jump(false);
            }
        if (Input.GetButtonDown("SuperJump"))
            {
                Jump(true);
            }


        process_motion();


        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        //rayo que va desde el centro del personaje al suelo
        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
    }
    // Creaci�n de movimiento
    /*void FixedUpdate()
    {
        if(playerrigidbody.velocity.x < runningSpeed_right) 
        {
            playerrigidbody.velocity = new Vector2(runningSpeed_right, // en el eje x
                                                   playerrigidbody.velocity.y // en el eje y
                                                                             );        
        }
    }*/

    void process_motion()
    {
        //logica de movimiento.
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) //Si el juego esta "inGame" se realiza la logica de movimiento
        {
            float inputMovimiento = Input.GetAxis("Horizontal");
            playerrigidbody.velocity = new Vector2(inputMovimiento * runningSpeed, playerrigidbody.velocity.y);

            manage_orientation(inputMovimiento);
        }
        else
        {
            playerrigidbody.velocity = new Vector2(0,playerrigidbody.velocity.y);
        }
    }


    void manage_orientation(float inputMovimiento)
    {
        //Si se cumple condici�n
            //personaje derecha y jugador izquierda  o personaje izquierda y jugador derecha
        if (looking_right == true && inputMovimiento < 0 || looking_right == false && inputMovimiento > 0)
        {
            looking_right = !looking_right;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
            
    }

    void Jump(bool superjump)
    {
        float jumpForceFactor = jumpForce;

        if (superjump&&manaPoints >= SUPERJUMP_COST)
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)//Si el juego esta "inGame" se realiza la logica de salto
        {

            if (IsTouchingTheGround())
            {
                playerrigidbody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            }
        }
    }
    //Nos indica si el personaje esta tocando o no el suelo.
    bool IsTouchingTheGround()
    {
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 1.8f, groundMask ))
        {
            return true;
        }
        else
        {
            return false;
        }
     
    }

    public void Die()
    {

        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if(travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }

    public void CollectHealth(int points)
    {
        this.healtPoints += points;
        if(this.healtPoints >= MAX_HEALTH)
        {
            this.healtPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if(this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealt()
    {
        return healtPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }
}
