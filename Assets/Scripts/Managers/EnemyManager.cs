using System.Collections.Generic;
using System.Linq;
using Level;
using Units;
using UnityEngine;

namespace Managers
{
    public class EnemyManager
    {
        #region Singleton

        private EnemyManager()
        {
        }

        private static EnemyManager _instance;
        public static EnemyManager Instance => _instance ??= new EnemyManager();

        #endregion

        private enum WaveState
        {
            Started,
            Complete
        }

        public bool AllEnemiesAreDead => _waveState == WaveState.Complete;
        public int Score { get; private set; } = 0;

        private const float enemyAgeToScoreRatio = 10f;

        private readonly List<RangedEnemy> _enemies = new List<RangedEnemy>();
        private readonly HashSet<RangedEnemy> _deadEnemies = new HashSet<RangedEnemy>();
        private readonly Dictionary<RangedEnemy, float> _scorePerEnemy = new Dictionary<RangedEnemy, float>();

        private WaveState _waveState = WaveState.Complete;
        private RangedEnemy _rangedEnemyResource;

        public void Initialize()
        {
            _rangedEnemyResource = Resources.Load<RangedEnemy>("Prefabs/RangedEnemy");
        }

        public void SpawnEnemy(EnemySpawn enemySpawn)
        {
            RangedEnemy enemy = 
                GameObject.Instantiate<RangedEnemy>(_rangedEnemyResource, enemySpawn.spawnCoords, Quaternion.identity);
            enemy.InitializeUnit();
            _enemies.Add(enemy);
        }

        public void StartWave()
        {
            _waveState = WaveState.Started;
        }

        public void GameStart()
        {
            _deadEnemies.Clear();
            _enemies.Clear();
            _scorePerEnemy.Clear();
            Score = 0;
        }

        public void Refresh()
        {
            foreach (var aiUnit in _enemies)
            {
                aiUnit.UpdateUnit();
                if (!aiUnit.isActiveAndEnabled && !_deadEnemies.Contains(aiUnit))
                {
                    _deadEnemies.Add(aiUnit);
                    _scorePerEnemy[aiUnit] = enemyAgeToScoreRatio / aiUnit.LifeTime;
                    Score = (int) _scorePerEnemy.Values.Sum();
                    if (_deadEnemies.Count >= _enemies.Count)
                        _waveState = WaveState.Complete;
                }
            }
        }

        public void FixedRefresh()
        {
            foreach (var aiUnit in _enemies)
            {
                aiUnit.FixedUpdateUnit();
            }
        }
    }
}