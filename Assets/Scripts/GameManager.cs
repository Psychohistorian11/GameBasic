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

    public int collectedObject = 0;

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
            MenuCanvasManager.sharedInstance.ShowMainMenu();
            MenuCanvasManager.sharedInstance.HideMainGame();
            MenuCanvasManager.sharedInstance.HideMainGameOver();
            //TODO: logica del menú
        } else if(newGameSatate == GameState.inGame)
        {
            //TODO: logic de la partida
            MenuCanvasManager.sharedInstance.ShowMainGame();
            LevelManager.sharedInstance.RemoveAllLevelBlock(); //Se borran todos los antiguas bloques 
            Invoke("RealodLevel", 0.1f); //Retrasar por un momento dichas acciones 

        } else if(newGameSatate == GameState.gameOver)
        {
            //TODO: Game over
            MenuCanvasManager.sharedInstance.HideMainGame();
            MenuCanvasManager.sharedInstance.HideMainMenu();
            MenuCanvasManager.sharedInstance.ShowMainGameOver();
        }

        this.currentGameState= newGameSatate;
    }

    void RealodLevel()
    {
        LevelManager.sharedInstance.GenerateInitialBLock();//se crean los primeros bloques tras el reinicio

        controller.StartGame(); //El personaje tras iniciaro reiniciar
                                //la partida vuelve a jugar 

        MenuCanvasManager.sharedInstance.HideMainMenu();
        MenuCanvasManager.sharedInstance.HideMainGameOver();
    }

    public void CollectObject( collectable collectableType)
    {
        collectedObject += collectableType.value;
    }


}
