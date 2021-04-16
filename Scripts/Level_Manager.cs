using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public static Level_Manager instance;

    public float waitToRespawn;

    public Transform spawnPoint;

    public string NextLevel;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void respawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        Player_Controller.instance.gameObject.SetActive(false);

        /* Fade from black to white from the UI
        yield return new WaitForSeconds(waitToRespawn - (1f/UIController.instance.fadeSpeed));
        UIController.instance.FadeTo();

        yield return new WaitForSeconds((1f/UIController.instance.fadeSpeed) + 0.2f);
        UIController.instance.FadeFrom();
        */

        yield return new WaitForSeconds(waitToRespawn);

        Player_Controller.instance.gameObject.SetActive(true);

        Player_Controller.instance.transform.position = spawnPoint.position;
    }

    public void EndLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }

}
