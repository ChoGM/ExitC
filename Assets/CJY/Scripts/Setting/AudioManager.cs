using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    private float bgmVolume;
    private float sfxVolume;

    public Slider bgmSlider;  // BGM �����̴�
    public Slider sfxSlider;  // SFX �����̴�

    public GameObject AudioSetting;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // �̹� �����ϴ� �ν��Ͻ��� ������ �ı�
            return;
        }

        // AudioSource�� ���ٸ� ã��
        if (bgmAudioSource == null) bgmAudioSource = GetComponent<AudioSource>();
        if (sfxAudioSource == null) sfxAudioSource = GetComponent<AudioSource>();

        // ���� �����͸� �ε� (PlayerPrefs���� ���� �ҷ�����)
        LoadVolumeSettings();
    }

    private void Start()
    {
        // ���� �ε�� ������ BGM ����
        SceneManager.sceneLoaded += (scene, mode) => PlaySceneBGM(scene.name);

        // ���� ���� �� ������ ����
        ApplyVolumeSettings();

        // �����̴� �� �ʱ�ȭ
        if (bgmSlider != null) bgmSlider.value = bgmVolume;
        if (sfxSlider != null) sfxSlider.value = sfxVolume;

        // �����̴� ���� �ٲ� ������ ����ǵ��� ����
        if (bgmSlider != null)
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void LoadVolumeSettings()
    {
        // PlayerPrefs���� ���� �� �ҷ�����
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f); // �⺻�� 0.5
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f); // �⺻�� 0.5
    }

    private void ApplyVolumeSettings()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = bgmVolume;
        }

        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = sfxVolume;
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = bgmVolume;
        }

        // PlayerPrefs�� ����
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.Save();  // ���� ������ ����
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = sfxVolume;
        }

        // PlayerPrefs�� ����
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();  // ���� ������ ����
    }

    private void PlaySceneBGM(string sceneName)
    {
        AudioClip newBGM = Resources.Load<AudioClip>($"Audio/BGM/{sceneName}");
        if (newBGM != null)
        {
            bgmAudioSource.clip = newBGM;
            bgmAudioSource.Play();
        }
    }

    public void AudioSettingOn()
    {
        AudioSetting.SetActive(true);
    }

    public void AudioSettingOff()
    {
        AudioSetting.SetActive(false);
    }
}