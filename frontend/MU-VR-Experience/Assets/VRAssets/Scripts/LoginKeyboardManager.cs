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

	static GameObject s_processWheel;
    EventSystem _system;
	InputMaster _keyboardControls;
	bool _tabPress, _shiftPress;

	void Awake()
	{
		_keyboardControls = new InputMaster();
		_keyboardControls.Enable();
		_keyboardControls.Menus.FormInputMovement.performed += _ => _tabPress = true;
		_keyboardControls.Menus.FormInputMovementRelease.performed += _ => _tabPress = false;
		_keyboardControls.Menus.PreviousFormInputMovement.performed += _ => _shiftPress = true;
		_keyboardControls.Menus.PreviousFormInputMovementRelease.performed += _ => _shiftPress = false;
		_keyboardControls.Menus.Submit.performed += _ => SubmitForm();
	}

    void Start()
    {
        _system = EventSystem.current;
		_tabPress = false;
		_shiftPress = false;
		s_processWheel = gameObject.transform.Find("ProcessWheel").gameObject;
		s_processWheel.SetActive(false);
        EmailField.Select();
    }

	void Update()
	{
		if (_tabPress && _shiftPress) PreviousField();
		else if (_tabPress) NextField();
	}

	private void NextField()
	{
		Selectable next = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
		if (next != null) next.Select();
		_tabPress = false;
	}

	private void PreviousField()
	{
		Selectable previous = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
        if (previous != null) previous.Select();
		_tabPress = false;
	}

	private void SubmitForm()
	{
		SendData();
	}

	public static void ToggleProcessWheel(bool toggle)
	{
		s_processWheel.SetActive(toggle);
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
