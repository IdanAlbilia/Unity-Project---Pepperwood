using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteraction : Interactable
{
    public Quest quest;
    bool finished = false;

    public string DialogueTextFirst;
    public string DialogueTextSecond;

    public DialogueWindow Dialogue;

    public override void Interact()
    {
        base.Interact();
        if(PlayerManager.instance.GetQuests().Contains(quest) && quest.completed && !finished)
        {
            Dialogue.Show(DialogueTextSecond);
            foreach (Item loot in quest.loot)
            {
                Debug.Log("creating loot " + loot.name);
                Inventory.instance.Add(loot);
                PlayerManager.instance.player.GetComponent<PlayerStats>().addExp(quest.exp, "Quest");
                finished = true;
                GetComponentInChildren<Animator>().SetTrigger("bowTrigger");
            }
        }
        else if (!PlayerManager.instance.GetQuests().Contains(quest))
        {
            Dialogue.Show(DialogueTextFirst);
            Debug.Log("Adding Quest: " + quest.name);
            PlayerManager.instance.addQuest(quest);
        }
    }


    public override void OnDefocused()
    {
        base.OnDefocused();
        Dialogue.Close();
    }
}
