using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using TMPro;

public class LoginKeyboardManager : MonoBehaviour
{
    public static bool HasSentLoginRequest = false;
    public Selectable EmailField;
    public TMP_InputField EmailTextField, PwdTextField;
    public Button SubmitButton;

    EventSystem system;

    void Start()
    {
        system = EventSystem.current;
        EmailField.Select();
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.tabKey.isPressed && keyboard.leftShiftKey.isPressed)
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            
            if (previous != null) previous.Select();
        }
        else if (keyboard.tabKey.isPressed)
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null) next.Select();
        }
        else if (keyboard.enterKey.isPressed)
        {
            SendData();
        }
    }

    public void SendData()
    {
        if (!HasSentLoginRequest)
        {
            StartCoroutine(LoginHandler.SignIn(EmailTextField.text, PwdTextField.text));
            HasSentLoginRequest = true;
        }
    }

    public void ResetTextFields()
    {
        EmailTextField.text = "";
        PwdTextField.text = "";
    }
}
