using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public Toggle bgmToggle;
    public Slider bgmVolumeSlider;

    private void OnEnable()
    {
        LoadSettingsFromPrefs(); // 씬 복귀 시에도 호출됨
    }

    private void LoadSettingsFromPrefs()
    {
        bool bgmOn = PlayerPrefs.GetInt("BGM_Toggle", 1) == 1;
        float volume = PlayerPrefs.GetFloat("BGM_Volume", 1f);

        Debug.Log($"[SettingController] 불러온 설정값: BGM_Toggle = {bgmOn}, BGM_Volume = {volume}");

        // UI 반영
        bgmToggle.isOn = bgmOn;
        bgmVolumeSlider.value = volume;

        // AudioManager 반영
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMute(!bgmOn);
            AudioManager.Instance.SetVolume(volume);
        }
        else
        {
            Debug.LogWarning("[SettingController] AudioManager.Instance가 존재하지 않음!");
        }
    }

    private void Start()
    {
        // 슬라이더/토글 리스너 연결
        bgmToggle.onValueChanged.AddListener(OnBGMToggleChanged);
        bgmVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnBGMToggleChanged(bool isOn)
    {
        Debug.Log($"[SettingController] 배경음 토글 변경됨: {isOn}");

        PlayerPrefs.SetInt("BGM_Toggle", isOn ? 1 : 0);
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMute(!isOn);
    }

    private void OnVolumeChanged(float volume)
    {
        Debug.Log($"[SettingController] 볼륨 변경됨: {volume}");

        PlayerPrefs.SetFloat("BGM_Volume", volume);
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetVolume(volume);
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
        Debug.Log("[SettingController] 설정 저장됨 (PlayerPrefs.Save 호출됨)");
    }
}