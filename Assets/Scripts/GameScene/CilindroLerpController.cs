using System;
using System.Collections;
using UnityEngine;

public class CilindroLerpController : MonoBehaviour
{
    [SerializeField] private float timeBetweenReductions;
    [SerializeField] private float reductionTime;
    [SerializeField] private float reductionAmount;
    [SerializeField] private float minimumScale;
    [SerializeField] private GameManager gameManager;

    private float startReductionTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startReductionTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameReady()) return;
        if (transform.localScale.x < minimumScale || transform.localScale.z < minimumScale) return;

        startReductionTimer += Time.deltaTime;
        if (startReductionTimer >= timeBetweenReductions)
        {
            StartCoroutine(ReduceScale());
            startReductionTimer = 0;
        }
    }

    private IEnumerator ReduceScale()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale - new Vector3(1, 0, 1) * reductionAmount;

        float lerpTime = 0;

        while (lerpTime < reductionTime)
        {
            if (transform.localScale.x < minimumScale || transform.localScale.z < minimumScale) break;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, lerpTime / reductionTime);

            lerpTime += Time.deltaTime;
            yield return null;
        }
    }
}
