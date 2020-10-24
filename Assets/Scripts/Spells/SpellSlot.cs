using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public MoveType originalMove;
    MoveType moveType;
    public Image move;
    Color c;

    // Start is called before the first frame update
    void Start()
    {
        moveType = PlayerStats.instance.GetComponent<PlayerControllerRPG>().moveType;
        c = move.color;
    }

    // Update is called once per frame
    void Update()
    {
        moveType = PlayerStats.instance.GetComponent<PlayerControllerRPG>().moveType;
        if(originalMove != moveType)
        {
            c.a = 0.2f;
            move.color = c;
        }
        else
        {
            c.a = 1f;
            move.color = c;
        }
    }
}
