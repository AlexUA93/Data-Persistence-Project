using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public string m_CharacterName;
    public TMP_InputField m_InputField;

    public TMP_Text ScoreBestResolt;
    private void Start()
    {
        LoadData();
        SetBestScore();
        SetName();
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SetCharacterName()
    {
        if (m_InputField != null)
        {
            m_CharacterName = m_InputField.text;
            DataManager.Instance.m_CharacterName = m_CharacterName;
        }
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            DataManager.Instance.m_BestCharacter = data.m_BestCharacter;
            DataManager.Instance.m_CharacterName = data.m_CharacterName;
            DataManager.Instance.m_Points = data.m_BestScore;
        }
    }

    void SetBestScore()
    {
        if (DataManager.Instance != null)
        {
            ScoreBestResolt.text = $"Best Score : {DataManager.Instance.m_BestCharacter} : { DataManager.Instance.m_Points}";
        }
    }

    void SetName()
    {
        m_InputField.text = DataManager.Instance.m_CharacterName;
    }
}
