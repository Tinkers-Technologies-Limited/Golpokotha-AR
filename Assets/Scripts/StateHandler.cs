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
    AudioSource audioSource;

    int potCount = 0;

    private void Start()
    {
        audioSource =  GetComponent<AudioSource>();
    }
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
        transform.GetChild(9).gameObject.SetActive(true);
        TextManager.instance.LoadNextText();
    }

    public void PlayC2()
    {
        //GetComponent<Animator>().Play("c2");
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c2;
        audioSource.Play();
    }
    public void PlayC3()
    {
        //GetComponent<Animator>().Play("c3");
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(2).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c3;
        audioSource.Play();
    }
    public void PlayC4()
    {
        //GetComponent<Animator>().Play("c4");
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(3).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c4;
        audioSource.Play();
    }
    public void PlayC5()
    {
        //GetComponent<Animator>().Play("c5");
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(4).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c5;
        audioSource.Play();
    }
    public void PlayC6()
    {
        //GetComponent<Animator>().Play("c6");
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(true);
        transform.GetChild(5).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c6;
        audioSource.Play();
    }
    public void PlayC7()
    {
        //GetComponent<Animator>().Play("c7");
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(6).gameObject.SetActive(true);
        transform.GetChild(6).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c7;
        audioSource.Play();
    }
    public void PlayC8()
    {
        //GetComponent<Animator>().Play("c8");
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(7).gameObject.SetActive(true);
        transform.GetChild(7).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c8;
        audioSource.Play();
    }
    public void PlayC9()
    {
        //GetComponent<Animator>().Play("c9");
        transform.GetChild(7).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(true);
        transform.GetChild(8).GetComponent<Animator>().enabled = true;
        //AudioSource s = GetComponent<AudioSource>();
        audioSource.clip = c9;
        audioSource.Play();
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

        transform.GetChild(9).gameObject.SetActive(false);

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
