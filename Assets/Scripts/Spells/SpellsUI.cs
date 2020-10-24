using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsUI : MonoBehaviour
{
    public GameObject spellsUI;

    // Start is called before the first frame update
    void Start()
    {
        spellsUI.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Spells"))
        {
            Debug.Log("pressed Spells");
            spellsUI.SetActive(!spellsUI.activeSelf);
        }
    }
}
