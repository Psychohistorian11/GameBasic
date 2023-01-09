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
    private bool looking_right = true;
    Rigidbody2D playerrigidbody;
    public LayerMask groundMask;
    Animator animator;

    private const string STATE_ALIVE = "IsALive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";

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
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
    }

    // Update is called once per frame
    void Update()
    {
        //entrada del teclas para realizar una acci�n
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) //Barra Espaciadora - Clicl Izquierdo
            {
            /*se llama al metodo saltar(Jump) que es la acci�n respectiva que realizaran esas
            dos teclas*/
            Jump();
            }

        process_motion();


        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        //rayo que va desde el centro del personaje al suelo
        Debug.DrawRay(this.transform.position, Vector2.down * 1.8f, Color.red);
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
        float inputMovimiento = Input.GetAxis("Horizontal");
        playerrigidbody.velocity = new Vector2(inputMovimiento * runningSpeed, playerrigidbody.velocity.y);

        manage_orientation(inputMovimiento);
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

    void Jump()

    {
        if (IsTouchingTheGround())
        {
            playerrigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
}
