// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c345157d-9261-48bf-8de0-402a51a0d129"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5253b7fc-4a1c-4ef0-be5c-cb0cb5af72c7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""a060199b-55fd-4f40-ae99-86fed8aec20b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""afa46a27-d73e-4d32-b780-b1ff9c38cb48"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""85dfccf1-e39f-4ca1-916f-46a0da16036e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8dff48ee-65d7-4e6f-8dad-dd4f9a7ab2d4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7f4f289c-fbbc-4a8a-b515-74c6fa0046a6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""73a78577-ad7e-46af-bf6d-56240d247176"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""561e2a07-850b-4b2d-8e4c-be53b427fecb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menus"",
            ""id"": ""99a2bc53-9be8-4367-9eec-eb7be95d0fe2"",
            ""actions"": [
                {
                    ""name"": ""Settings"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ad837957-e7df-41fe-a416-cff9f2f1e1dd"",
                    ""expectedControlType"": ""Key"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FormInputMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b4f93acf-f79f-4130-99ef-f748dcfcbe97"",
                    ""expectedControlType"": ""Key"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviousFormInputMovement"",
                    ""type"": ""Button"",
                    ""id"": ""68a737bd-910c-43e7-80ca-84c615e25e36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FormInputMovementRelease"",
                    ""type"": ""Button"",
                    ""id"": ""4a4dc01f-c2fb-410f-807c-2a0146899d34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviousFormInputMovementRelease"",
                    ""type"": ""Button"",
                    ""id"": ""63ebb324-4be6-4af6-856e-90bd886f1210"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""1a09838b-05c5-46b3-b6a3-96e5ae357004"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a4451d87-73c9-4e97-99bc-0a29376a1cac"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": ""Press(pressPoint=1,behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Settings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""053421ba-c32e-4fb5-8a90-ae75537aabf4"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FormInputMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf2b802d-dd4a-4e05-9897-e638536d57ca"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousFormInputMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3ff3317-e0bd-450a-a8ac-2798d96cfbea"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FormInputMovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1a9b78e-530b-443f-8b0e-adf95217baf1"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousFormInputMovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b57e0c5-a96e-41e0-ada9-6e1903bf76df"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_MouseClick = m_Player.FindAction("MouseClick", throwIfNotFound: true);
        // Menus
        m_Menus = asset.FindActionMap("Menus", throwIfNotFound: true);
        m_Menus_Settings = m_Menus.FindAction("Settings", throwIfNotFound: true);
        m_Menus_FormInputMovement = m_Menus.FindAction("FormInputMovement", throwIfNotFound: true);
        m_Menus_PreviousFormInputMovement = m_Menus.FindAction("PreviousFormInputMovement", throwIfNotFound: true);
        m_Menus_FormInputMovementRelease = m_Menus.FindAction("FormInputMovementRelease", throwIfNotFound: true);
        m_Menus_PreviousFormInputMovementRelease = m_Menus.FindAction("PreviousFormInputMovementRelease", throwIfNotFound: true);
        m_Menus_Submit = m_Menus.FindAction("Submit", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_MouseClick;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @MouseClick => m_Wrapper.m_Player_MouseClick;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @MouseClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Menus
    private readonly InputActionMap m_Menus;
    private IMenusActions m_MenusActionsCallbackInterface;
    private readonly InputAction m_Menus_Settings;
    private readonly InputAction m_Menus_FormInputMovement;
    private readonly InputAction m_Menus_PreviousFormInputMovement;
    private readonly InputAction m_Menus_FormInputMovementRelease;
    private readonly InputAction m_Menus_PreviousFormInputMovementRelease;
    private readonly InputAction m_Menus_Submit;
    public struct MenusActions
    {
        private @InputMaster m_Wrapper;
        public MenusActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Settings => m_Wrapper.m_Menus_Settings;
        public InputAction @FormInputMovement => m_Wrapper.m_Menus_FormInputMovement;
        public InputAction @PreviousFormInputMovement => m_Wrapper.m_Menus_PreviousFormInputMovement;
        public InputAction @FormInputMovementRelease => m_Wrapper.m_Menus_FormInputMovementRelease;
        public InputAction @PreviousFormInputMovementRelease => m_Wrapper.m_Menus_PreviousFormInputMovementRelease;
        public InputAction @Submit => m_Wrapper.m_Menus_Submit;
        public InputActionMap Get() { return m_Wrapper.m_Menus; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenusActions set) { return set.Get(); }
        public void SetCallbacks(IMenusActions instance)
        {
            if (m_Wrapper.m_MenusActionsCallbackInterface != null)
            {
                @Settings.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnSettings;
                @Settings.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnSettings;
                @Settings.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnSettings;
                @FormInputMovement.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnFormInputMovement;
                @FormInputMovement.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnFormInputMovement;
                @FormInputMovement.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnFormInputMovement;
                @PreviousFormInputMovement.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnPreviousFormInputMovement;
                @PreviousFormInputMovement.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnPreviousFormInputMovement;
                @PreviousFormInputMovement.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnPreviousFormInputMovement;
                @FormInputMovementRelease.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnFormInputMovementRelease;
                @FormInputMovementRelease.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnFormInputMovementRelease;
                @FormInputMovementRelease.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnFormInputMovementRelease;
                @PreviousFormInputMovementRelease.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnPreviousFormInputMovementRelease;
                @PreviousFormInputMovementRelease.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnPreviousFormInputMovementRelease;
                @PreviousFormInputMovementRelease.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnPreviousFormInputMovementRelease;
                @Submit.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnSubmit;
            }
            m_Wrapper.m_MenusActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Settings.started += instance.OnSettings;
                @Settings.performed += instance.OnSettings;
                @Settings.canceled += instance.OnSettings;
                @FormInputMovement.started += instance.OnFormInputMovement;
                @FormInputMovement.performed += instance.OnFormInputMovement;
                @FormInputMovement.canceled += instance.OnFormInputMovement;
                @PreviousFormInputMovement.started += instance.OnPreviousFormInputMovement;
                @PreviousFormInputMovement.performed += instance.OnPreviousFormInputMovement;
                @PreviousFormInputMovement.canceled += instance.OnPreviousFormInputMovement;
                @FormInputMovementRelease.started += instance.OnFormInputMovementRelease;
                @FormInputMovementRelease.performed += instance.OnFormInputMovementRelease;
                @FormInputMovementRelease.canceled += instance.OnFormInputMovementRelease;
                @PreviousFormInputMovementRelease.started += instance.OnPreviousFormInputMovementRelease;
                @PreviousFormInputMovementRelease.performed += instance.OnPreviousFormInputMovementRelease;
                @PreviousFormInputMovementRelease.canceled += instance.OnPreviousFormInputMovementRelease;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
            }
        }
    }
    public MenusActions @Menus => new MenusActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
    }
    public interface IMenusActions
    {
        void OnSettings(InputAction.CallbackContext context);
        void OnFormInputMovement(InputAction.CallbackContext context);
        void OnPreviousFormInputMovement(InputAction.CallbackContext context);
        void OnFormInputMovementRelease(InputAction.CallbackContext context);
        void OnPreviousFormInputMovementRelease(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
    }
}
