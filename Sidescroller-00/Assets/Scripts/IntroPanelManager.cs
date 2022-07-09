using UnityEngine;

public class IntroPanelManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform levelIntroPanel;
    
    private bool isLevelIntroPanelRunning;
    public bool IsLevelIntroPanelRunning { get { return isLevelIntroPanelRunning; } }

    private bool isHideLevelIntroPanel = false;
    private float counterToHideLevelIntroPanel;
    private float timerLimitToHideLevelIntroPanel = 3f;
    private float yLevelIntroPanelTopLimit = 700f;
    private float yLevelIntroPanelBottomLimit = 300f;
    private float levelIntroPanelSpeedIn = 360f;
    private float levelIntroPanelSpeedOut = 760f;


    private void Awake()
    {
        isLevelIntroPanelRunning = true;


        counterToHideLevelIntroPanel = 0;
    }


    public void DisplayLevelIntroPanel()
    {
        if (levelIntroPanel.transform.localPosition.y >= yLevelIntroPanelBottomLimit && !isHideLevelIntroPanel)
        {
            levelIntroPanel.transform.localPosition -= Vector3.up * levelIntroPanelSpeedIn * Time.deltaTime;
        }
        else
        {
            CountToHideLevelIntroPanel();
        }
    }
    private void CountToHideLevelIntroPanel()
    {
        if (counterToHideLevelIntroPanel <= timerLimitToHideLevelIntroPanel)
        {
            counterToHideLevelIntroPanel += Time.deltaTime;

        }
        else if (counterToHideLevelIntroPanel >= timerLimitToHideLevelIntroPanel)
        {
            isHideLevelIntroPanel = true;
            HideLevelIntroPanel();
        }
    }
    private void HideLevelIntroPanel()
    {
        if (levelIntroPanel.transform.localPosition.y <= yLevelIntroPanelTopLimit &&
            isHideLevelIntroPanel)
        {
            levelIntroPanel.transform.localPosition += Vector3.up * levelIntroPanelSpeedOut * Time.deltaTime;
        }
        else
        {
            isLevelIntroPanelRunning = false;
        }
    }

}
