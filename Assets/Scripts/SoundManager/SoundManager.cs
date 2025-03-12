using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I {get; private set;}
    [SerializeField] private SoundLibrary soundLibrary;
    void Awake()
    {
        if(I)
        {
            Destroy(gameObject);
        }
        else
        {
            I = this;
            DontDestroyOnLoad(I);
        }
    }

    void Start()
    {
        
    }

    public void PlaySound()
    {
        
    }
}
