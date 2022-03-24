using Managers;
using UnityEngine;

namespace Units
{
    public class RangedEnemy : Unit
    {
        [SerializeField] private float stoppingDistance;
        private Vector3 enemyToPlayerDirection;
        private float shotTime = 0.0f;
        [SerializeField] private float timeBetweenTwoShots;
        [SerializeField] private GameObject AimSight;
        // yet to change based on Bomb Manager As I need Bomb For That thing
        private Vector3 finalBombThrowPosition;
        private Vector3 closestTargetInPlayerDirection;
        private GameObject granade;

        //Increase to fire in player direction with more speed Dont Change Unitl its needed 
        private float firingVelocityOfGranade = 2f;

        public override void InitializeUnit()
        {
            granade = (GameObject)Resources.Load("Prefabs/Bombs/FuseBomb");
            base.InitializeUnit();
        }

        public override void UpdateUnit()
        {
            base.UpdateUnit();
            if (Alive)
            {
                if (PlayerManager.Instance.Player.isActiveAndEnabled)
                {
                    EnemyFire();
                }
                
            }

        }

        protected override Vector3 GetMoveDir()
        {
            Player player = PlayerManager.Instance.Player;
            if (player)
            {
                enemyToPlayerDirection = (player.transform.position - transform.position);
                if (enemyToPlayerDirection.sqrMagnitude >= stoppingDistance && player.Grounded)
                {
                    enemyToPlayerDirection = (PlayerManager.Instance.Player.transform.position - transform.position);
                    if (enemyToPlayerDirection.sqrMagnitude >= stoppingDistance)
                    {
                        return enemyToPlayerDirection;
                    }
                    else
                    {
                        return Vector3.zero;
                    }
                }
                else
                {
                    return Vector3.zero;
                }
            }
            else
            {
                return Vector3.zero;
            }
        }

        protected override Vector3 GetFacingDirection()
        {
            //Player fall Down 
            if (PlayerManager.Instance.Player.transform.position.y > 0f)
            {
                return PlayerManager.Instance.Player.transform.position - transform.position;
            }
            else
            {
                return Vector3.zero;
            }
        }




        private void EnemyFire()
        {
            if (enemyToPlayerDirection.sqrMagnitude <= stoppingDistance)
            {
                finalBombThrowPosition = PlayerManager.Instance.Player.transform.position;
                closestTargetInPlayerDirection = enemyToPlayerDirection.normalized * Random.Range(0.5f, 2f);
                finalBombThrowPosition = finalBombThrowPosition - closestTargetInPlayerDirection;
                shotTime += Time.deltaTime;
                if (shotTime >= timeBetweenTwoShots)
                {
                    Vector3 V0 = CalculateVelocity(finalBombThrowPosition, AimSight.transform.position, 1f);
                    FireAShot(V0);
                    shotTime = 0.0f;
                }
            }
        }

        private void FireAShot(Vector3 V0)
        {
            GameObject bomb = Instantiate(granade, AimSight.transform.position, Quaternion.identity);
            Rigidbody rbGranadeObj = bomb.GetComponent<Rigidbody>();
            rbGranadeObj.velocity = V0 * firingVelocityOfGranade;
        }

        private Vector3 CalculateVelocity(Vector3 nearByPlayerTarget, Vector3 origin, float time)
        {
            Vector3 distance = nearByPlayerTarget - origin;
            //Imagine 3d Where In Y Direction Y0 is 0
            Vector3 distanceXZ = distance;
            //At Initial Point It Should Be Zero
            distanceXZ.y = 0;
            //Calculation For Y
            //distance For Y ;
            float Sy = distance.y;
            //Disttance For X
            float Sxz = distanceXZ.magnitude;
            //Calculate Velocity
            //Velocity x is Distance in X / time
            float Vxz = Sxz / time;
            //Distance in y is Velocity of Y  At Vy0
            float Vy = (Sy / time) + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
            Vector3 resultNormalized = distanceXZ.normalized;
            resultNormalized *= Vxz;
            resultNormalized.y = Vy;
            return resultNormalized;
        }
    }
}
