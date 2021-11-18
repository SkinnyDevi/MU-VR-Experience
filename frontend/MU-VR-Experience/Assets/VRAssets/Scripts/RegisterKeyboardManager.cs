using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

public class RegisterKeyboardManager : MonoBehaviour
{
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
		if (PwdTextField.text.Equals(ConfirmPwdField.text))
		{
			StartCoroutine(RegisterHandler.CreateUserFromPlayer(EmailTextField.text, PwdTextField.text));
		}
		else
		{
			Debug.Log("Passwords do not match.");
		}
        
    }
}
