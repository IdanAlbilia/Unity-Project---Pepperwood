using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : MonoBehaviour
{
    public Transform bar;
    private Transform back;
    private Transform border;
    //private Transform barSprite;

    PlayerStats playerStats;


    // Start is called before the first frame update
    private void Start()
    {
        playerStats = PlayerStats.instance;

        back = transform.Find("Background");
        border = transform.Find("Border");
        //bar.localScale = new Vector3(1f, 1f);
    }

    public void SetSize()
    {
        float currentE = playerStats.getExp();
        float MaxE = playerStats.maxExp;
        float percentage = currentE / MaxE;
        bar.localScale = new Vector3(percentage, 1);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
       // Debug.Log("new exp bar");
        SetSize();
    }
}
