using UnityEngine;
using UnityEngine.InputSystem.UI;

public class GetCanvasComponents : MonoBehaviour
{
    [SerializeField] private GameObject buttonReady;
    [SerializeField] private MultiplayerEventSystem multiplayerEventSystem;

    public GameObject GetButtonReady()
    {
        return buttonReady;
    }

    public MultiplayerEventSystem GetMultiplayerEventSystem()
    {
        return multiplayerEventSystem;
    }
}
