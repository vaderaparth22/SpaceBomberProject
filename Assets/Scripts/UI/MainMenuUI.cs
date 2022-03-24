using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenuUI : UI
{
   
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            UnityEngine.Application.Quit(); 
        #endif
    }
}
