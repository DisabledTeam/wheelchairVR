using NikitaPathFinding.Scripts.ModularCharController;
using UnityEngine;

namespace NikitaPathFinding.Scripts.Units
{
    public class Unit : MonoBehaviour
    {

        [SerializeField] private MovePositionPathfinding _positionPathfinding;
    
    
    
        private GameObject _selectedVisuals;
        private MovePositionDirect _movePositionDirect;
        private MovePositionPathfinding _movePositionPathfinding;
        private Rigidbody2D _rb;

    

        [SerializeField] private int _maximumHP;
        private int currentHP;

        private Animator _animator;

        private void Awake()
        {
            currentHP = _maximumHP;
            _animator = transform.Find("Visuals").GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            //_movePositionDirect = GetComponent<MovePositionDirect>();
            //_movePositionPathfinding = GetComponent<MovePositionPathfinding>();

            _selectedVisuals = transform.Find("SelectedVisuals").gameObject;
            SetSelectedVisualsVisible(false);
        }

        public void SetSelectedVisualsVisible(bool visible)
        {
            _selectedVisuals.SetActive(visible);
        }

        public void MoveTo(Vector3 targetPosition)
        {
            _positionPathfinding.SetMovePosition(targetPosition);
        }

        private void Update()
        {
            //Animate();
            CheckDamage();
        }

        /* Анимейт для MovePositionDirect

    private void Animate()
    {
        if(_movePositionDirect.moveDirection != Vector3.zero)
        {
            _animator.SetFloat("SpeedX", _movePositionDirect.moveDirection.x);
            _animator.SetFloat("SpeedY", _movePositionDirect.moveDirection.y);
        }

        _animator.SetFloat("MoveSpeed", _rb.velocity.magnitude);
    }

    */

        /* Анимейт для пасфайдинга

    private void Animate()
    {
        if (_movePositionPathfinding.moveDirection != Vector3.zero)
        {
            _animator.SetFloat("SpeedX", _movePositionPathfinding.moveDirection.x);
            _animator.SetFloat("SpeedY", _movePositionPathfinding.moveDirection.y);
        }

        _animator.SetFloat("MoveSpeed", _rb.velocity.magnitude);
    }
    */

        public void TakeDamage(int hp)
        {
            currentHP -= hp;
            if (currentHP <= 0)
            {
                _animator.SetTrigger("Dead");
            }
        }

        void CheckDamage()
        {
            if (Input.GetMouseButtonDown(2))
            {
                TakeDamage(2);
                Debug.Log(this.gameObject.name + " " + currentHP);
            }
        }

    }
}
