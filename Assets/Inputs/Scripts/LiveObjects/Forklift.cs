using System;
using UnityEngine;
using Cinemachine;

namespace Game.Scripts.LiveObjects
{
    public class Forklift : MonoBehaviour
    {
        [SerializeField]
        private GameObject _lift, _steeringWheel, _leftWheel, _rightWheel, _rearWheels;
        [SerializeField]
        private Vector3 _liftLowerLimit, _liftUpperLimit;
        [SerializeField]
        private float _speed = 5f, _liftSpeed = 1f;
        [SerializeField]
        private CinemachineVirtualCamera _forkliftCam;
        [SerializeField]
        private GameObject _driverModel;
        private bool _inDriveMode = false;
        [SerializeField]
        private InteractableZone _interactableZone;

        public static event Action onDriveModeEntered;
        public static event Action onDriveModeExited;

        private PlayerInputActions _input;

        private void OnEnable()
        {
            InteractableZone.onZoneInteractionComplete += EnterDriveMode;
        }
        private void Start()
        {
            _input = new PlayerInputActions();
            _input.Forklift.Enable();
            _input.Forklift.Escape.performed += Escape_performed;
        }



        private void Escape_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (_inDriveMode)
            {
                ExitDriveMode();
            }
        }

        private void EnterDriveMode(InteractableZone zone)
        {
            if (_inDriveMode != true && zone.GetZoneID() == 5) //Enter ForkLift
            {
                _input.Player.Disable();
                _inDriveMode = true;
                _forkliftCam.Priority = 11;
                onDriveModeEntered?.Invoke();
                _driverModel.SetActive(true);
                _interactableZone.CompleteTask(5);
            }
        }

        private void ExitDriveMode()
        {
            _input.Player.Enable();
            _input.Forklift.Disable();
            _inDriveMode = false;
            _forkliftCam.Priority = 9;
            _driverModel.SetActive(false);
            onDriveModeExited?.Invoke();
        }

        private void Update()
        {
            if (_inDriveMode == true)
            {
                LiftControls();
                CalcutateMovement();
                //if (Input.GetKeyDown(KeyCode.Escape))
                //ExitDriveMode();
            }

        }

        private void CalcutateMovement()
        {
            var move = _input.Forklift.Movement.ReadValue<Vector2>();

            transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime * _speed);

            if (Mathf.Abs(move.y) > 0)
            {
                var tempRot = transform.rotation.eulerAngles;
                tempRot.y += move.x * _speed / 2;
                transform.rotation = Quaternion.Euler(tempRot);
            }


            /*float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(0, 0, v);
            var velocity = direction * _speed;

            transform.Translate(velocity * Time.deltaTime);

            if (Mathf.Abs(v) > 0)
            {
                var tempRot = transform.rotation.eulerAngles;
                tempRot.y += h * _speed / 2;
                transform.rotation = Quaternion.Euler(tempRot);
            }*/
        }

        private void LiftControls()
        {
            var lift = _input.Forklift.LiftLower.ReadValue<float>();

            switch (lift)
            {
                case -1:
                    if (_lift.transform.localPosition.y > _liftLowerLimit.y)
                    {
                        Vector3 tempPos = _lift.transform.localPosition;
                        tempPos.y -= Time.deltaTime * _liftSpeed;
                        _lift.transform.localPosition = new Vector3(tempPos.x, tempPos.y, tempPos.z);
                    }
                    else if (_lift.transform.localPosition.y <= _liftUpperLimit.y)
                    {
                        _lift.transform.localPosition = _liftLowerLimit;
                    }
                    break;

                case 1:
                    if (_lift.transform.localPosition.y < _liftUpperLimit.y)
                    {
                        Vector3 tempPos = _lift.transform.localPosition;
                        tempPos.y += Time.deltaTime * _liftSpeed;
                        _lift.transform.localPosition = new Vector3(tempPos.x, tempPos.y, tempPos.z);
                    }
                    else if (_lift.transform.localPosition.y >= _liftUpperLimit.y)
                    {
                        _lift.transform.localPosition = _liftUpperLimit;
                    }
                    break;

                    //LiftUpRoutine();
                    //LiftDownRoutine();
            }
        }

        private void LiftUpRoutine()
        {
            if (_lift.transform.localPosition.y < _liftUpperLimit.y)
            {
                Vector3 tempPos = _lift.transform.localPosition;
                tempPos.y += Time.deltaTime * _liftSpeed;
                _lift.transform.localPosition = new Vector3(tempPos.x, tempPos.y, tempPos.z);
            }
            else if (_lift.transform.localPosition.y >= _liftUpperLimit.y)
                _lift.transform.localPosition = _liftUpperLimit;
        }

        private void LiftDownRoutine()
        {
            if (_lift.transform.localPosition.y > _liftLowerLimit.y)
            {
                Vector3 tempPos = _lift.transform.localPosition;
                tempPos.y -= Time.deltaTime * _liftSpeed;
                _lift.transform.localPosition = new Vector3(tempPos.x, tempPos.y, tempPos.z);
            }
            else if (_lift.transform.localPosition.y <= _liftUpperLimit.y)
                _lift.transform.localPosition = _liftLowerLimit;
        }

        private void OnDisable()
        {
            InteractableZone.onZoneInteractionComplete -= EnterDriveMode;
        }

    }
}