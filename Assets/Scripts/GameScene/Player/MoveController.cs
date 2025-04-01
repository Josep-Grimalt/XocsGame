using System.Collections;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    
    public void StopPlayer()
    {
        GetComponentInParent<PlayerMovementController>().enabled = false;
        GetComponentInParent<BoxCollider>().excludeLayers = playerMask;
        GetComponentInParent<BoxCollider>().includeLayers = 0;
    }

    public void MovePlayer()
    {
        GetComponentInParent<PlayerMovementController>().enabled = true;
        StartCoroutine(IncludeLayer());
    }

    private IEnumerator IncludeLayer()
    {
        yield return new WaitForSeconds(1f);
        
        GetComponentInParent<BoxCollider>().includeLayers = playerMask;
        GetComponentInParent<BoxCollider>().excludeLayers = 0;
    }
}
