using Unity.VisualScripting;
using UnityEngine;

namespace Tank
{
    public class TankMovement : MonoBehaviour
    {
        public int PlayerNumber = 1;         
        public float Speed = 12f;            
        public float TurnSpeed = 180f;       
        public AudioSource MovementAudio;    
        public AudioClip EngineIdling;       
        public AudioClip EngineDriving;      
        public float PitchRange = 0.2f;

        private string _movementAxisName;     
        private string _turnAxisName;         
        private Rigidbody _rigidbody;         
        private float _movementInputValue;    
        private float _turnInputValue;        
        private float _originalPitch;         


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        
        private void FixedUpdate()
        {
            Move();
            Turn();
        }

        
        private void Start()
        {
            _movementAxisName = "Vertical" + PlayerNumber;
            _turnAxisName = "Horizontal" + PlayerNumber;

            _originalPitch = MovementAudio.pitch;
        }

        private void Update()
        {
            _movementInputValue = Input.GetAxis(_movementAxisName);
            _turnInputValue = Input.GetAxis(_turnAxisName);
            
            EngineAudio();
        }
        
        private void EngineAudio()
        {
            if (Mathf.Abs(_movementInputValue) < 0.1f && Mathf.Abs(_turnInputValue) < 0.1f) 
            {
                if (MovementAudio.clip == EngineDriving)
                {
                    MovementAudio.clip = EngineIdling;
                    MovementAudio.pitch = Random.Range(_originalPitch - PitchRange, _originalPitch + PitchRange);
                    MovementAudio.Play();
                }
            }
            else
            {
                if (MovementAudio.clip == EngineIdling)
                {
                    MovementAudio.clip = EngineDriving;
                    MovementAudio.pitch = Random.Range(_originalPitch - PitchRange, _originalPitch + PitchRange);
                    MovementAudio.Play();
                }
            }
        }

        private void Move()
        {
            Vector3 movement = transform.forward * _movementInputValue * Speed * Time.fixedDeltaTime;

            _rigidbody.MovePosition(_rigidbody.position + movement);
        }


        private void Turn()
        {
            float turn = _turnInputValue * TurnSpeed * Time.fixedDeltaTime;

            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);
        }
        
        private void OnEnable()
        {
            _rigidbody.isKinematic = false;
            _movementInputValue = 0f;
            _turnInputValue = 0f;
        }


        private void OnDisable()
        {
            _rigidbody.isKinematic = true;
        }
    }
}


