using UnityEngine.EventSystems;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject characterUI;

    CharacterPage characterPage;

    // Start is called before the first frame update
    void Start()
    {
        characterPage = CharacterPage.instance;
        characterUI.SetActive(!characterUI.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CharacterPage"))
        {
            Debug.Log("pressed CharacterPage");
            characterUI.SetActive(!characterUI.activeSelf);
        }
    }
}
