// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""MovePlayer"",
            ""id"": ""06bc85e9-2841-426e-85d4-a63058738660"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f58f564c-dd7b-4661-8f49-74dbe2b5c641"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""fd4bde5c-c78c-49e0-8b99-76eeccd07756"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""9331d260-700a-4141-b0c7-a996b55bb661"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""edbb2489-0819-43dd-bbbf-39f2ad7c7800"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""670ffa49-f141-4faa-b767-43bb99498b67"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b4b324da-3b3d-41cb-b081-5fa29dc82106"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ccb76c3-0a4d-46a5-b31c-c82dbc4a390c"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bbeed11-d267-4918-a98f-2a919b1981d1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4a09441-b547-4f50-8193-31672f588400"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""767861f2-1ef3-49f8-9778-934b8125bcff"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MovePlayer
        m_MovePlayer = asset.FindActionMap("MovePlayer", throwIfNotFound: true);
        m_MovePlayer_Move = m_MovePlayer.FindAction("Move", throwIfNotFound: true);
        m_MovePlayer_Interact = m_MovePlayer.FindAction("Interact", throwIfNotFound: true);
        m_MovePlayer_Select = m_MovePlayer.FindAction("Select", throwIfNotFound: true);
        m_MovePlayer_Back = m_MovePlayer.FindAction("Back", throwIfNotFound: true);
        m_MovePlayer_Aim = m_MovePlayer.FindAction("Aim", throwIfNotFound: true);
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

    // MovePlayer
    private readonly InputActionMap m_MovePlayer;
    private IMovePlayerActions m_MovePlayerActionsCallbackInterface;
    private readonly InputAction m_MovePlayer_Move;
    private readonly InputAction m_MovePlayer_Interact;
    private readonly InputAction m_MovePlayer_Select;
    private readonly InputAction m_MovePlayer_Back;
    private readonly InputAction m_MovePlayer_Aim;
    public struct MovePlayerActions
    {
        private @PlayerControls m_Wrapper;
        public MovePlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_MovePlayer_Move;
        public InputAction @Interact => m_Wrapper.m_MovePlayer_Interact;
        public InputAction @Select => m_Wrapper.m_MovePlayer_Select;
        public InputAction @Back => m_Wrapper.m_MovePlayer_Back;
        public InputAction @Aim => m_Wrapper.m_MovePlayer_Aim;
        public InputActionMap Get() { return m_Wrapper.m_MovePlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovePlayerActions set) { return set.Get(); }
        public void SetCallbacks(IMovePlayerActions instance)
        {
            if (m_Wrapper.m_MovePlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnInteract;
                @Select.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnSelect;
                @Back.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnBack;
                @Aim.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_MovePlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public MovePlayerActions @MovePlayer => new MovePlayerActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IMovePlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
