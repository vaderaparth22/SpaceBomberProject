using Level;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager
    {
        #region Singleton
        private UIManager() { }
        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UIManager();
                return _instance;
            }
        }
        #endregion

        private MenuUI _menuUI;
        private HudUI _hudUI;
        private GameOverUI _gameOverUI;
        private GameLevel _level;
        public void Initialize(GameLevel level)
        {
            this._level = level;
            _menuUI = GameObject.FindObjectOfType<MenuUI>();
            _hudUI = GameObject.FindObjectOfType<HudUI>();
            _gameOverUI = GameObject.FindObjectOfType<GameOverUI>();
            _menuUI.Initialize();
            _hudUI.Initialize(level);
            _gameOverUI.Initialize();
        }

        public void GameStart()
        {
            _menuUI.GetCanvasAndDisable();
        }

        public void GameOver()
        {
            _gameOverUI.Show();
        }
        
        public void Refresh()
        {
            _menuUI.Refresh();
            _hudUI.Refresh();
        }

       
    }
}
