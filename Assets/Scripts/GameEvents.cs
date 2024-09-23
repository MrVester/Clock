using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    void Awake()
    {
        current = this;
    }
    public event Action<DateTime> onTimeUpdated;
    public void UpdateTime(DateTime dateTime)
    {
        onTimeUpdated?.Invoke(dateTime);
    }
    public event Action<bool> onChangingTime;
    public void ChangeTime(bool isChangingTime)
    {
        onChangingTime?.Invoke(isChangingTime);
    }
}
