using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("BasicGameScene");
    }
    
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
