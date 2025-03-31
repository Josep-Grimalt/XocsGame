using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera targetCamera;
    [SerializeField] private CinemachineSplineCart cart;

    private void FixedUpdate()
    {
        //en acabar el recorregut canvia de camera
        if (cart.SplinePosition >= 4)
        {
            ChangeCamera();
        }
    }

    private void ChangeCamera()
    {
        // si ja ha canviat de camera no fa res
        if (targetCamera.Priority > 0) return;

        targetCamera.Priority = 5;
    }
}