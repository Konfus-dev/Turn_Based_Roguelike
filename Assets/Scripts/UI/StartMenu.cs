using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject StartMenuUI;
    public Player Player;
    public UIPopup ControlsPopUp;

    private void Start()
    {

    }

    public void OnGameStart()
    {
        StartCoroutine(StartGame(.2f));
    }

    private IEnumerator StartGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        StartMenuUI.SetActive(false);
    }

    public void OnShowControls()
    {
        StartCoroutine(ShowControls(.2f));
    }

    private IEnumerator ShowControls(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ControlsPopUp.enabled = true;
        ControlsPopUp.Show();
    }

    public void OnHideControls()
    {
        StartCoroutine(HideControls(.2f));
    }

    private IEnumerator HideControls(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ControlsPopUp.Hide();
    }

    public void OnGameQuit()
    {
        StartCoroutine(QuitGame(.4f));
    }

    private IEnumerator QuitGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Application.Quit();
    }
}
