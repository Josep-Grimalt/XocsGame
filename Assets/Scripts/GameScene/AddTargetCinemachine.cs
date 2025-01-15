using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class AddTargetCinemachine : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup targets;
    private PlayerInputManager newPlayer;

    private void Awake()
    {
        newPlayer = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        newPlayer.onPlayerJoined += AddTarget;
    }

    private void OnDisable()
    {
        newPlayer.onPlayerJoined -= AddTarget;
    }

    private void AddTarget(PlayerInput input)
    {
        if (targets != null)
        {
            targets.AddMember(input.gameObject.transform, 0.5f, 1);
        }
    }
}
