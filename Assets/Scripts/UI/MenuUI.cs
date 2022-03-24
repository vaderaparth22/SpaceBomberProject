using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MenuUI : UI
{
    private Canvas _menuCanvas;
    [SerializeField] private GameObject gameOverSprite;
    [SerializeField] private GameObject pauseSprite;
    private GameObject _resumeButtonObject;
    private Button _resumeButton;

    public override void Initialize()
    {
        _menuCanvas = GameObject.Find("MenuCanvas").GetComponent<Canvas>();
        _resumeButtonObject = GameObject.Find("ResumeButton");
        _resumeButton = _resumeButtonObject.GetComponent<Button>();
        _resumeButton.interactable = true;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void ExitTheGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    //Resume Game And free All movement;
    public void ResumeGame()
    {
        _menuCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void GetCanvasAndDisable()
    {
        if (_menuCanvas)
        {
            _menuCanvas.enabled = false;
        }
    }


    public void Refresh()
    {
        if (!PlayerManager.Instance.Player.Alive)
        {
            _menuCanvas.enabled = true;
            pauseSprite.SetActive(false);
            gameOverSprite.SetActive(true);
            _resumeButton.interactable = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            _menuCanvas.enabled = true;
            pauseSprite.SetActive(true);
            gameOverSprite.SetActive(false);
            _resumeButtonObject.SetActive(true);
        }
    }
}