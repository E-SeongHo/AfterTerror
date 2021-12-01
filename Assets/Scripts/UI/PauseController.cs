using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				GenerateWindow();
			}
		}
	}

	public void GenerateWindow()
    {
		isPause = true;
		PauseWindow.SetActive(true);

		Time.timeScale = 0f;
    }
	public void OnClickResume()
	{
		isPause = false;
		PauseWindow.SetActive(false);

		Time.timeScale = 1f;
	}
	public void OnClickNewGame()
	{
		SceneLoader.Instance.LoadScene("Stage1");
		Time.timeScale = 1f;
	}
	public void OnClickMainMenu()
	{
		SceneLoader.Instance.LoadScene("Title");
		Time.timeScale = 1f;
	}
	public void OnClickSetting()
	{
		//Setting Window Open
	}
	public void OnClickExitGame()
	{
		Application.Quit();
	}
}
