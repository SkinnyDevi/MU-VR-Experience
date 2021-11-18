using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

public class LoginKeyboardManager : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            
            if (previous != null) previous.Select();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null) next.Select();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SendData();
        }
    }

    public void SendData()
    {
        StartCoroutine(LoginHandler.SignIn(EmailTextField.text, PwdTextField.text));
    }
}
