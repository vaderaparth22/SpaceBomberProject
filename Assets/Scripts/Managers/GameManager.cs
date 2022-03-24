using Level;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager
    {
        #region Singleton
        private GameManager() { }
        private static GameManager _instance;
        public static GameManager Instance => _instance ??= new GameManager();

        #endregion

        private GameLevel _level;

        public void Initialize(GameLevel level, Cinemachine.CinemachineVirtualCamera vcam )
        {
            this._level = level;
            
            PlayerManager.Instance.Initialize(vcam);
            EnemyManager.Instance.Initialize();
            BombManager.Instance.Initialize();
        }

        public void Refresh()
        {
            _level.Refresh();
            
            PlayerManager.Instance.Refresh();
            EnemyManager.Instance.Refresh();
            BombManager.Instance.Refresh();
        }

        public void FixedRefresh()
        {
            
            PlayerManager.Instance.FixedRefresh();
            EnemyManager.Instance.FixedRefresh();
            BombManager.Instance.FixedRefresh();
        }
        
        public void ChangeToMainMenu()
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        public void GameStart()
        {   
            _level.GameStart();
            PlayerManager.Instance.GameStart();
            EnemyManager.Instance.GameStart();
            BombManager.Instance.GameStart();
        }

        public void ChangeToGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
