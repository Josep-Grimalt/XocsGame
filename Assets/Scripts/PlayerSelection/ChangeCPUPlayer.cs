using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        if (activePlayers == 0)
        {
            activePlayers++;
            return;
        }

        if (activePlayers < cpuPlayers.Length)
        {
            cpuPlayers[activePlayers - 1].SetActive(false);
            SceneManager.MoveGameObjectToScene(cpuPlayers[activePlayers - 1], SceneManager.GetActiveScene());
            activePlayers++;
        }
    }

    private void ReactivateCPU(PlayerInput input)
    {
        activePlayers--;
        cpuPlayers[activePlayers - 1].SetActive(true);
        DontDestroyOnLoad(cpuPlayers[activePlayers - 1]);
    }
}
