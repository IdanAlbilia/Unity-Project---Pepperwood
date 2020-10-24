using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;

    List<Quest> quests = new List<Quest>();

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void addQuest(Quest newQuest)
    {
        quests.Add(newQuest);
    }

    public List<Quest> GetQuests()
    {
        return quests;
    }
}
