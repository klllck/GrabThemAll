using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValidation : Singleton<SliderValidation>
{
    [SerializeField] private TextMeshProUGUI sliderText;
    private Slider slider;

    public TextMeshProUGUI SliderText { get => sliderText; set => sliderText = value; }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        UpdateText((int)slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    private void UpdateText(float value)
    {
        SliderText.text = slider.value.ToString();
    }
}
