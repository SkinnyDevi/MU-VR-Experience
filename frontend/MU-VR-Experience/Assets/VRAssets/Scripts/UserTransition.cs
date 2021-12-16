using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;

public class UserTransition : MonoBehaviour
{
    public Canvas TransitionCanvas;
    public GameObject Crosshair, LoginMenu, RegisterMenu, LoginMainArea, RegisterMainArea;
    public static bool TransitionMade = false;

    PointerControls _rayCaster;
    Animator _transition;
	bool _menuOption, _successExit;

    void Start()
    {
        _rayCaster = Crosshair.GetComponent<PointerControls>();
        _transition = TransitionCanvas.GetComponent<Animator>();
        _successExit = false;
    }

    void Update()
    {
        string currentAnimatorState = _transition.GetCurrentAnimatorClipInfo(0)[0].clip.ToString().Split(' ')[0];
        if (_rayCaster.GetCurrentObject().Equals("Login") || _rayCaster.GetCurrentObject().Equals("Register"))
        {
			_menuOption = _rayCaster.GetCurrentObject().Equals("Login"); // true = login, false = register
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
                    if (_successExit) CleanFields();
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
                _successExit = true;
				PointerControls.CanInteractAgain = false;
                break;
            case "Crossfade_Sleep":
                if (_successExit) // needed to not enter on enter->exit loop
                {
                    ResetRequests();
                    ResetTriggers(); 
                    ResumeControls();
                    _successExit = false;
                }
                break;
        }
    }

	public void ExitTransition()
    {
        if (_successExit) _transition.SetTrigger("FinishLongerTransition");
        else
        {
            TransitionMade = false;
            _transition.SetTrigger("FinishTransition");
        }

        _rayCaster.TransitionFinished();
    }

	    public void TriggerSuccessExit()
    {
        _successExit = true;
    }

    private void MakeTransition()
    {   
        _transition.SetTrigger("StartTransition");
        TransitionMade = true;
    }

    private void StopControls()
    {
        Crosshair.SetActive(false);
        KeyboardMovementControls.IsInMenu = true;

        if (_menuOption) LoginMenu.SetActive(true);
		else RegisterMenu.SetActive(true);
    }

    private void ResumeControls()
    {
        Crosshair.SetActive(true);
        KeyboardMovementControls.IsInMenu = false;
    }

    private void ResetTriggers()
    {
        _transition.ResetTrigger("StartTransition");
        _transition.ResetTrigger("FinishTransition");
        _transition.ResetTrigger("FinishLongerTransition");
    }

    private void ResetRequests()
    {
        RegisterKeyboardManager.HasSentRegisterRequest = false;
        LoginKeyboardManager.HasSentLoginRequest = false;
    }
    
    private void CloseMenu()
    {
        if (_menuOption) LoginMenu.SetActive(false);
		else RegisterMenu.SetActive(false);
    }

    private void HideAlerts()
    {
        LoginMenu.transform.Find("Canvas/Login Successful").gameObject.SetActive(false);
        LoginMenu.transform.Find("Canvas/Login Error").gameObject.SetActive(false);
        RegisterMenu.transform.Find("Canvas/Register Successful").gameObject.SetActive(false);
        RegisterMenu.transform.Find("Canvas/Register Error").gameObject.SetActive(false);
    }

    private void CleanFields()
    {
        LoginMainArea.GetComponent<LoginKeyboardManager>().ResetTextFields();
        RegisterMainArea.GetComponent<RegisterKeyboardManager>().ResetTextFields();
    }
}
