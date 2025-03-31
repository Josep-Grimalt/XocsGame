using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera targetCamera, dollyCamera;
    [SerializeField] private CinemachineSplineCart cart;
    [SerializeField] private CinemachineTargetGroup targetGroup;

    private float actualSplinePos;
    private int actualTarget;

    void Awake()
    {
        actualTarget = 0;
    }

    void Start()
    {
        //en començar mira al 1r jugador
        UpdateLookAtTarget();
    }

    private void FixedUpdate()
    {
        //en acabar el recorregut canvia de camera
        if (cart.SplinePosition >= 5)
        {
            ChangeCamera();
            return;
        }

        //en arribar al següent knot del camí canvia de target per mirar al pròxim jugador
        if (actualSplinePos < cart.SplinePosition)
        {
            actualSplinePos = cart.SplinePosition;
            UpdateLookAtTarget();
        }
    }

    private void UpdateLookAtTarget()
    {
        actualTarget++;

        if (actualTarget >= targetGroup.Targets.Count)
        {
            actualTarget = 0;
        }

        dollyCamera.LookAt = targetGroup.Targets[actualTarget].Object.transform;
    }

    private void ChangeCamera()
    {
        targetCamera.Priority = 5;
    }
}