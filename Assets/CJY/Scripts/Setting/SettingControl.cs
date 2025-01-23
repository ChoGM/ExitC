using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    public AudioSource bgmAudioSource; // BGM ����� �ҽ� �߰�
    public AudioSource sfxAudioSource; // SFX ����� �ҽ� �߰�

    private Data gameData;

    private void Start()
    {
        Datamanager.Instance.LoadGameData();

        // �����̴� �ʱⰪ ����
        bgmSlider.value = gameData.BGMVolume;
        sfxSlider.value = gameData.SFXVolume;

        // �����̴� �� ���� �̺�Ʈ ���
        bgmSlider.onValueChanged.AddListener((value) => {
            gameData.BGMVolume = value;
            bgmAudioSource.volume = value; // BGM ����� �ҽ� ���� ����
        });
        sfxSlider.onValueChanged.AddListener((value) => {
            gameData.SFXVolume = value;
            sfxAudioSource.volume = value; // SFX ����� �ҽ� ���� ����
        });

        // ����� �ҽ� �ʱ� ���� ����
        bgmAudioSource.volume = gameData.BGMVolume;
        sfxAudioSource.volume = gameData.SFXVolume;
    }

    public void SaveSettings()
    {
        Datamanager.Instance.SaveGameData();
    }
}
