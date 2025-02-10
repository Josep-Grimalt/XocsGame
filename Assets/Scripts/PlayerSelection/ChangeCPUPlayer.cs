using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCPUPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] cpuPlayers;
    private int activePlayers = 0;
    private PlayerInputManager newPlayer;

    private void Awake()
    {
        newPlayer = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        newPlayer.onPlayerJoined += DeactivateCPU;
        newPlayer.onPlayerLeft += ReactivateCPU;
    }

    private void OnDisable()
    {
        newPlayer.onPlayerJoined -= DeactivateCPU;
        newPlayer.onPlayerLeft -= ReactivateCPU;
    }

    private void DeactivateCPU(PlayerInput input)
    {
        if(activePlayers < cpuPlayers.Length)
        {
            cpuPlayers[activePlayers].SetActive(false);
            activePlayers++;
        }
    }

    private void ReactivateCPU(PlayerInput input)
    {
        activePlayers--;
        cpuPlayers[activePlayers].SetActive(true);
    }
}
