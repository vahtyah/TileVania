using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    float musicVolume = 0.5f;
    AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GameObject.FindWithTag("BackgroundMusic").GetComponent<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat("volume");
        musicSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    public void VolumeUpdater()
    {
        musicVolume = volumeSlider.value; 
    }
}
