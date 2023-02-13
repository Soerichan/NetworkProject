using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField]
    private RectTransform PercentPanel;
    [SerializeField]
    private RectTransform yellowTeamPannel;
    [SerializeField]
    private RectTransform blueTeamPannel;

    [SerializeField]
    private TMP_Text yellowTeamPercent;
    [SerializeField]
    private TMP_Text blueTeamPercent;

    [SerializeField]
    private TMP_Text yellowTeamResult;
    [SerializeField]
    private TMP_Text blueTeamResult;

    public float entireBlockCount=100;
    public float yelBlockCount=40;
    public float blueBlockCount=30;

    private void Start()
    {
        SetResultPanel();
    }

    public void SetResultPanel()
    {
        
        yellowTeamPercent.text = "" + yelBlockCount + "%";
        blueTeamPercent.text = "" + blueBlockCount + "%";

       

    }

}
