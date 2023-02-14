using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public float m_fBlueScore;
    public float m_fYellowScore;

    public TextMeshProUGUI m_textBlueScore;
    public TextMeshProUGUI m_textYellowScore;

    private void Update()
    {
        m_textBlueScore.text    = m_fBlueScore.ToString();
        m_textYellowScore.text  = m_fYellowScore.ToString();
    }
}
