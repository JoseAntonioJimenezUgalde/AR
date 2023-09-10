using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARGameManager : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }


  public void OpenInfo(string url)
  {
    Application.OpenURL(url);
  }
}
