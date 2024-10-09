using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public AudioSource step;
    public AudioSource run;
    public AudioSource jump;
    public AudioSource land;
    public AudioSource hide;
    public AudioSource breakShelf;
    public AudioSource door;
    public AudioSource enemyStep;
    public AudioSource hurt;

    public Player_Movement PM;
    public Player_Health PH;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void WalkSound()
    {
        step.Play();
    }
    public void WalkSoundStop()
    {
        step.Stop();
    }
    public void JumpSound()
    {
        jump.Play();
    }

    public void RunSound()
    {
        run.Play();
    }
    public void RunSoundStop()
    {
        run.Stop();
    }
    public void LandSound()
    {
        land.Play();
    }

    public void HideSound()
    {
        hide.Play();
    }
    public void HideSoundStop()
    {
        hide.Stop();
    }

    public void EnemyStep()
    {
        enemyStep.Play();
    }
}