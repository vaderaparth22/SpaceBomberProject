using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/WaveScriptableObject", order = 1)]
    public class SpawnPackage : ScriptableObject
    {
        public EnemySpawn[] enemies;
    }
}