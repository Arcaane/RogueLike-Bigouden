// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/BAV_Prefab/BAV_PlayerController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @BAV_PlayerController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @BAV_PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BAV_PlayerController"",
    ""maps"": [
        {
            ""name"": ""Player_GK"",
            ""id"": ""2289b125-7141-4017-a061-6c6505b586c4"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""dfa4b4fe-95c9-4a12-8566-ab11730b7f90"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""XAttack"",
                    ""type"": ""Button"",
                    ""id"": ""72633990-087c-4c5c-9117-e9a44f392cbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""YAttack"",
                    ""type"": ""Button"",
                    ""id"": ""58ca634e-3f56-456f-a8b3-da0320a8f40c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AButton"",
                    ""type"": ""Button"",
                    ""id"": ""9821e560-b080-4584-8264-ce58fc5bf2a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BButton"",
                    ""type"": ""Button"",
                    ""id"": ""4d538565-6a14-419f-905f-decf7717c67f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""43fc503a-c3d4-4b05-ad63-d19844cf57c2"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ZQSD"",
                    ""id"": ""9ca15c14-e53d-4df8-88cd-ebfde59cde90"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""038d694a-46e4-4c46-850c-c82911603e2e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4c3af298-28ac-4e14-998b-afc5e1821506"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f5a7eaa2-aa84-4389-b7fb-be637b714f39"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""42773d34-2c9b-441d-8c57-aeebedf0cf85"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""291f2e59-def6-406f-8c4d-2cf12a4ee06e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""XAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""367d327f-522a-48f5-a631-0f02e1209e0a"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""XAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cc37861-0185-495e-a066-4a5dd7b7f44b"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""YAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c859db4-1d97-4a62-82c2-0f6e2c4668cc"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""YAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""653b0900-cc12-4cea-b130-31893de91101"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59151a2d-263f-4133-942f-7f8cf0a39f52"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""AButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4ec3eba-cf8b-4d82-929e-0b30bcc792b5"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""BButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81d8e9fa-0d40-4997-9d2b-d21acae60cbd"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""BButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player_GK
        m_Player_GK = asset.FindActionMap("Player_GK", throwIfNotFound: true);
        m_Player_GK_Move = m_Player_GK.FindAction("Move", throwIfNotFound: true);
        m_Player_GK_XAttack = m_Player_GK.FindAction("XAttack", throwIfNotFound: true);
        m_Player_GK_YAttack = m_Player_GK.FindAction("YAttack", throwIfNotFound: true);
        m_Player_GK_AButton = m_Player_GK.FindAction("AButton", throwIfNotFound: true);
        m_Player_GK_BButton = m_Player_GK.FindAction("BButton", throwIfNotFound: true);
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

    // Player_GK
    private readonly InputActionMap m_Player_GK;
    private IPlayer_GKActions m_Player_GKActionsCallbackInterface;
    private readonly InputAction m_Player_GK_Move;
    private readonly InputAction m_Player_GK_XAttack;
    private readonly InputAction m_Player_GK_YAttack;
    private readonly InputAction m_Player_GK_AButton;
    private readonly InputAction m_Player_GK_BButton;
    public struct Player_GKActions
    {
        private @BAV_PlayerController m_Wrapper;
        public Player_GKActions(@BAV_PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_GK_Move;
        public InputAction @XAttack => m_Wrapper.m_Player_GK_XAttack;
        public InputAction @YAttack => m_Wrapper.m_Player_GK_YAttack;
        public InputAction @AButton => m_Wrapper.m_Player_GK_AButton;
        public InputAction @BButton => m_Wrapper.m_Player_GK_BButton;
        public InputActionMap Get() { return m_Wrapper.m_Player_GK; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_GKActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer_GKActions instance)
        {
            if (m_Wrapper.m_Player_GKActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnMove;
                @XAttack.started -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnXAttack;
                @XAttack.performed -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnXAttack;
                @XAttack.canceled -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnXAttack;
                @YAttack.started -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnYAttack;
                @YAttack.performed -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnYAttack;
                @YAttack.canceled -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnYAttack;
                @AButton.started -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnAButton;
                @AButton.performed -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnAButton;
                @AButton.canceled -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnAButton;
                @BButton.started -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnBButton;
                @BButton.performed -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnBButton;
                @BButton.canceled -= m_Wrapper.m_Player_GKActionsCallbackInterface.OnBButton;
            }
            m_Wrapper.m_Player_GKActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @XAttack.started += instance.OnXAttack;
                @XAttack.performed += instance.OnXAttack;
                @XAttack.canceled += instance.OnXAttack;
                @YAttack.started += instance.OnYAttack;
                @YAttack.performed += instance.OnYAttack;
                @YAttack.canceled += instance.OnYAttack;
                @AButton.started += instance.OnAButton;
                @AButton.performed += instance.OnAButton;
                @AButton.canceled += instance.OnAButton;
                @BButton.started += instance.OnBButton;
                @BButton.performed += instance.OnBButton;
                @BButton.canceled += instance.OnBButton;
            }
        }
    }
    public Player_GKActions @Player_GK => new Player_GKActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayer_GKActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnXAttack(InputAction.CallbackContext context);
        void OnYAttack(InputAction.CallbackContext context);
        void OnAButton(InputAction.CallbackContext context);
        void OnBButton(InputAction.CallbackContext context);
    }
}
