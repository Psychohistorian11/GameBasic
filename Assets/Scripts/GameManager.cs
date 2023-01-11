using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
        menu,
        inGame,
        gameOver

}

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public GameState currentGameState = GameState.menu;
    // Start is called before the first frame update

    private PlayerController controller; 

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        
    }
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetButtonDown("Submit") && currentGameState != GameState.inGame)//si se oprime la tecla "Enter" se iniciara el juego
        {
            StartGame();
        }

    }

    //Metodo para iniciar el juego
    public void StartGame()
    {
        SetGameState(GameState.inGame); //Inica el juego
    }

    //Metodo para cuando la partida hay sido finalizada
    public void GameOver() 
    {
        SetGameState(GameState.gameOver);//Se acaba el juego
    }
    
    //Metodo para volver al menú
    public void BackToMenu()
    {
        SetGameState(GameState.menu);//Vuelve al menú
    }

    private void SetGameState(GameState newGameSatate)
    {
        if(newGameSatate == GameState.menu)
        {
            //TODO: logica del menú
        } else if(newGameSatate == GameState.inGame)
        {
            //TODO: logic de la partida
            controller.StartGame();
        } else if(newGameSatate == GameState.gameOver)
        {
            //TODO: Game over
        }

        this.currentGameState= newGameSatate;
    }

    
}
