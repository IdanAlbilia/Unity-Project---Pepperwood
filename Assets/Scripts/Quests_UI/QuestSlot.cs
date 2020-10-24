using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public Transform targetName;
    public Transform targetMark;
    public Quest quest;
    Text questName;

    public GameObject questDescription; 

    // Start is called before the first frame update
    void Start()
    {
        questDescription.SetActive(false);
        questName = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (quest != null)
        {
            questDescription.GetComponent<Text>().text = quest.questDescription;
            questName.text = quest.name;
            if (quest.completed)
                targetMark.GetComponent<Image>().enabled = true;
        }
    }

    public void OnButtonClicked()
    {
        questDescription.SetActive(!questDescription.activeSelf);
    }

    public QuestSlot AddQuest(Quest newQuest)
    {
        quest = newQuest;
        return this;
    }

}
