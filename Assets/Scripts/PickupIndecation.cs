using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupIndecation : MonoBehaviour
{
    #region Singelton

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than once instances of PlayerStats were found!");
        }
        instance = this;
    }

    #endregion

    public static PickupIndecation instance;
    Text itemName;
    public float visibleTime = 3f;
    float lastPoppedTime;

    // Start is called before the first frame update
    void Start()
    {
        instance.gameObject.SetActive(false);
        itemName = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastPoppedTime >= visibleTime && instance.enabled)
        {
            instance.gameObject.SetActive(false);
        }
    }

    public void SetUpText(string newItemName)
    {
        lastPoppedTime = Time.time;
        instance.gameObject.SetActive(true);
        itemName.text = "Picked up " + newItemName;
    }
}
