using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JukeBox : MonoBehaviour {

    AudioManager audiomanager;
    bool JukeBoxOn = true;
    int i = 0;
    String[] TuneNames = {"RidersOfMars","DesertsOfGardha","BattleOfHighnoon"};
    
    public void Start()
    {
        audiomanager = GetComponent<AudioManager>();
        StartCoroutine("JukeBoxPlayer");      
    }

    IEnumerator JukeBoxPlayer()
    {
        while (JukeBoxOn == true)
        {
            PlayTune(TuneNames[i]);
            float tuneLength = audiomanager.AudioClipLength(TuneNames[i]);
            yield return new WaitForSeconds(tuneLength);

            if (i <= TuneNames.Length)
            {
                i = 0;
            }
            else{
                i++;
            }            
        }
    }
    

    public void PlayTune(string name)
    {
        audiomanager.Play(name);
    }



}
