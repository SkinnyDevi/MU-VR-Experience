using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using TMPro;

public class RegisterKeyboardManager : MonoBehaviour
{
    public static bool HasSentRegisterRequest = false;
    public Selectable EmailField;
    public TMP_InputField EmailTextField, PwdTextField, ConfirmPwdField;
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
        if (!HasSentRegisterRequest)
        {
            StartCoroutine(RegisterHandler.CreateUserFromPlayer(EmailTextField.text, PwdTextField.text, ConfirmPwdField.text));
            HasSentRegisterRequest = true;
        }
    }

    public void ResetTextFields()
    {
        EmailTextField.text = "";
        PwdTextField.text = "";
        ConfirmPwdField.text = "";
    }
}
