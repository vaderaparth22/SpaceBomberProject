using UnityEngine;
namespace Bombs
{
    public class FuseBomb : Bomb
    {
        [SerializeField] private float FuseTime = 2f;
        private float timeToExplode;

        protected override void Initialize()
        {
            timeToExplode = Time.time + FuseTime;
        }

        protected override void Refresh()
        {
            if (Time.time >= timeToExplode)
            {
                Explode();
            }
        }
    }
}