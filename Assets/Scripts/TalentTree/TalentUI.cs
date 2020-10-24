using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TalentUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject talentUI;
    public Text talentPointsText;

    TalentTree talentTree;

    // Start is called before the first frame update
    void Start()
    {
        talentTree = TalentTree.instance;
        talentUI.SetActive(!talentUI.activeSelf);
        talentPointsText.text = "Talent Points: " + talentTree.currentTalentPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TalentTree"))
        {
            Debug.Log("pressed TalentTree");
            talentUI.SetActive(!talentUI.activeSelf);
            talentPointsText.text = "Talent Points: " + talentTree.currentTalentPoints.ToString();
        }
    }

    public void UpdateUI()
    {
        talentPointsText.text = "Talent Points: " + talentTree.currentTalentPoints.ToString();
    }
}
