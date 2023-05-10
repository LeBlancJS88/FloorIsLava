using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SetVolume : MonoBehaviour
{
        public AudioMixer mixer;

        public void MasterLevel (float sliderValue)
        {
            mixer.SetFloat("BoardGameMasterVol", Mathf.Log10(sliderValue) * 20);
        }

        public void MusicLevel(float sliderValue)
        {
            mixer.SetFloat("BoardGameMusicVol", Mathf.Log10(sliderValue) * 20);
        }

        public void SFXLevel(float sliderValue)
        {
            mixer.SetFloat("BoardGameSFXVol", Mathf.Log10(sliderValue) * 20);
        }

        public void SpeechLevel(float sliderValue)
        {
            mixer.SetFloat("BoardGameSpeechVol", Mathf.Log10(sliderValue) * 20);
        }
    }
