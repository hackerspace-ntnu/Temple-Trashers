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
                    ""name"": ""Cancel"",
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
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""6702a6d5-4f86-4f26-90c0-615b6eeedd8c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ready for next wave"",
                    ""type"": ""Button"",
                    ""id"": ""0292f4fa-f566-411f-a1a7-367de1e92915"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move tower"",
                    ""type"": ""Button"",
                    ""id"": ""34be8f5b-dd5f-48f5-91c6-67c76e24d444"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0c19d4af-bdfa-42be-b800-b205869f9363"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""edce3a85-93ba-4e26-a73d-23a6f59585f9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8b796246-44d5-4906-97b4-fba9651cc7ea"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bbdb5578-e7cf-4ba5-aa8b-0c46054922ad"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2c4428d3-d60d-4cd0-b18d-209b6610405f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d6d2d0ef-f903-4692-9eb5-194e8394bec9"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""947a7270-4410-4212-95dd-4e43ef3a597e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""10541b19-10c9-4da5-a6d2-13cf315c3d52"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2dafee8c-2ec4-49b8-8779-a805cd5531c5"",
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
                    ""id"": ""f961be31-7635-4b79-82e9-200fa38dea94"",
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
                    ""id"": ""b9b2bf0f-9ee3-4df3-943f-051e33b4ecd3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8194008d-3a54-41a2-ad62-5b376082a62e"",
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
                    ""id"": ""bac400b0-2247-4df1-9c23-f98655a46a99"",
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
                    ""id"": ""1fa37b50-7164-456f-9b73-1a444e0b65b8"",
                    ""path"": ""<Keyboard>/#(E)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1aab1220-e456-4e6f-afdf-dd88a698329e"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3a671d1-a478-4c46-87e1-3ba1c1720988"",
                    ""path"": ""<Keyboard>/#(R)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ddd6f56-69c5-495d-b7f6-0d6711169908"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eeed5e82-f945-41b1-924f-37ce63a89e81"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d61bf77-1cda-452c-927e-df6ea1888261"",
                    ""path"": ""<Keyboard>/#(N)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Ready for next wave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c1bc488-9030-434a-a8a3-eaf1bad2bea1"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f01a655d-a686-401a-83fc-abf3fddc7d5f"",
                    ""path"": ""<Keyboard>/#(P)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43c9fc11-5452-4f1b-b055-121563ed34c0"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move tower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac308be8-d7ad-4bf2-a443-5a947eaf84b5"",
                    ""path"": ""<Keyboard>/#(M)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move tower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""deaea30b-aee8-4db5-bc8c-98b23625b044"",
            ""actions"": [
                {
                    ""name"": ""MouseMove"",
                    ""type"": ""Value"",
                    ""id"": ""1aa781e4-b54b-4cca-b2aa-0f093d7005e1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""5c44b654-f1cc-47d0-a098-d6bb4dd2bb35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""09c7330c-b070-42f6-8ae4-0d8ee27aa928"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4162ed88-c961-49d0-8761-fe84917eb277"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MouseClick"",
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
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
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
        m_MovePlayer_Cancel = m_MovePlayer.FindAction("Cancel", throwIfNotFound: true);
        m_MovePlayer_Aim = m_MovePlayer.FindAction("Aim", throwIfNotFound: true);
        m_MovePlayer_Pause = m_MovePlayer.FindAction("Pause", throwIfNotFound: true);
        m_MovePlayer_Readyfornextwave = m_MovePlayer.FindAction("Ready for next wave", throwIfNotFound: true);
        m_MovePlayer_Movetower = m_MovePlayer.FindAction("Move tower", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_MouseMove = m_Menu.FindAction("MouseMove", throwIfNotFound: true);
        m_Menu_MouseClick = m_Menu.FindAction("MouseClick", throwIfNotFound: true);
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
    private readonly InputAction m_MovePlayer_Cancel;
    private readonly InputAction m_MovePlayer_Aim;
    private readonly InputAction m_MovePlayer_Pause;
    private readonly InputAction m_MovePlayer_Readyfornextwave;
    private readonly InputAction m_MovePlayer_Movetower;
    public struct MovePlayerActions
    {
        private @PlayerControls m_Wrapper;
        public MovePlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_MovePlayer_Move;
        public InputAction @Interact => m_Wrapper.m_MovePlayer_Interact;
        public InputAction @Select => m_Wrapper.m_MovePlayer_Select;
        public InputAction @Cancel => m_Wrapper.m_MovePlayer_Cancel;
        public InputAction @Aim => m_Wrapper.m_MovePlayer_Aim;
        public InputAction @Pause => m_Wrapper.m_MovePlayer_Pause;
        public InputAction @Readyfornextwave => m_Wrapper.m_MovePlayer_Readyfornextwave;
        public InputAction @Movetower => m_Wrapper.m_MovePlayer_Movetower;
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
                @Cancel.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnCancel;
                @Aim.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnAim;
                @Pause.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnPause;
                @Readyfornextwave.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnReadyfornextwave;
                @Readyfornextwave.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnReadyfornextwave;
                @Readyfornextwave.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnReadyfornextwave;
                @Movetower.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMovetower;
                @Movetower.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMovetower;
                @Movetower.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMovetower;
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
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Readyfornextwave.started += instance.OnReadyfornextwave;
                @Readyfornextwave.performed += instance.OnReadyfornextwave;
                @Readyfornextwave.canceled += instance.OnReadyfornextwave;
                @Movetower.started += instance.OnMovetower;
                @Movetower.performed += instance.OnMovetower;
                @Movetower.canceled += instance.OnMovetower;
            }
        }
    }
    public MovePlayerActions @MovePlayer => new MovePlayerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_MouseMove;
    private readonly InputAction m_Menu_MouseClick;
    public struct MenuActions
    {
        private @PlayerControls m_Wrapper;
        public MenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseMove => m_Wrapper.m_Menu_MouseMove;
        public InputAction @MouseClick => m_Wrapper.m_Menu_MouseClick;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @MouseMove.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMouseMove;
                @MouseMove.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMouseMove;
                @MouseMove.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMouseMove;
                @MouseClick.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMouseClick;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseMove.started += instance.OnMouseMove;
                @MouseMove.performed += instance.OnMouseMove;
                @MouseMove.canceled += instance.OnMouseMove;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
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
        void OnCancel(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnReadyfornextwave(InputAction.CallbackContext context);
        void OnMovetower(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnMouseMove(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
    }
}
