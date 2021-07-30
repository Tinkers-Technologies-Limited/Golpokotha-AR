using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Text;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using UnityEngine.Networking;
using GleyInternetAvailability;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] GameObject objectToSpawn;

    PlacementIndicator placementIndicator;

    [SerializeField] Vector3 afterPlacementScale;

    bool canSpawn = true;
    bool hasInternet;

    [SerializeField] GameObject tapToPlaceTxt;
    [SerializeField] ARPlaneManager aRPlaneManager;
    [SerializeField] AudioClip startAudio;
    [SerializeField] GameObject mainCharacter;
    [SerializeField] GameObject spawnVFX;
    [SerializeField] GameObject noInternetPanel;

    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

        StartCoroutine(LookForInternetConnection());

    }


    IEnumerator LookForInternetConnection()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            GleyInternetAvailability.Network.IsAvailable(CompleteMethod);
        }


    }

    private void CompleteMethod(ConnectionResult connectionResult)
    {
        if (connectionResult == ConnectionResult.Working)
        {
            noInternetPanel.SetActive(false);
            hasInternet = true;
            Debug.LogError(hasInternet);
        }
        else
        {
            noInternetPanel.SetActive(true);
            hasInternet = false;
        }
            
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public IEnumerator ShowMarkerAndReadyToSpawn()
    {
        yield return new WaitForSeconds(3f);

        tapToPlaceTxt.SetActive(true);
        canSpawn = true;
    }
    private void Update()
    {
        SpawnSystem();
    }

    private void SpawnSystem()
    {
        if (((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Space)) && canSpawn && hasInternet)
        {

            Vector3 spawnPoint = placementIndicator.transform.position;

            objectToSpawn.SetActive(true);

            objectToSpawn.transform.position = spawnPoint;
            GameObject particle =  Instantiate(spawnVFX, spawnPoint, Quaternion.identity);
            Destroy(particle, 5f);

            //scale up
            objectToSpawn.transform.DOScale(afterPlacementScale, 1f);

            //turn of placement indicator and spawing..
            aRPlaneManager.enabled = false;

            foreach (ARPlane plane in aRPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

            placementIndicator.gameObject.SetActive(false);

            tapToPlaceTxt.SetActive(false);

            canSpawn = false;

            StartCoroutine(startScene());

        }
    }

    IEnumerator startScene()
    {
        yield return new WaitForSeconds(2f);

        //mainCharacter.GetComponent<Animator>().Play("start");
        mainCharacter.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        AudioSource s = mainCharacter.GetComponent<AudioSource>();
        s.clip = startAudio;
        s.Play();
    }



}
