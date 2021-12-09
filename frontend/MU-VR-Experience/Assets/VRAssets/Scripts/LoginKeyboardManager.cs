using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

public class LoginKeyboardManager : MonoBehaviour
{
    public static bool HasSentLoginRequest = false;
    public Selectable EmailField;
    public TMP_InputField EmailTextField, PwdTextField;
    public Button SubmitButton;

    EventSystem system;
	InputMaster keyboardControls;
	bool tabPress, shiftPress;
	static GameObject processWheel;

	void Awake()
	{
		keyboardControls = new InputMaster();
		keyboardControls.Enable();
		keyboardControls.Menus.FormInputMovement.performed += _ => tabPress = true;
		keyboardControls.Menus.FormInputMovementRelease.performed += _ => tabPress = false;
		keyboardControls.Menus.PreviousFormInputMovement.performed += _ => shiftPress = true;
		keyboardControls.Menus.PreviousFormInputMovementRelease.performed += _ => shiftPress = false;
		keyboardControls.Menus.Submit.performed += _ => SubmitForm();
	}

    void Start()
    {
        system = EventSystem.current;
		tabPress = false;
		shiftPress = false;
		processWheel = gameObject.transform.Find("ProcessWheel").gameObject;
		processWheel.SetActive(false);
        EmailField.Select();
    }

	void Update()
	{
		if (tabPress && shiftPress) PreviousField();
		else if (tabPress) NextField();
	}

	void NextField()
	{
		Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
		if (next != null) next.Select();
		tabPress = false;
	}

	void PreviousField()
	{
		Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
        if (previous != null) previous.Select();
		tabPress = false;
	}

	void SubmitForm()
	{
		SendData();
	}

	public static void ToggleProcessWheel(bool toggle)
	{
		processWheel.SetActive(toggle);
	}

    public void SendData()
    {
		if (GameObject.Find("Login Menu").activeSelf)
		{
			ToggleProcessWheel(true);
			if (LoginHandler.ValidationRetry) HasSentLoginRequest = false;
			if (!HasSentLoginRequest)
			{
				StartCoroutine(LoginHandler.SignIn(EmailTextField.text, PwdTextField.text));
				HasSentLoginRequest = true;
			}
		}
    }

    public void ResetTextFields()
    {
        EmailTextField.text = "";
        PwdTextField.text = "";
    }
}
