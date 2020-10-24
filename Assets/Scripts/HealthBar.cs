using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform bar;
    private Transform back;
    private Transform border;

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
        float currentH = playerStats.currentHealth;
        float MaxH = playerStats.maxHealth;
        float percentage = currentH / MaxH;
        bar.localScale = new Vector3(percentage, 1);
    }
    // Update is called once per frame
    void Update()
    {
        SetSize();
    }
}
