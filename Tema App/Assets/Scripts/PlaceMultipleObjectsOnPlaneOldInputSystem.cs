using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlaneOldInputSystem : MonoBehaviour
{
    
    /// Dokunulduðunda oluþturulacak prefab.
    
    [SerializeField]
    [Tooltip("Dokunulan konumda bu prefab'ý oluþturur.")]
    GameObject placedPrefab;

   
    /// Oluþturulan nesne.
    
    GameObject spawnedObject;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Mevcut dokunma var mý diye kontrol edin.
        if (Input.touchCount == 0)
            return;

        // Mevcut dokunma girdisini saklayýn.
        Touch touch = Input.GetTouch(0);

        // Dokunma girdisi ekraný yeni dokundu mu diye kontrol edin.
        if (touch.phase == TouchPhase.Began)
        {
            // Raycast'ýn izleyicilere çarptýðýný kontrol edin.
            if (aRRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast çarpmalarý mesafeye göre sýralandýðýndan, ilk çarpma en yakýn olaný temsil eder.
                var hitPose = hits[0].pose;

                // Prefab'ý oluþturun.
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }

            // Oluþturulan nesnenin her zaman kameraya baktýðýný saðlamak için. Gerekmiyorsa silin.
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }
}
