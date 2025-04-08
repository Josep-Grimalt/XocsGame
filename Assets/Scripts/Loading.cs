using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private bool isSplashScreen;
    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField] private float loadingTimer = 10f;

    private float timer;
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= loadingTimer)
        {
            if (isSplashScreen)
                SceneManager.LoadScene(1);
            
            else if (gameManagerSO)
                SceneManager.LoadScene(gameManagerSO.startingScene);
        }
    }
}
