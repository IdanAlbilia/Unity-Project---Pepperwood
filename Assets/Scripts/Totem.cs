using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : Interactable
{
    public TotemType totemType;
    PlayerManager playerManager;

    public int modifier;
    public int effectTime;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    public override void Interact()
    {
        base.Interact();
        PlayerStats playerStats = playerManager.player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.interctWithTotem(this, totemType);
        }
    }
}

public enum TotemType { Earth, Water, Fire, Air }

