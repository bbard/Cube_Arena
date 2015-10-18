using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int m_Score = 0;
    [SerializeField]
    private Text m_ScoreText;

    private int m_HighScore = 0;
    [SerializeField]
    private Text m_HighScoreText;

    private const string m_ScoreMask = "000000";
    private const string m_ScoreKey = "Score";
    private const string m_HighScoreKey = "HighScore";

    void Start()
    {
        LoadHighScore();
        m_ScoreText.text = "Score: " + m_Score.ToString(m_ScoreMask);
    }

    public void ScoreUp(int point)
    {
        m_Score += point;
        m_ScoreText.text = "Score: " + m_Score.ToString(m_ScoreMask);
    }

    void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(m_HighScoreKey))
            m_HighScore = PlayerPrefs.GetInt(m_HighScoreKey);
        m_HighScoreText.text = "Highscore: " + m_HighScore.ToString(m_ScoreMask);
    }

    void OnDisable()
    {
        SaveScore();
    }

    void SaveScore()
    {
        if (!NewHighScore())
            return;

        PlayerPrefs.SetInt(m_HighScoreKey, m_Score);
        PlayerPrefs.Save();
    }

    public bool NewHighScore()
    {
        return m_Score > m_HighScore;
    }

}
