using TMPro;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public Transform musicItem;

    public void OnSliderChanged(float value = 15)
    {
        valueText.text = value.ToString();

        for (int i = 0; i < musicItem.childCount; i++)
        {
            musicItem.GetChild(i).GetComponent<AudioSource>().volume = value / 100;
        }
    }
}
