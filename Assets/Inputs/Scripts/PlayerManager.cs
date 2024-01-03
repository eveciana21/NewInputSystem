using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.Player;


public class PlayerManager : MonoBehaviour
{
    private PlayerInputActions _input;
    private Player _player;


    void Start()
    {
        InitializeInputs();
        _player = GetComponentInChildren<Player>();
    }

    private void InitializeInputs()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
    }

    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        var move = _input.Player.Movement.ReadValue<Vector3>();
        if (_player != null)
        {
            _player.Movement(move);
        }
    }

    private void Rotate()
    {
        var rotate = _input.Player.Rotate.ReadValue<float>();
        if (_player != null)
        {
            _player.Rotation(rotate);
        }
    }

}

