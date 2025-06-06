using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer 연결")]
    public AudioMixer audioMixer;

    [Header("Audio Source")]
    public AudioSource bgmSource;

    private const string VolumeParam = "Volume";

    private void Awake()
    {
        // 싱글톤
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ApplySavedSettings(); // 저장된 설정 불러오기
    }

    public void SetVolume(float volume)
    {
        // 0~1 → dB 변환
        float volumeInDb = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat(VolumeParam, volumeInDb);

        if (bgmSource != null)
            bgmSource.volume = volume;
    }

    public void SetMute(bool isMuted)
    {
        audioMixer.SetFloat(VolumeParam, isMuted ? -80f : Mathf.Log10(PlayerPrefs.GetFloat("BGM_Volume", 1f)) * 20f);

        if (bgmSource != null)
            bgmSource.mute = isMuted;
    }

    private void ApplySavedSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGM_Volume", 1f);
        bool isOn = PlayerPrefs.GetInt("BGM_Toggle", 1) == 1;

        SetMute(!isOn);      // false → 음소거
        SetVolume(savedVolume);
    }
}