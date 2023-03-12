using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private RectTransform skipText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSceneChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitForSceneChange()
    {
        yield return new WaitForSeconds(7f);
        skipText.gameObject.SetActive(false);
        yield return new WaitForSeconds(32f);
        SceneManager.LoadScene("Main_Menu");   
    }
}
