using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CustomToggle : Toggle
{
    [SerializeField] private int number;
    [SerializeField] private bool[] powerups, obstacles;
    
    protected override void Start()
    {
        base.Start();
        onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(bool value)
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (i + 1 == number)
                obstacles[i] = true;
        }
    }
}
