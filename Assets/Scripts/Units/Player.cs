using UnityEngine;
using Managers;

namespace Units
{
    public class Player : Unit
    {
        private Vector3 startPoint;
        private Vector3 endPoint;
        private Vector3 direction;
        private float distance;
        
        /*[SerializeField] private int health;*/
        [SerializeField] private AudioClip shootSoundClip;
        
        public BombType equippedBombType { get; set; }
        private SimpleCameraShake simpleCameraShake;
        private AudioSource weaponAudioSource;

        public override void InitializeUnit()
        {
            base.InitializeUnit();
            simpleCameraShake = GameObject.FindObjectOfType<SimpleCameraShake>();
            weaponAudioSource = GetComponent<AudioSource>();
        }

        public override void UpdateUnit()
        {
            base.UpdateUnit();

            if (AmmoManager.Instance.IsBombEquiped)
            {
                if (InputManager.Instance.IsLeftMouseButtonHolding)
                {
                    startPoint = transform.position;
                    endPoint = InputManager.Instance.GetDirectionToMousePosition();
                    direction = (endPoint - startPoint).normalized;
                }

                if (InputManager.Instance.IsLeftMouseButtonUp)
                {
                    AimAndShoot();
                }
            }
        }

        protected override Vector3 GetMoveDir()
        {
            return InputManager.Instance.GetMovementVector();
        }

        protected override Vector3 GetFacingDirection()
        {
            return InputManager.Instance.GetDirectionToMousePosition();
        }

        private void AimAndShoot()
        {
            distance = Vector3.Distance(startPoint, endPoint);
            BombManager.Instance.ThrowBomb(equippedBombType, direction, shootPosition.position, transform.forward, distance);
            AmmoManager.Instance.IsBombEquiped = false;
            simpleCameraShake.ShakeCamera();
            weaponAudioSource.PlayOneShot(shootSoundClip);
        }
    }
}
