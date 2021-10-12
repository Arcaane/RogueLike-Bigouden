// GENERATED AUTOMATICALLY FROM 'Assets/InputController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Object = UnityEngine.Object;

public class InputController : IInputActionCollection, IDisposable
{
    // Player
    private readonly InputActionMap m_Player;
    private readonly InputAction m_Player_LowerAttack;
    private readonly InputAction m_Player_MoveInput;
    private readonly InputAction m_Player_UpperAttack;
    private IPlayerActions m_PlayerActionsCallbackInterface;

    public InputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputController"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""940d032a-b701-49f5-b8b5-468adbfb521b"",
            ""actions"": [
                {
                    ""name"": ""LowerAttack"",
                    ""type"": ""Button"",
                    ""id"": ""e7778ee4-badd-4fae-8aaf-829c83ddbc54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpperAttack"",
                    ""type"": ""Button"",
                    ""id"": ""a9e46390-1519-4f11-a11d-8a1cf838f901"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""924443ac-83cd-417a-82e4-1a863351c881"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e7126b40-b546-4ebf-be40-4e6eb278fbca"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LowerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62f59fe7-8997-4787-af73-1d16ca319b21"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpperAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbb3bcce-de37-4bd9-be9e-bcb6bc28de1f"",
                    ""path"": ""<Gamepad>/leftStick"",
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
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", true);
        m_Player_LowerAttack = m_Player.FindAction("LowerAttack", true);
        m_Player_UpperAttack = m_Player.FindAction("UpperAttack", true);
        m_Player_MoveInput = m_Player.FindAction("MoveInput", true);
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
        private readonly InputController m_Wrapper;

        public PlayerActions(InputController wrapper)
        {
            m_Wrapper = wrapper;
        }

        public InputAction LowerAttack => m_Wrapper.m_Player_LowerAttack;
        public InputAction UpperAttack => m_Wrapper.m_Player_UpperAttack;
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
                UpperAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpperAttack;
                UpperAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpperAttack;
                UpperAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpperAttack;
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
                UpperAttack.started += instance.OnUpperAttack;
                UpperAttack.performed += instance.OnUpperAttack;
                UpperAttack.canceled += instance.OnUpperAttack;
                MoveInput.started += instance.OnMoveInput;
                MoveInput.performed += instance.OnMoveInput;
                MoveInput.canceled += instance.OnMoveInput;
            }
        }
    }

    public interface IPlayerActions
    {
        void OnLowerAttack(InputAction.CallbackContext context);
        void OnUpperAttack(InputAction.CallbackContext context);
        void OnMoveInput(InputAction.CallbackContext context);
    }
}