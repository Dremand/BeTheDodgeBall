using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManage : MonoBehaviour {

    public Animator optionsButton;
    public Animator startAndExitButton;
    public Animator optionsAnim;
    public Animator songsPickAnim;
    public Animator soundAnim;
    public Animator pickBallAnim;
    public GameObject pickBallColour;
    public GameObject pickBallColourBack;
    public GameObject pickBallBack;
    public GameObject optionsOn;
    public GameObject optionsOff;
    public GameObject pickSongsButton;
    public GameObject pickSongsBackButton;
    public GameObject scrollView;
    public GameObject backSongsButton;
    public GameObject backOptionsButton;

    public void GoToOptions()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("goToOptions");
        optionsButton.SetTrigger("OptionsUp");
        startAndExitButton.SetTrigger("Small");
        Invoke("OptionsOn", 1f);
    }
    public void OptionsFromTS()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("gotoOptionsFromTS");
    }
    public void BackToTitle()
    {
        optionsOff.SetActive(false);
        optionsOn.SetActive(true);
        pickSongsButton.SetActive(false);
        optionsAnim.SetTrigger("Small");
        Invoke("OptionsOff", 1f);
    }
    public void OptionsOn()
    {
        optionsOff.SetActive(true);
        optionsOn.SetActive(false);
        pickSongsButton.SetActive(true);
        optionsAnim.SetTrigger("Big");
    }
    public void OptionsOff()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("goBack");
        optionsButton.SetTrigger("OptionsDown");
        startAndExitButton.SetTrigger("Big");
    }
    public void PickSongs()
    {
        Invoke("ScrollOn", 1f);
        pickSongsButton.SetActive(false);
        pickSongsBackButton.SetActive(true);
        pickBallColour.SetActive(false);
        optionsOff.SetActive(false);
        backSongsButton.SetActive(true);
        backOptionsButton.SetActive(false);
        songsPickAnim.SetTrigger("SongsUp");
        soundAnim.SetTrigger("Small");
    }
    public void SongsButton()
    {
        Invoke("SongDown", 1f);
        scrollView.SetActive(false);
        backSongsButton.SetActive(false);
        songsPickAnim.SetTrigger("SongsDown");
    }
    public void SongDown()
    {
        pickSongsButton.SetActive(true);
        pickSongsBackButton.SetActive(false);
        pickBallColour.SetActive(true);
        optionsOff.SetActive(true);
        backOptionsButton.SetActive(true);
        soundAnim.SetTrigger("Big");
    }
    public void ScrollOn()
    {
        scrollView.SetActive(true);
    }
    public void BallPick()
    {
        pickBallAnim.SetTrigger("Up");
        soundAnim.SetTrigger("Small");
        pickSongsButton.SetActive(false);
        optionsOff.SetActive(false);
        backOptionsButton.SetActive(false);
        pickBallColourBack.SetActive(true);
        pickBallBack.SetActive(true);
        pickBallColour.SetActive(false);
    }
    public void BallPickBack()
    {
        Invoke("BallBack", 1f);
        pickBallAnim.SetTrigger("Down");
        pickBallColourBack.SetActive(false);
        pickBallBack.SetActive(false);
        pickBallColour.SetActive(true);
    }
    public void BallBack()
    {
        soundAnim.SetTrigger("Big");
        pickSongsButton.SetActive(true);
        optionsOff.SetActive(true);
        backOptionsButton.SetActive(true);
    }
}
