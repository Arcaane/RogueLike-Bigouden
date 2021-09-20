// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/AUC_Scripts/Utility/Player2Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Player2Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player2Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player2Controls"",
    ""maps"": [
        {
            ""name"": ""Player2"",
            ""id"": ""c7d1a26b-be43-40cc-9b51-f64f067315ae"",
            ""actions"": [
                {
                    ""name"": ""XButton"",
                    ""type"": ""Button"",
                    ""id"": ""0c531382-db21-4c19-a98e-a10ea4e2ca75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""YButton"",
                    ""type"": ""Button"",
                    ""id"": ""068f7f5f-1d84-4ed2-864e-42a8ac893ab5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BButon"",
                    ""type"": ""Button"",
                    ""id"": ""f2bc2bd5-122e-49fb-9ceb-784cb8b17e24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AButton"",
                    ""type"": ""Button"",
                    ""id"": ""197fdae0-5f8c-41ee-bb0d-53b306183f2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LStick"",
                    ""type"": ""Value"",
                    ""id"": ""f7d2e39d-832c-436e-ada4-bcc8ba865f41"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""5ed23e94-5a81-47ae-a904-994d49e02a1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4f3ff479-9b94-4cb9-9773-e4f6f398c212"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d835b82-8528-4b87-8aaa-1a61a59ef235"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5eef384d-5f4a-4346-88e5-a6efbba1c57e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BButon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef768381-b96c-4465-a7c6-3fd7067413ee"",
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
                    ""id"": ""0a11db0e-abd7-4c41-9fb2-fc7b6be514f3"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03174b5c-4ad8-4fbc-81c5-778e75161e78"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player2
        m_Player2 = asset.FindActionMap("Player2", throwIfNotFound: true);
        m_Player2_XButton = m_Player2.FindAction("XButton", throwIfNotFound: true);
        m_Player2_YButton = m_Player2.FindAction("YButton", throwIfNotFound: true);
        m_Player2_BButon = m_Player2.FindAction("BButon", throwIfNotFound: true);
        m_Player2_AButton = m_Player2.FindAction("AButton", throwIfNotFound: true);
        m_Player2_LStick = m_Player2.FindAction("LStick", throwIfNotFound: true);
        m_Player2_RTrigger = m_Player2.FindAction("RTrigger", throwIfNotFound: true);
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

    // Player2
    private readonly InputActionMap m_Player2;
    private IPlayer2Actions m_Player2ActionsCallbackInterface;
    private readonly InputAction m_Player2_XButton;
    private readonly InputAction m_Player2_YButton;
    private readonly InputAction m_Player2_BButon;
    private readonly InputAction m_Player2_AButton;
    private readonly InputAction m_Player2_LStick;
    private readonly InputAction m_Player2_RTrigger;
    public struct Player2Actions
    {
        private @Player2Controls m_Wrapper;
        public Player2Actions(@Player2Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @XButton => m_Wrapper.m_Player2_XButton;
        public InputAction @YButton => m_Wrapper.m_Player2_YButton;
        public InputAction @BButon => m_Wrapper.m_Player2_BButon;
        public InputAction @AButton => m_Wrapper.m_Player2_AButton;
        public InputAction @LStick => m_Wrapper.m_Player2_LStick;
        public InputAction @RTrigger => m_Wrapper.m_Player2_RTrigger;
        public InputActionMap Get() { return m_Wrapper.m_Player2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer2Actions instance)
        {
            if (m_Wrapper.m_Player2ActionsCallbackInterface != null)
            {
                @XButton.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnXButton;
                @XButton.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnXButton;
                @XButton.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnXButton;
                @YButton.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnYButton;
                @YButton.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnYButton;
                @YButton.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnYButton;
                @BButon.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnBButon;
                @BButon.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnBButon;
                @BButon.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnBButon;
                @AButton.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAButton;
                @AButton.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAButton;
                @AButton.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAButton;
                @LStick.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnLStick;
                @LStick.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnLStick;
                @LStick.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnLStick;
                @RTrigger.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRTrigger;
                @RTrigger.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRTrigger;
                @RTrigger.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRTrigger;
            }
            m_Wrapper.m_Player2ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @XButton.started += instance.OnXButton;
                @XButton.performed += instance.OnXButton;
                @XButton.canceled += instance.OnXButton;
                @YButton.started += instance.OnYButton;
                @YButton.performed += instance.OnYButton;
                @YButton.canceled += instance.OnYButton;
                @BButon.started += instance.OnBButon;
                @BButon.performed += instance.OnBButon;
                @BButon.canceled += instance.OnBButon;
                @AButton.started += instance.OnAButton;
                @AButton.performed += instance.OnAButton;
                @AButton.canceled += instance.OnAButton;
                @LStick.started += instance.OnLStick;
                @LStick.performed += instance.OnLStick;
                @LStick.canceled += instance.OnLStick;
                @RTrigger.started += instance.OnRTrigger;
                @RTrigger.performed += instance.OnRTrigger;
                @RTrigger.canceled += instance.OnRTrigger;
            }
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);
    public interface IPlayer2Actions
    {
        void OnXButton(InputAction.CallbackContext context);
        void OnYButton(InputAction.CallbackContext context);
        void OnBButon(InputAction.CallbackContext context);
        void OnAButton(InputAction.CallbackContext context);
        void OnLStick(InputAction.CallbackContext context);
        void OnRTrigger(InputAction.CallbackContext context);
    }
}
