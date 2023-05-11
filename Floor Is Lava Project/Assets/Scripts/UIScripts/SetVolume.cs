using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SetVolume : MonoBehaviour
{
        public AudioMixer mixer;

        public void MasterLevel (float sliderValue)
        {
            mixer.SetFloat("LavaCourseMasterVol", Mathf.Log10(sliderValue) * 20);
        }

        public void MusicLevel(float sliderValue)
        {
            mixer.SetFloat("LavaCourseMusicVol", Mathf.Log10(sliderValue) * 20);
        }

        public void SFXLevel(float sliderValue)
        {
            mixer.SetFloat("LavaCourseSFXVol", Mathf.Log10(sliderValue) * 20);
        }

        public void SpeechLevel(float sliderValue)
        {
            mixer.SetFloat("LavaCourseAmbientVol", Mathf.Log10(sliderValue) * 20);
        }
    }
