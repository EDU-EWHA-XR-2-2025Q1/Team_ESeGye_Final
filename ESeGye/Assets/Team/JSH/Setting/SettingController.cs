using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public Toggle bgmToggle;
    public Slider bgmVolumeSlider;

    void Start()
    {
        // 저장된 값 불러오기
        bgmToggle.isOn = PlayerPrefs.GetInt("BGM_Toggle", 1) == 1;
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGM_Volume", 1f);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("BGM_Toggle", bgmToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("BGM_Volume", bgmVolumeSlider.value);
        PlayerPrefs.Save();
        Debug.Log("설정이 저장되었습니다!");
    }
}