using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlaceObject : MonoBehaviour
{
    public GameObject arObject, sillon, cajonero, mesa, sillonModerno, sillonDoble;

    public ARRaycastManager arRaycastManager;
    public ARPlaneManager aRPlaneManager;
    private GameObject bearStatue;
    private float distanciaInicial = 0;
    private float distanciaNueva = 0;
    private float ultimoX = 0;

    public float speed = 0.25f;
    public float speedRot = 25f;

    public bool rotacion = false;
    public bool escala = false;
    public bool posicion = false;
    public TextMeshProUGUI Text;
    public string nameMueble;

  // Start is called before the first frame update
  void Start()
  {
    EnableTransformation(2);
        aRPlaneManager = FindAnyObjectByType<ARPlaneManager>();
  }


  private void OnEnable()
  {
    EnhancedTouch.EnhancedTouchSupport.Enable();
    EnhancedTouch.TouchSimulation.Enable();

    EnhancedTouch.Touch.onFingerDown += PlaceObjectInPlane;
    EnhancedTouch.Touch.onFingerMove += PlaceObjectInPlane;
  }

  private void OnDisable()
  {
    EnhancedTouch.EnhancedTouchSupport.Disable();
    EnhancedTouch.TouchSimulation.Disable();

    EnhancedTouch.Touch.onFingerDown -= PlaceObjectInPlane;
    EnhancedTouch.Touch.onFingerMove -= PlaceObjectInPlane;
  }

  private void PlaceObjectInPlane(EnhancedTouch.Finger finger)
  {
    //Debug.Log("Fire");
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    arRaycastManager.Raycast(finger.screenPosition, hits, TrackableType.PlaneWithinPolygon);

    if (bearStatue == null && hits.Count > 0)
    {
      gameObject.AddComponent<ARAnchor>();
            switch (nameMueble)
            {
                case "Cajonero": bearStatue = Instantiate(cajonero, hits[0].pose.position, Quaternion.identity, gameObject.transform);

                    foreach (var plane in aRPlaneManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    aRPlaneManager.enabled = false;
                    EnableTransformation(2);
                    break;

                case "Mesa": bearStatue = Instantiate(mesa, hits[0].pose.position, Quaternion.identity, gameObject.transform);
                    foreach (var plane in aRPlaneManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    aRPlaneManager.enabled = false;
                    EnableTransformation(2);

                    break;
                case "Sillon":bearStatue = Instantiate(sillon, hits[0].pose.position, Quaternion.identity, gameObject.transform);
                    foreach (var plane in aRPlaneManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    aRPlaneManager.enabled = false;
                    EnableTransformation(2);

                    break;
                case "SillonModerno":bearStatue = Instantiate(sillonModerno, hits[0].pose.position, Quaternion.identity, gameObject.transform);
                    foreach (var plane in aRPlaneManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    aRPlaneManager.enabled = false;
                    EnableTransformation(2);

                    break;
                case "SillonDoble": bearStatue = Instantiate(sillonDoble, hits[0].pose.position, Quaternion.identity, gameObject.transform);
                    foreach (var plane in aRPlaneManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    aRPlaneManager.enabled = false;
                    EnableTransformation(2);

                    break;

                default: Text.text = "No se ha seleccionado ningun mueble";
                    break;
            }

        }

        if (hits.Count > 0 && posicion)
            if (!bearStatue.activeSelf)
            {
                bearStatue.SetActive(true);
                foreach (var plane in aRPlaneManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                aRPlaneManager.enabled = false;
            }
      bearStatue.transform.position = hits[0].pose.position;
  }


  private void Update()
  {
       

        if (EnhancedTouch.Touch.activeFingers.Count == 2 && bearStatue && escala)
       {
      Finger d1 = EnhancedTouch.Touch.activeFingers[0];
      Finger d2 = EnhancedTouch.Touch.activeFingers[1];

      //Debug.Log("Detección Dos Dedos");
      if (d1.currentTouch.phase == TouchPhase.Began || d2.currentTouch.phase == TouchPhase.Began)
      {
        distanciaInicial = Vector2.Distance(d1.screenPosition, d2.screenPosition);
        Debug.Log(distanciaInicial);
      }

      if (d1.currentTouch.phase == TouchPhase.Moved || d2.currentTouch.phase == TouchPhase.Moved)
      {
        distanciaNueva = Vector2.Distance(d1.screenPosition, d2.screenPosition);
        Debug.Log(distanciaNueva);
      }

      if (distanciaInicial < distanciaNueva && bearStatue.transform.localScale.x < 1f)
      {
        bearStatue.transform.localScale += Vector3.one * Time.deltaTime * speed;
      }
      else if (distanciaInicial > distanciaNueva && bearStatue.transform.localScale.x > 0.2f)
      {
        bearStatue.transform.localScale -= Vector3.one * Time.deltaTime * speed;
      }

      distanciaInicial = distanciaNueva;
    }


    if (EnhancedTouch.Touch.activeFingers.Count == 1 && bearStatue && rotacion)
    {
      EnhancedTouch.Touch t = EnhancedTouch.Touch.fingers[0].currentTouch;
      if (t.phase == TouchPhase.Began)
      {
        ultimoX = t.screenPosition.x;
      }

      if (t.phase == TouchPhase.Moved)
      {
        if (ultimoX > t.screenPosition.x)
        {
          bearStatue.transform.Rotate(Vector3.up * speedRot * Time.deltaTime);
          Debug.Log("Rotar Izquierda");
        }
        else
        {
          bearStatue.transform.Rotate(Vector3.down * speedRot * Time.deltaTime);
          Debug.Log("Rotar Derecha");
        }
        ultimoX = t.screenPosition.x;
      }
    }
  }



  public void EnableTransformation(int n)
  {
    posicion = false;
    rotacion = false;
    escala = false;
    switch (n)
    {
      case 1:
        posicion = true;
        break;
      case 2:
        rotacion = true;
                break;
      case 3:
        escala = true;
                break;

      default:
        posicion = true;
        break;
    }
  }

    public void Object(string mueble)
    {
        nameMueble = mueble;
        Debug.Log(mueble);
    }

    public void Clear()
    {
        nameMueble = null;
        Destroy(bearStatue);
       
    }

    public void NameMueble(string mueble)
    {
        Text.text = mueble;
    }

    public void OpenPlane()
    {
        foreach (var plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
        aRPlaneManager.enabled = true;
    }

    public void DeactivateObject()
    {
        if (bearStatue.activeSelf) { bearStatue.SetActive(false);
            OpenPlane();

        }
        else
        {
            Debug.Log("No hay nada activo");
        }

    }
}
