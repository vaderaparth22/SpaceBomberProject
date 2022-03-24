using System.Collections;
using Managers;
using UnityEngine;

namespace Level
{
    public class GameLevel : MonoBehaviour
    {
        public enum State { NotStarted, Started, Complete }

        private EnemyManager _enemyManager;
        
        [SerializeField] private Wave[] waves;
        
        public int CurrentWave { get; private set; }
        private State _state = State.NotStarted;
        
        public void GameStart()
        {
            _enemyManager = EnemyManager.Instance;
            CurrentWave = 0;
            StartNextWave();
            FirstWaveStarted();
        }

        private void FirstWaveStarted()
        {
            _state = State.Started;
        }

        public void Refresh()
        {
            if (_state < State.Complete && _enemyManager.AllEnemiesAreDead)
            {
                if (CurrentWave < waves.Length)
                    StartNextWave();
                else
                    WaveComplete();
            }
        }

        private void StartNextWave()
        {
            StartCoroutine(StartWave(waves[CurrentWave]));
            CurrentWave++;
        }
        

        IEnumerator StartWave(Wave waveToSpawn)
        {
            _enemyManager.StartWave();
            
            yield return new WaitForSeconds(waveToSpawn.timeUntilStart);
            foreach (var currentEnemy in waveToSpawn.spawnPackage.enemies)
            {
                _enemyManager.SpawnEnemy(currentEnemy);
            }
        }
        
        void WaveComplete()
        {
            UIManager.Instance.GameOver();
        }
    }
}