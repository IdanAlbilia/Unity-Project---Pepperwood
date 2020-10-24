using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public questType questType;

    new public string name = "New quest";
    public string questDescription;

    public Item[] loot;
    public int exp;

    [SerializeField]
    private bool finished = false;

    public bool completed;

    protected virtual void OnEnable()
    {
        completed = finished;
    }

    public virtual void Objective(GameObject gameObject)
    {
        // meant to be overriden
    }
}

public enum questType { Kill, Gathering };