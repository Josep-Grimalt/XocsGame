using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChangeCPUPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] cpuPlayers;
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
        if (input.playerIndex == 0)
        {
            return;
        }

        if (input.playerIndex - 1 < cpuPlayers.Length)
        {
            cpuPlayers[input.playerIndex - 1].SetActive(false);
            SceneManager.MoveGameObjectToScene(cpuPlayers[input.playerIndex - 1], SceneManager.GetActiveScene());
        }
    }

    private void ReactivateCPU(PlayerInput input)
    {
        if (input.playerIndex == 0)
        {
            return;
        }

        cpuPlayers[input.playerIndex - 1].SetActive(true);
        DontDestroyOnLoad(cpuPlayers[input.playerIndex - 1]);
    }
}
