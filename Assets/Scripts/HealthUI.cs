using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{

    public GameObject uiPrefab;
    public Transform target;

    float visibleTime = 5;
    float lastVisibleTime;

    Transform ui;
    Image healthSlider;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        foreach(Canvas c in FindObjectsOfType<Canvas>())
        {
            if(c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        GetComponent<CharacterStats>().OnHealthChanged += onHealthChanged;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if(Time.time - lastVisibleTime > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }
        }
    }

    void onHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastVisibleTime = Time.time;
            float healthPercentage = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercentage;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }
}
