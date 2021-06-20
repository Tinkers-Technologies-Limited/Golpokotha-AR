using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Text;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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

    bool canSpawn = false;

    [SerializeField] GameObject tapToPlaceTxt;
    [SerializeField] ARPlaneManager aRPlaneManager;

    Vector3 spawnPoint;
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

    }

    public IEnumerator ShowMarketAndReadyToSpawn(Vector3 point)
    {
        spawnPoint = point;
        yield return new WaitForSeconds(1f);

        tapToPlaceTxt.SetActive(true);
        canSpawn = true;
    }
    private void Update()
    {
        SpawnSystem();
    }

    private void SpawnSystem()
    {
        if (((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Space)) && canSpawn)
        {
            if(spawnPoint == null)
                spawnPoint = placementIndicator.transform.position;

            objectToSpawn.SetActive(true);

            objectToSpawn.transform.position = spawnPoint;

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

        }
    }



}
