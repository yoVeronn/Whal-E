using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("UI references :")]
    [SerializeField] Slider slider;
    [SerializeField] Gradient progressBarGradient;
    [SerializeField] Image fill;
    //[SerializeField] private Text uiStartText;

    [Header("Object references :")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform endLineTransform;

    private Vector3 endLinePosition;

    private float fullDistance;
    
    void Start()
    {
        endLinePosition = endLineTransform.position;
        fullDistance = GetDistance();
    }
    
    private float GetDistance()
    {
        return (endLinePosition - playerTransform.position).sqrMagnitude;
    }

    private void UpdateProgressFill(float value)
    {
        slider.value = value;

        fill.color = progressBarGradient.Evaluate(slider.normalizedValue);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x < endLinePosition.x)
        {
            float newDistance = GetDistance();
            float progressValue = Mathf.InverseLerp(fullDistance, 0f, newDistance);

            UpdateProgressFill(progressValue);
        }   
    }
}
