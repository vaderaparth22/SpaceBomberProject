using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Managers
{
    public class PlayerManager
    {
        #region Singleton
        private PlayerManager() { }
        private static PlayerManager _instance;
        public static PlayerManager Instance => _instance ??= new PlayerManager();

        #endregion
        public Player Player { get; private set; }
        private GameObject _playerResource;
        private Transform _playerParent;
        private Cinemachine.CinemachineVirtualCamera _vcam;

        public void Initialize(Cinemachine.CinemachineVirtualCamera vcam)
        {
            this._vcam = vcam;
            _playerResource = Resources.Load<GameObject>("Prefabs/Player");
            _playerParent = new GameObject("PlayerParent").transform;
        }

        public void GameStart()
        {
            Player = GameObject.Instantiate(_playerResource, _playerParent).GetComponent<Player>();
            Player.InitializeUnit();
            _vcam.Follow = Player.transform;
        }

        public void Refresh()
        {
           Player.UpdateUnit();
        }

        public void FixedRefresh()
        {
           Player.FixedUpdateUnit();
        }

        public void PlayerDied(Player pDied)
        {
            Debug.Log("GAME OVER");
        }

        public void PlayerFellOffMap()
        {
            Debug.Log("Fell off the map");
        }
    }
}
