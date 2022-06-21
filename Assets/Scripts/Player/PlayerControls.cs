//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Player/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""16f77a22-9053-4b19-af96-e149cacd66a2"",
            ""actions"": [
                {
                    ""name"": ""Move Left/Right"",
                    ""type"": ""Button"",
                    ""id"": ""e93ea270-2bba-44ba-a156-ef50e84750dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""900be613-224a-4b9a-8229-257f8eedb533"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""a938f0da-8d6b-4926-ab32-7f8e09c74463"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ShootFirst"",
                    ""type"": ""Value"",
                    ""id"": ""a401eb31-5be2-4755-ad4e-003e75809b85"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ShootSecond"",
                    ""type"": ""Value"",
                    ""id"": ""d632f93d-94f9-4cb6-9017-f1a993b5a78e"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Grapple"",
                    ""type"": ""Button"",
                    ""id"": ""20f09b45-6732-4fcb-80a9-148f19d2198b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchWeapons"",
                    ""type"": ""Button"",
                    ""id"": ""bfa2df2e-4b46-46b9-88fa-988611dc4089"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""ab9d1d1d-5b88-476a-946c-6dc8a05fc604"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Left/Right"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b77a2f38-f8a1-4c4b-80a5-af922569cd3e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move Left/Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d1e797f7-c497-4c58-beb3-420c141db785"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move Left/Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8f824f94-b340-40e8-93fe-cff1445517a8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8fba064-70a1-46b1-9689-2bd8320394f4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e97854b-db7d-4791-8a35-7dc3d7085f58"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cddf21e-6025-4b86-ba48-9cc193596b7e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShootFirst"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28cf8ca7-ea25-41f8-8675-f93b91837d9d"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Grapple"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""591428dd-8140-4d14-a677-4a70e98aeba2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShootSecond"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94bff071-3ff9-424d-ace4-101cea4db31f"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SwitchWeapons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MoveLeftRight = m_Player.FindAction("Move Left/Right", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_ShootFirst = m_Player.FindAction("ShootFirst", throwIfNotFound: true);
        m_Player_ShootSecond = m_Player.FindAction("ShootSecond", throwIfNotFound: true);
        m_Player_Grapple = m_Player.FindAction("Grapple", throwIfNotFound: true);
        m_Player_SwitchWeapons = m_Player.FindAction("SwitchWeapons", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MoveLeftRight;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_ShootFirst;
    private readonly InputAction m_Player_ShootSecond;
    private readonly InputAction m_Player_Grapple;
    private readonly InputAction m_Player_SwitchWeapons;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveLeftRight => m_Wrapper.m_Player_MoveLeftRight;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @ShootFirst => m_Wrapper.m_Player_ShootFirst;
        public InputAction @ShootSecond => m_Wrapper.m_Player_ShootSecond;
        public InputAction @Grapple => m_Wrapper.m_Player_Grapple;
        public InputAction @SwitchWeapons => m_Wrapper.m_Player_SwitchWeapons;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MoveLeftRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveLeftRight;
                @MoveLeftRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveLeftRight;
                @MoveLeftRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveLeftRight;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @ShootFirst.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootFirst;
                @ShootFirst.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootFirst;
                @ShootFirst.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootFirst;
                @ShootSecond.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootSecond;
                @ShootSecond.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootSecond;
                @ShootSecond.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootSecond;
                @Grapple.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapple;
                @Grapple.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapple;
                @Grapple.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapple;
                @SwitchWeapons.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeapons;
                @SwitchWeapons.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeapons;
                @SwitchWeapons.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeapons;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveLeftRight.started += instance.OnMoveLeftRight;
                @MoveLeftRight.performed += instance.OnMoveLeftRight;
                @MoveLeftRight.canceled += instance.OnMoveLeftRight;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @ShootFirst.started += instance.OnShootFirst;
                @ShootFirst.performed += instance.OnShootFirst;
                @ShootFirst.canceled += instance.OnShootFirst;
                @ShootSecond.started += instance.OnShootSecond;
                @ShootSecond.performed += instance.OnShootSecond;
                @ShootSecond.canceled += instance.OnShootSecond;
                @Grapple.started += instance.OnGrapple;
                @Grapple.performed += instance.OnGrapple;
                @Grapple.canceled += instance.OnGrapple;
                @SwitchWeapons.started += instance.OnSwitchWeapons;
                @SwitchWeapons.performed += instance.OnSwitchWeapons;
                @SwitchWeapons.canceled += instance.OnSwitchWeapons;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMoveLeftRight(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnShootFirst(InputAction.CallbackContext context);
        void OnShootSecond(InputAction.CallbackContext context);
        void OnGrapple(InputAction.CallbackContext context);
        void OnSwitchWeapons(InputAction.CallbackContext context);
    }
}
