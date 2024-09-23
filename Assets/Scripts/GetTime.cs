using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class GetTime : MonoBehaviour
{
    [SerializeField] private bool lever = false;
    [SerializeField] Button syncButton;
    public MyClock newClock;
    private string url = "https://yandex.com/time/sync.json";

    private void Awake()
    {
        syncButton.onClick.AddListener(()=>StartCoroutine(UpdateTime()));
    }

    private void Start()
    {
        StartCoroutine(UpdateTime());
        
    }

    void Update()
    {
        if (lever)     //MAKE UPDATE EVERY HOUR
        {
            StartCoroutine(UpdateTime());
            lever = false;
        }
    }

    IEnumerator UpdateTime()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch");
            }
            else
            {
                string jsonvalue = req.downloadHandler.text;
                MyTime data = JsonUtility.FromJson<MyTime>(jsonvalue);
                newClock = new MyClock(data.time);
                GameEvents.current.UpdateTime(newClock.GetDateTime());
            }
        }
    }
    public class MyTime
    {
        public string time = "";
        public string clock = "";
        
    }
    public class MyClock
    {
        DateTime myDateTime;
        public string time;
        public MyClock(string jsonTime)
        {
            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(jsonTime)).LocalDateTime;
            myDateTime = dateTime;
        }
        public MyClock(int seconds)
        {
            DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(seconds).LocalDateTime;
            myDateTime = dateTime;
        }
        public DateTime GetDateTime()
        {
            return myDateTime;
        }
        public string GetTime()
        {
            return time;
        }
    }
}
