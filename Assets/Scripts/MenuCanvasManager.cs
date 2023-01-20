using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasManager : MonoBehaviour
{
    public static MenuCanvasManager sharedInstance;
    public Canvas gameOverCanva;
    public Canvas gameCanvas;
    public Canvas menuCanvas;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }
    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }
    public void ShowMainGame()
    {
        gameCanvas.enabled = true;
    }
    public void HideMainGame()
    {
        gameCanvas.enabled = false;
    }
    public void ShowMainGameOver()
    {
        gameOverCanva.enabled = true;
    }
    public void HideMainGameOver()
    {
        gameOverCanva.enabled = false;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
