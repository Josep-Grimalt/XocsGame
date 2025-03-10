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
        Debug.Log(input.gameObject.name);

        if (targets != null)
        {
            targets.AddMember(input.gameObject.transform, 0.5f, 1);
        }
    }

    public void AddTarget(GameObject input)
    {
        Debug.Log("player " + input.name + " added to targets");

        if (targets != null)
        {
            targets.AddMember(input.transform, 0.5f, 1);
        }
    }

    public void RemoveTarget(GameObject input)
    {
        if (targets != null)
        {
            targets.RemoveMember(input.transform);
        }
    }
}
