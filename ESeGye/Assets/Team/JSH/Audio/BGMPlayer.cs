using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    // ✅ 싱글톤 인스턴스
    public static BGMPlayer Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        // 싱글톤 중복 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        // 초기 설정값 적용
        audioSource.volume = PlayerPrefs.GetFloat("BGM_Volume", 1f);
        audioSource.mute = !(PlayerPrefs.GetInt("BGM_Toggle", 1) == 1);
    }

    // 🔉 외부에서 조작할 수 있게 메서드 제공
    public void SetVolume(float volume)
    {
        if (audioSource != null)
            audioSource.volume = volume;
    }

    public void SetMute(bool isMuted)
    {
        if (audioSource != null)
            audioSource.mute = isMuted;
    }
}