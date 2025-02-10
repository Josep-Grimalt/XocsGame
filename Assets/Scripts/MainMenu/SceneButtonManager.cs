using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    public void StartGameReadyButton()
    {
        SceneManager.LoadScene("BasicGameScene");
    }
    
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
