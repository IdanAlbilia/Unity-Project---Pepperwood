using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject questsUI;

    public GameObject questSlotPrefab;

    QuestPage questPage;
    QuestSlot[] questSlots = new QuestSlot[7];

    // Start is called before the first frame update
    void Start()
    {
        questPage = QuestPage.instance;
        questsUI.SetActive(!questsUI.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Quests"))
        {
            foreach (Transform child in itemsParent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (Quest quest in PlayerManager.instance.GetQuests())
            {
                Instantiate<QuestSlot>(questSlotPrefab.GetComponent<QuestSlot>().AddQuest(quest), itemsParent);
            }
            questsUI.SetActive(!questsUI.activeSelf);
        }
    }

}
