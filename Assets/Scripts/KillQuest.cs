using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kill Quest", menuName = "Quests/KillQuest")]
public class KillQuest : Quest
{
    public int requiredAmount;
    public int currentAmount;

    public string tag;

    public void isReached()
    {
        completed = (currentAmount >= requiredAmount);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        currentAmount = 0;
    }

    public override void Objective(GameObject gameObject)
    {
        base.Objective(gameObject);
        switch (questType)
        {
            case (questType.Kill):
                {
                    if (gameObject.tag == tag)
                    {
                        currentAmount++;
                        isReached();
                    }
                    break;
                }
            case (questType.Gathering):
                {
                    if (gameObject.tag == tag)
                    {
                        currentAmount++;
                        isReached();
                    }
                    break;
                }
        }
    }
}
