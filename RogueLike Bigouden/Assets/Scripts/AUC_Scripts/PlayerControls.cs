// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Object = UnityEngine.Object;

public class PlayerControls : IInputActionCollection, IDisposable
{
    // Player
    private readonly InputActionMap m_Player;
    private readonly InputAction m_Player_AButton;
    private readonly InputAction m_Player_BButton;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_XAttack;
    private readonly InputAction m_Player_YAttack;
    private IPlayerActions m_PlayerActionsCallbackInterface;

    public PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
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
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""291f2e59-def6-406f-8c4d-2cf12a4ee06e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
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
                    ""groups"": """",
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
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""BButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", true);
        m_Player_Move = m_Player.FindAction("Move", true);
        m_Player_XAttack = m_Player.FindAction("XAttack", true);
        m_Player_YAttack = m_Player.FindAction("YAttack", true);
        m_Player_AButton = m_Player.FindAction("AButton", true);
        m_Player_BButton = m_Player.FindAction("BButton", true);
    }

    public InputActionAsset asset { get; }
    public PlayerActions Player => new PlayerActions(this);

    public void Dispose()
    {
        Object.Destroy(asset);
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

    public struct PlayerActions
    {
        private readonly PlayerControls m_Wrapper;

        public PlayerActions(PlayerControls wrapper)
        {
            m_Wrapper = wrapper;
        }

        public InputAction Move => m_Wrapper.m_Player_Move;
        public InputAction XAttack => m_Wrapper.m_Player_XAttack;
        public InputAction YAttack => m_Wrapper.m_Player_YAttack;
        public InputAction AButton => m_Wrapper.m_Player_AButton;
        public InputAction BButton => m_Wrapper.m_Player_BButton;

        public InputActionMap Get()
        {
            return m_Wrapper.m_Player;
        }

        public void Enable()
        {
            Get().Enable();
        }

        public void Disable()
        {
            Get().Disable();
        }

        public bool enabled => Get().enabled;

        public static implicit operator InputActionMap(PlayerActions set)
        {
            return set.Get();
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                XAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXAttack;
                XAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXAttack;
                XAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXAttack;
                YAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnYAttack;
                YAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnYAttack;
                YAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnYAttack;
                AButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAButton;
                AButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAButton;
                AButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAButton;
                BButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBButton;
                BButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBButton;
                BButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBButton;
            }

            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
                XAttack.started += instance.OnXAttack;
                XAttack.performed += instance.OnXAttack;
                XAttack.canceled += instance.OnXAttack;
                YAttack.started += instance.OnYAttack;
                YAttack.performed += instance.OnYAttack;
                YAttack.canceled += instance.OnYAttack;
                AButton.started += instance.OnAButton;
                AButton.performed += instance.OnAButton;
                AButton.canceled += instance.OnAButton;
                BButton.started += instance.OnBButton;
                BButton.performed += instance.OnBButton;
                BButton.canceled += instance.OnBButton;
            }
        }
    }

    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnXAttack(InputAction.CallbackContext context);
        void OnYAttack(InputAction.CallbackContext context);
        void OnAButton(InputAction.CallbackContext context);
        void OnBButton(InputAction.CallbackContext context);
    }
}