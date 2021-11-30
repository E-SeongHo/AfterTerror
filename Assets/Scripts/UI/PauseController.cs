using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
	public GameObject PauseWindow;

	public bool isPause;


	private void Awake()
	{
		isPause = false;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPause == true)
			{
				isPause = false;
				PauseWindow.SetActive(false);

				Time.timeScale = 1f;
			}
			else
			{
				isPause = true;
				PauseWindow.SetActive(true);

				Time.timeScale = 0f;
			}
		}
	}

	
	public void OnClickResume()
	{
		isPause = false;
		PauseWindow.SetActive(false);

		Time.timeScale = 1f;
	}
	public void OnClickNewGame()
	{
		SceneManager.LoadScene("Stage1");
	}
	public void OnClickMainMenu()
	{
		SceneManager.LoadScene("Title");
	}
	public void OnClickSetting()
	{
		//Setting Window Open
	}
	public void OnClickExitGame()
	{
		Application.Quit();	//이거 맞음?
	}
}
