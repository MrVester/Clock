using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject SArrow;
    public GameObject MArrow;
    public GameObject HArrow;
    [SerializeField] private TMP_Text timeText;
    private int inGameSeconds=0;
    private float timeAtStart;
    private bool isChangingTime;

    private void Start()
    {
        GameEvents.current.onChangingTime += SetChangingTime;
        GameEvents.current.onTimeUpdated += UpdateClockTime;
    }
    private void UpdateClockTime(DateTime dateTime)
    {
        timeAtStart = dateTime.Second+dateTime.Minute*60+dateTime.Hour*3600;
    }
    private void SetChangingTime(bool isChanging)
    {
        isChangingTime = isChanging;
    }
    void FixedUpdate()
    {
        if (!isChangingTime)
        {
            timeAtStart+= Time.fixedDeltaTime;
            inGameSeconds = Mathf.RoundToInt(timeAtStart);
            UpdateClock();
            UpdateTime();
        }
        
    }
    private void UpdateClock()
    {
        
        
        SArrow.transform.localRotation = Quaternion.Euler(0f, 0f, -inGameSeconds * 360 / 60);
        MArrow.transform.localRotation = Quaternion.Euler(0f, 0f, -inGameSeconds * 360 / 3600);
        HArrow.transform.localRotation = Quaternion.Euler(0f, 0f, -inGameSeconds * 360 / 43200);
    }
    private void UpdateTime()
    {
        DateTime digitalTime = DateTimeOffset.FromUnixTimeSeconds(inGameSeconds).UtcDateTime;
        timeText.text= digitalTime.ToString("HH:mm:ss");
    }
}
