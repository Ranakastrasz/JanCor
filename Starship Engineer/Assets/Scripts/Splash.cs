using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Splash : MonoBehaviour {

    // Transitions to another sceen after a short time
    
    public float timer = 5.0f;
    public string sceneName = "Main";

    void Start () {
        StartCoroutine(Transitionz());
    }

    IEnumerator Transitionz()
    {
        yield return new WaitForSecondsRealtime(timer);
        SceneManager.LoadScene(sceneName);

    }
}
