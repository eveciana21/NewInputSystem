using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Game.Scripts.UI;

namespace Game.Scripts.LiveObjects
{
    public class Drone : MonoBehaviour
    {
        private enum Tilt
        {
            NoTilt, Forward, Back, Left, Right
        }

        private Tilt _tilt;

        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private float _speed = 5f;
        [SerializeField]
        private float _liftSpeed = 5f;
        [SerializeField]
        private float _strafeSpeed;
        private bool _inFlightMode = false;
        [SerializeField]
        private Animator _propAnim;
        [SerializeField]
        private CinemachineVirtualCamera _droneCam;
        [SerializeField]
        private InteractableZone _interactableZone;


        public static event Action OnEnterFlightMode;
        public static event Action onExitFlightmode;

        private PlayerInputActions _playerInput;


        private void OnEnable()
        {
            InteractableZone.onZoneInteractionComplete += EnterFlightMode;
        }

        private void Start()
        {
            _playerInput = new PlayerInputActions();
            _playerInput.Drone.Enable();
            _playerInput.Drone.Escape.performed += Escape_performed;
        }

        private void Escape_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (_inFlightMode)
            {
                _inFlightMode = false;
                onExitFlightmode?.Invoke();
                ExitFlightMode();
            }

        }

        private void EnterFlightMode(InteractableZone zone)
        {
            if (_inFlightMode != true && zone.GetZoneID() == 4) // drone Scene
            {
                _playerInput.Player.Disable();
                _propAnim.SetTrigger("StartProps");
                _droneCam.Priority = 11;
                _inFlightMode = true;
                OnEnterFlightMode?.Invoke();
                UIManager.Instance.DroneView(true);
                _interactableZone.CompleteTask(4);
            }
        }

        private void ExitFlightMode()
        {
            _playerInput.Player.Enable();
            _playerInput.Drone.Disable();
            _droneCam.Priority = 9;
            _inFlightMode = false;
            UIManager.Instance.DroneView(false);
        }

        private void Update()
        {
            if (_inFlightMode)
            {
                CalculateTilt();
                CalculateRotationUpdate();

                /*if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _inFlightMode = false;
                    onExitFlightmode?.Invoke();
                    ExitFlightMode();
                }*/
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(transform.up * (9.81f), ForceMode.Acceleration);
            if (_inFlightMode)
            {
                CalculateLiftFixedUpdate();
            }
        }

        private void CalculateRotationUpdate()
        {
            var rotation = _playerInput.Drone.Rotation.ReadValue<float>();

            var tempRot = transform.localRotation.eulerAngles;
            tempRot.y = rotation * _speed / 3;
            transform.localRotation = Quaternion.Euler(tempRot);

            /*if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var tempRot = transform.localRotation.eulerAngles;
                tempRot.y -= _speed / 3;
                transform.localRotation = Quaternion.Euler(tempRot);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                var tempRot = transform.localRotation.eulerAngles;
                tempRot.y += _speed / 3;
                transform.localRotation = Quaternion.Euler(tempRot);
            }*/
        }

        private void CalculateLiftFixedUpdate()
        {
            var lift = _playerInput.Drone.UpDown.ReadValue<float>();
            _rigidbody.AddForce(lift * transform.up * _liftSpeed, ForceMode.Acceleration);


            /*if (Input.GetKey(KeyCode.Space))
             {
                 _rigidbody.AddForce(transform.up * _speed, ForceMode.Acceleration);
             }
             if (Input.GetKey(KeyCode.V))
             {
                 _rigidbody.AddForce(-transform.up * _speed, ForceMode.Acceleration);
             }*/
        }



        private void CalculateTilt()
        {
            var tilt = _playerInput.Drone.Movement.ReadValue<Vector3>();

            switch (tilt)

            {
                case Vector3 t when t.Equals(Vector3.left):
                    transform.rotation = Quaternion.Euler(00, transform.localRotation.eulerAngles.y, 30);
                    break;
                case Vector3 t when t.Equals(Vector3.right):
                    transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, -30);
                    break;
                case Vector3 t when t.Equals(Vector3.forward):
                    transform.rotation = Quaternion.Euler(30, transform.localRotation.eulerAngles.y, 0);
                    break;
                case Vector3 t when t.Equals(Vector3.back):
                    transform.rotation = Quaternion.Euler(-30, transform.localRotation.eulerAngles.y, 0);
                    break;
                default:
                    transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);
                    break;
            }

            /*if (Input.GetKey(KeyCode.A))
                 transform.rotation = Quaternion.Euler(00, transform.localRotation.eulerAngles.y, 30);
             else if (Input.GetKey(KeyCode.D))
                 transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, -30);
             else if (Input.GetKey(KeyCode.W))
                 transform.rotation = Quaternion.Euler(30, transform.localRotation.eulerAngles.y, 0);
             else if (Input.GetKey(KeyCode.S))
                 transform.rotation = Quaternion.Euler(-30, transform.localRotation.eulerAngles.y, 0);
             else
                 transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);
            */
        }


        private void OnDisable()
        {
            InteractableZone.onZoneInteractionComplete -= EnterFlightMode;
        }
    }
}
