using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;

public class UserTransition : MonoBehaviour
{
    public Canvas TransitionCanvas;
    public GameObject Crosshair, LoginMenu, RegisterMenu, LoginMainArea, RegisterMainArea;
    public static bool TransitionMade = false;

    PointerControls rayCaster;
    Animator transition;
	bool menuOption, successExit;

    void Start()
    {
        rayCaster = Crosshair.GetComponent<PointerControls>();
        transition = TransitionCanvas.GetComponent<Animator>();
        successExit = false;
    }

    void Update()
    {
        string currentAnimatorState = transition.GetCurrentAnimatorClipInfo(0)[0].clip.ToString().Split(' ')[0];
        if (rayCaster.GetCurrentObject().Equals("Login") || rayCaster.GetCurrentObject().Equals("Register"))
        {
			menuOption = rayCaster.GetCurrentObject().Equals("Login"); // true = login, false = register
            if (!TransitionMade)
            {
                MakeTransition();
            }
        }
        
        switch(currentAnimatorState)
        {
            case "Crossfade_In":
                StopControls();
                break;
            case "Crossfade_Sleep_Blank":
                if (!TransitionMade) {
                    CloseMenu();
                    if (successExit) CleanFields();
                    HideAlerts();
                };
                break;
            case "Crossfade_Sleep_Blank_Out":
                TransitionMade = false;
                CleanFields();
                HideAlerts();
                CloseMenu();
                break;
            case "Crossfade_Out":
                successExit = true;
				PointerControls.canInteractAgain = false;
                break;
            case "Crossfade_Sleep":
                if (successExit) // needed to not enter on enter->exit loop
                {
                    ResetRequests();
                    ResetTriggers(); 
                    ResumeControls();
                    successExit = false;
                }
                break;
        }
    }

    void MakeTransition()
    {   
        transition.SetTrigger("StartTransition");
        TransitionMade = true;
    }

    void StopControls()
    {
        Crosshair.SetActive(false);
        KeyboardMovementControls.IsInMenu = true;

        if (menuOption) LoginMenu.SetActive(true);
		else RegisterMenu.SetActive(true);
    }

    void ResumeControls()
    {
        Crosshair.SetActive(true);
        KeyboardMovementControls.IsInMenu = false;
    }

    public void ExitTransition()
    {
        if (successExit) transition.SetTrigger("FinishLongerTransition");
        else
        {
            TransitionMade = false;
            transition.SetTrigger("FinishTransition");
        }

        rayCaster.TransitionFinished();
    }

    void ResetTriggers()
    {
        transition.ResetTrigger("StartTransition");
        transition.ResetTrigger("FinishTransition");
        transition.ResetTrigger("FinishLongerTransition");
    }

    void ResetRequests()
    {
        RegisterKeyboardManager.HasSentRegisterRequest = false;
        LoginKeyboardManager.HasSentLoginRequest = false;
    }
    
    void CloseMenu()
    {
        if (menuOption) LoginMenu.SetActive(false);
		else RegisterMenu.SetActive(false);
    }

    void HideAlerts()
    {
        LoginMenu.transform.Find("Canvas/Login Successful").gameObject.SetActive(false);
        LoginMenu.transform.Find("Canvas/Login Error").gameObject.SetActive(false);
        RegisterMenu.transform.Find("Canvas/Register Successful").gameObject.SetActive(false);
        RegisterMenu.transform.Find("Canvas/Register Error").gameObject.SetActive(false);
    }

    void CleanFields()
    {
        LoginMainArea.GetComponent<LoginKeyboardManager>().ResetTextFields();
        RegisterMainArea.GetComponent<RegisterKeyboardManager>().ResetTextFields();
    }

    public void TriggerSuccessExit()
    {
        successExit = true;
    }
}
