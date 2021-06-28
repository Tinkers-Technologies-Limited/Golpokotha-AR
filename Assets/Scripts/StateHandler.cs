using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;

public class StateHandler : MonoBehaviour
{

    [SerializeField] GameObject recordButton;
    [SerializeField] GameObject tutPanel;
    [SerializeField] GameObject rainPrefab;
    [SerializeField] AudioClip c2;
    [SerializeField] AudioClip c3;
    [SerializeField] AudioClip c4;
    [SerializeField] AudioClip c5;
    [SerializeField] AudioClip c6;
    [SerializeField] AudioClip c7;
    [SerializeField] AudioClip c8;
    [SerializeField] AudioClip c9;

    int potCount = 0;
   public void AppearButtonsForFirst()
    {
        recordButton.SetActive(true);
        tutPanel.SetActive(true);
        TextManager.instance.LoadNextText();

    }
    public void AppearButtonsForRest()
    {
        recordButton.SetActive(true);
        TextManager.instance.LoadNextText();

    }
    public void TapWaterPot()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        TextManager.instance.LoadNextText();
    }

    public void PlayC2()
    {
        GetComponent<Animator>().Play("c2");
        AudioSource s = GetComponent<AudioSource>();
        s.clip = c2;
        s.Play();
    }
    public void PlayC3()
    {
        GetComponent<Animator>().Play("c3");
        AudioSource s = GetComponent<AudioSource>();
        s.clip = c3;
        s.Play();
    }
    public void PlayC4()
    {
        GetComponent<Animator>().Play("c4");
        AudioSource s = GetComponent<AudioSource>();
        s.clip = c4;
        s.Play();
    }
    public void PlayC5()
    {
        GetComponent<Animator>().Play("c5");
        AudioSource s = GetComponent<AudioSource>();
        s.clip = c5;
        s.Play();
    }
    public void PlayC6()
    {
        GetComponent<Animator>().Play("c6");
        AudioSource s = GetComponent<AudioSource>();
        s.clip = c6;
        s.Play();
    }
    public void PlayC7()
    {
        GetComponent<Animator>().Play("c7");
        AudioSource s = GetComponent<AudioSource>();
        s.clip = c7;
        s.Play();
    }
    public void PlayC8()
    {
        GetComponent<Animator>().Play("c8");
        AudioSource s = GetComponent<AudioSource>();
        s.clip = c8;
        s.Play();
    }
    public void PlayC9()
    {
        GetComponent<Animator>().Play("c9");
        AudioSource s = GetComponent<AudioSource>();
        s.clip = c9;
        s.Play();
    }

    private void Update()
    {
        if(potCount == 3)
        {
            StartCoroutine(AfterPot());
            potCount = 0;
        }
    }

    public void IncreasePotCount()
    {
        potCount += 1;
    }

    IEnumerator AfterPot()
    {
        TextManager.instance.ResetDisplayTexts();

        transform.GetChild(0).gameObject.SetActive(false);

        rainPrefab.GetComponent<RainScript>().RainIntensity = 1f;
        rainPrefab.GetComponent<RainScript>().EnableWind = true;

        yield return new WaitForSeconds(4f);

        rainPrefab.GetComponent<RainScript>().RainIntensity = 0f;

        rainPrefab.GetComponent<RainScript>().EnableWind = false;
        yield return new WaitForSeconds(3f);

        PlayC9();

    }

    public void EndGame()
    {
        Application.Quit(1);
    }

}
