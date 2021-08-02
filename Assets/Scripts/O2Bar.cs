using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class O2Bar : MonoBehaviour
{
    public GameObject player;

    public float maxO2 = 100;
    public static float currentO2;
    public GameObject oxygenBar;
    public TextMeshProUGUI oxygenLevel;

    private float slowO2Gain = 3f;
    private float slowO2Loss = 4f;
    private float fastO2Gain = 5f;
    private float fastO2Loss = 8f;

    public Gradient O2Gradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        currentO2 = maxO2;
        SetMaxO2(maxO2);

    }

    // Update is called once per frame
    void Update()
    {
        if (MenuUIHandler.isGameActive == true)
        {
            StartCoroutine(LevelLoadingTime());
        }
        
    }

    IEnumerator LevelLoadingTime()
    {
        yield return new WaitForSeconds(2);
        UpdateOxygen();
    }

    public void UpdateOxygen()
    {
        if (player.transform.position.y > 3f)
        {
            currentO2 += fastO2Gain * Time.deltaTime;
        }

        if (player.transform.position.y <=3f && player.transform.position.y > 2f)
        {
            currentO2 += slowO2Gain * Time.deltaTime;
        }

        if (player.transform.position.y <= 2f && player.transform.position.y > -2)
        {
            currentO2 -= slowO2Loss * Time.deltaTime;
        }

        if (player.transform.position.y < -2)
        {
            currentO2 -= fastO2Loss * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentO2 -= 10;
        }

        if (currentO2 >= 100)
        {
            currentO2 = 100;
        }

        if (currentO2 <= 0)
        {
            currentO2 = 0;
            // game over;
        }

        SetO2(currentO2);
    }

    void SetO2(float oxygen)
    {
        fill.color = O2Gradient.Evaluate(oxygen/maxO2);
        oxygenLevel.text = string.Format("Oxygen: {0:#0.0}%", oxygen);
    }

    void SetMaxO2(float oxygen)
    {
        fill.color = O2Gradient.Evaluate(oxygen/maxO2);
        oxygenLevel.text = string.Format("Oxygen: 100%");

    }
}
