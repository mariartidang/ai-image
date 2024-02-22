using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] RawImage[] rawImages;
    [SerializeField] Slider slider;
    [SerializeField] GameObject loadingScreen;
    static public UIManager Instance { get; private set; }
    
    

    [SerializeField] TMP_Text questionText;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        slider.value = 0;
    }

    public void SetQuestionText(string text)
    {
        questionText.SetText(text);
    }

    public void SetPicture(int index, Texture picture)
    {
        rawImages[index].texture = picture;
    }

    public void SetSliderValue(float progress)
    {
        slider.value += progress;
    }

    public void EndLoading()
    {
        loadingScreen.SetActive(false);
    }

    public void SetSliderMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
    }
}
