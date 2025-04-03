using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField] private float loadingTimer = 10f;

    private float timer;
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= loadingTimer)
        {
            if (gameManagerSO)
            {
                SceneManager.LoadScene(gameManagerSO.startingScene);
            }
        }
    }
}
