using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
  public void ChangeSQL()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeJSON()
    {
        SceneManager.LoadScene(1);
    }
}
