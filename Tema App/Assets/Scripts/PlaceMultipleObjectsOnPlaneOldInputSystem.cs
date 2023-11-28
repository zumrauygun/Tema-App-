using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlaneOldInputSystem : MonoBehaviour
{
    
    /// Dokunuldu�unda olu�turulacak prefab.
    
    [SerializeField]
    [Tooltip("Dokunulan konumda bu prefab'� olu�turur.")]
    GameObject placedPrefab;

   
    /// Olu�turulan nesne.
    
    GameObject spawnedObject;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Mevcut dokunma var m� diye kontrol edin.
        if (Input.touchCount == 0)
            return;

        // Mevcut dokunma girdisini saklay�n.
        Touch touch = Input.GetTouch(0);

        // Dokunma girdisi ekran� yeni dokundu mu diye kontrol edin.
        if (touch.phase == TouchPhase.Began)
        {
            // Raycast'�n izleyicilere �arpt���n� kontrol edin.
            if (aRRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast �arpmalar� mesafeye g�re s�raland���ndan, ilk �arpma en yak�n olan� temsil eder.
                var hitPose = hits[0].pose;

                // Prefab'� olu�turun.
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }

            // Olu�turulan nesnenin her zaman kameraya bakt���n� sa�lamak i�in. Gerekmiyorsa silin.
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }
}
