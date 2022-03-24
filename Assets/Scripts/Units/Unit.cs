using System;
using UnityEngine;

namespace Units
{
    public enum UnitType
    {
        RangedEnemy,
        Player
    }

    public abstract class Unit : MonoBehaviour
    {
        public bool Alive { get; private set; } = true;

        [SerializeField] private int health;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float maxMagnitude;
        [SerializeField] private float killDepth = -26f;
        [SerializeField] protected Transform shootPosition;

        [SerializeField] private float groundDetectionRange = 10f;

        private float _rbDragValue;

        private Animator _animator;
        private Rigidbody _rb;
        private LayerMask groundLayerMask;
        private static readonly int XSpeed = Animator.StringToHash("xSpeed");
        private static readonly int YSpeed = Animator.StringToHash("ySpeed");

        public bool Grounded { get; private set; } = true;
        
        private int _health;
        public int Health
        {
            get => _health;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException($"Health must not be negative");

                _health = value;
            }
        }

        public float LifeTime { get; private set; } = 0;

        public virtual void InitializeUnit()
        {
            Alive = true;
            Health = health;
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();

            if (_rb)
                _rbDragValue = _rb.drag;

            groundLayerMask = LayerMask.GetMask("Ground");
        }

        public virtual void UpdateUnit()
        {
            if (!isActiveAndEnabled)
                return;

            LifeTime += Time.deltaTime;
            Grounded = IsGrounded();

            UpdateDrag();
           
            if (Grounded)
            {
                Vector3 moveDir = GetMoveDir();
                UpdateAnimator(moveDir);
            }

            if (transform.position.y <= killDepth)
                Kill();
        }

        public void FixedUpdateUnit()
        {
            if (Grounded)
            {
                Vector3 moveDir = GetMoveDir();
                Move(moveDir);

                Vector3 rotDir = GetFacingDirection();
                Rotate(rotDir);
            }
        }

        protected abstract Vector3 GetMoveDir();

        protected abstract Vector3 GetFacingDirection();

        protected virtual void UpdateAnimator(Vector3 dir)
        {
            Vector3 animVector = transform.InverseTransformDirection(dir);
            animVector.x = Mathf.Clamp(animVector.x, -1f, 1f);
            animVector.z = Mathf.Clamp(animVector.z, -1f, 1f);

            _animator.SetFloat(XSpeed, animVector.x, 0.1f, Time.deltaTime);
            _animator.SetFloat(YSpeed, animVector.z, 0.1f, Time.deltaTime);
        }

        private void Move(Vector3 dir)
        {
            //_rb.velocity = dir.normalized*speed;
            //transform.position += dir * speed * Time.deltaTime;

            if (_rb.velocity.sqrMagnitude < maxMagnitude)
                _rb.AddForce(dir * speed);
        }

         public void TakeDamage(int damegeAmount)
         {
            Health = Math.Max(0, Health - damegeAmount);
            if (Health <= 0)
            {
                Kill();
            }
         }


        private void Rotate(Vector3 dir)
        {
            if (dir != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = targetRotation;
            }
        }

        /// <summary>
        /// Changes the drag value depending on if the unit is on ground or not
        /// </summary>
        private void UpdateDrag()
        {
            if (Grounded)
                _rb.drag = _rbDragValue;
            else
                _rb.drag = 0;
        }
        
        /// <summary>
        /// Check if the unit is on ground
        /// </summary>
        /// <returns></returns>
        private bool IsGrounded() =>
            Physics.Raycast(transform.position, Vector3.down, out RaycastHit rayHit, groundDetectionRange,
                groundLayerMask);


        protected void Kill()
        {
            Alive = false;
            gameObject.SetActive(false);
           
        }
    }
}