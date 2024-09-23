
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTime : MonoBehaviour
{
    [SerializeField] private Button changingTimeButton;
    [SerializeField] private Button setByClockButton;
    [SerializeField] private Button setByKeyboardButton;
    [SerializeField] private TMP_Text buttonText;
    private GetTime getTime;
    private Clock clock;
    private RotateArrows rotateArrows;
    private void Awake()
    {
        changingTimeButton.onClick.AddListener(EnableChangingTime);
        setByClockButton.onClick.AddListener(SetTimeByClock);
        setByKeyboardButton.onClick.AddListener(SetTimeByKeyBoard);
        clock = GetComponent<Clock>();
        getTime = GetComponent<GetTime>();
        rotateArrows = GetComponent<RotateArrows>();
    }
    private void Start()
    {
        AfterSetTime();
    }
    void Update()
    {
        
    }
    public void OnButtonPressed()
    {

    }
    private void EnableChangingTime()
    {
        rotateArrows.SetChangeTime(true);
        EnableSetButtons();
        GameEvents.current.ChangeTime(true);
    }
    private void SetTimeByClock()
    {
        double hArrowDegrees = clock.HArrow.transform.rotation.eulerAngles.z;
        hArrowDegrees = (hArrowDegrees + 360) % 360;
        double minutes = 720 - ((hArrowDegrees / 360) * 720);

        DateTime arrowTime = DateTimeOffset.FromUnixTimeSeconds((int)minutes*60).UtcDateTime;
        GameEvents.current.UpdateTime(arrowTime);
        AfterSetTime();

    }
    private void SetTimeByKeyBoard()
    {
        DateTime keyboardTime;
        if (DateTime.TryParse(clock.inputField.text, out keyboardTime))
        {
            GameEvents.current.UpdateTime(keyboardTime);
            AfterSetTime();
        }
           
    }
    private void AfterSetTime()
    {
        rotateArrows.SetChangeTime(false);
        buttonText.text = "Change time";
        setByClockButton.gameObject.SetActive(false);
        setByKeyboardButton.gameObject.SetActive(false);
        changingTimeButton.gameObject.SetActive(true);
        GameEvents.current.ChangeTime(false);
    }
    private void EnableSetButtons()
    {
        setByClockButton.gameObject.SetActive(true);
        setByKeyboardButton.gameObject.SetActive(true);
        changingTimeButton.gameObject.SetActive(false);
    }
}
