using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameUI : MonoBehaviour
{
    public Text gameoverText;
    public Text retryText;

    public GameObject bloodScreen;

    private GameObject mapManager;
    private GameObject buttonManager;

    private void Start()
    {
        //mapManager = GameObject.Find("MapManager");
        buttonManager = GameObject.Find("EnemyButtonManager");
    }

    private void Update()
    {
        if(ShieldmanController.Instance.GetDieState())
        {
            buttonManager.SetActive(false);
            // mapManager.GetComponent<MapManager>().StopMapMove();

            gameoverText.gameObject.SetActive(true);
            retryText.gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneLoader.Instance.LoadScene("Stage1");
            }
        }
    }
    public void BloodScreenON()
    {
        bloodScreen.SetActive(true);
    }
    public void BloodScreenOFF()
    {
        bloodScreen.SetActive(false);
    }
}
