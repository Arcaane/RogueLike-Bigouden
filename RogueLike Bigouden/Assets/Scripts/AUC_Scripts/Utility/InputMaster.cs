// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Object = UnityEngine.Object;

public class InputMaster : IInputActionCollection, IDisposable
{
    // Player
    private readonly InputActionMap m_Player;
    private readonly InputAction m_Player_HigherAttack;
    private readonly InputAction m_Player_LowerAttack;
    private readonly InputAction m_Player_MoveInput;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private int m_XboxGamepadSchemeIndex = -1;

    public InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""efd0ec73-293e-43aa-ac9d-9a4f19886e86"",
            ""actions"": [
                {
                    ""name"": ""LowerAttack"",
                    ""type"": ""Button"",
                    ""id"": ""39132d96-e974-45e5-bb79-1e17ed683e6a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HigherAttack"",
                    ""type"": ""Button"",
                    ""id"": ""4b87f8f2-ca98-4d69-bd73-0ad20b4b0e2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveInput"",
                    ""type"": ""Button"",
                    ""id"": ""90ee15ed-81e3-4e5d-8cd7-c2ebf8266cad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""035a5b43-a943-4d67-b93f-9cd1222d60fb"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LowerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4726818-4cbc-4d47-8cee-3ffdc7671b8b"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HigherAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3350a1f-f7b7-46c4-bb4d-a051622ba1a8"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85598d80-a942-46b8-a523-17430bca8ed3"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97e949cf-1c8d-48ca-a222-488bc26c3a52"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""798d2638-60af-425e-809e-c4b6282be8a8"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox Gamepad"",
            ""bindingGroup"": ""Xbox Gamepad"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", true);
        m_Player_LowerAttack = m_Player.FindAction("LowerAttack", true);
        m_Player_HigherAttack = m_Player.FindAction("HigherAttack", true);
        m_Player_MoveInput = m_Player.FindAction("MoveInput", true);
    }

    public InputActionAsset asset { get; }
    public PlayerActions Player => new PlayerActions(this);

    public InputControlScheme XboxGamepadScheme
    {
        get
        {
            if (m_XboxGamepadSchemeIndex == -1) m_XboxGamepadSchemeIndex = asset.FindControlSchemeIndex("Xbox Gamepad");
            return asset.controlSchemes[m_XboxGamepadSchemeIndex];
        }
    }

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
        private readonly InputMaster m_Wrapper;

        public PlayerActions(InputMaster wrapper)
        {
            m_Wrapper = wrapper;
        }

        public InputAction LowerAttack => m_Wrapper.m_Player_LowerAttack;
        public InputAction HigherAttack => m_Wrapper.m_Player_HigherAttack;
        public InputAction MoveInput => m_Wrapper.m_Player_MoveInput;

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
                LowerAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLowerAttack;
                LowerAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLowerAttack;
                LowerAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLowerAttack;
                HigherAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHigherAttack;
                HigherAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHigherAttack;
                HigherAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHigherAttack;
                MoveInput.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveInput;
                MoveInput.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveInput;
                MoveInput.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveInput;
            }

            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                LowerAttack.started += instance.OnLowerAttack;
                LowerAttack.performed += instance.OnLowerAttack;
                LowerAttack.canceled += instance.OnLowerAttack;
                HigherAttack.started += instance.OnHigherAttack;
                HigherAttack.performed += instance.OnHigherAttack;
                HigherAttack.canceled += instance.OnHigherAttack;
                MoveInput.started += instance.OnMoveInput;
                MoveInput.performed += instance.OnMoveInput;
                MoveInput.canceled += instance.OnMoveInput;
            }
        }
    }

    public interface IPlayerActions
    {
        void OnLowerAttack(InputAction.CallbackContext context);
        void OnHigherAttack(InputAction.CallbackContext context);
        void OnMoveInput(InputAction.CallbackContext context);
    }
}