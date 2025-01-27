using UnityEngine;

public class PlayerManagerController : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(this);
    }
}
