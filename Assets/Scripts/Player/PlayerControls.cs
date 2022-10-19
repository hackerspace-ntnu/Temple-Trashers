// GENERATED AUTOMATICALLY FROM 'Assets/InputSettings/PlayerControls.inputactions'

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
                },
                {
                    ""name"": ""DUp"",
                    ""type"": ""Button"",
                    ""id"": ""ae0a9726-d0a4-4757-81f8-3683dffdbb50"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DDown"",
                    ""type"": ""Button"",
                    ""id"": ""42a6c193-0d8b-472e-bc38-c688b7372cdc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DLeft"",
                    ""type"": ""Button"",
                    ""id"": ""53e9d0f4-0937-4c45-8553-462f3d4f5258"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DRight"",
                    ""type"": ""Button"",
                    ""id"": ""c47fb957-6919-48e8-9969-a148317e8161"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Join"",
                    ""type"": ""Value"",
                    ""id"": ""2890a695-128d-456b-bdd7-811039967d3a"",
                    ""expectedControlType"": """",
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
                    ""path"": ""<Gamepad>/buttonSouth"",
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
                    ""path"": ""<Gamepad>/buttonEast"",
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
                    ""id"": ""fbea72d5-397f-4fc6-9980-39fb1360484f"",
                    ""path"": ""<Gamepad>/systemButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""07799a7b-60d6-43de-a68c-9fd8c16c3d9e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76b11653-92b3-485a-b4aa-007a2496413d"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6091ca3-d08f-4892-bb79-738b4048a4c7"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb45578f-4af0-4821-a136-3d35355f1a6f"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1c8eaae-a87a-4fd2-950e-2bc769e8dd49"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd6b730a-8c7b-48ad-85b0-b60d30f5b420"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fcf7edc-b838-46b0-b483-f67f08c9a0f8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61e9bb17-a300-4297-93c6-b6f4a142a27f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Join"",
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
                },
                {
                    ""name"": ""Cancel_Menu"",
                    ""type"": ""Button"",
                    ""id"": ""feb7851d-a86e-4ac7-9c45-b84dd4523100"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Join"",
                    ""type"": ""Value"",
                    ""id"": ""e9fbbe78-1c62-40d3-8f6c-807a8fc2b2c9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6c2d50d1-dee0-41ad-9bc4-725783a200a5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""8ce44a95-781a-4489-9b42-26062c0d3d0b"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""de0c889b-37a7-4bd0-9ed5-67c2355efc6c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Cancel_Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc1d4a7f-a2ed-4e9e-9471-bf8ab3cb4abb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel_Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a115a22b-698c-4d8b-a0ae-e9d587632c1e"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18b6c6d4-f15b-462e-be9b-dd25a9252f93"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc68d791-0284-4971-b4f8-f5fb56d66c93"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1be80b7-629f-4fec-8fb0-b0ffe7ff4758"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f3e7673-e23d-433b-b8af-eb91256eeeaf"",
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
                    ""id"": ""4ea5fe3a-0c2e-4583-a750-83e78cda4c51"",
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
                    ""id"": ""c73ee4d1-7d98-4531-a3c5-2dcafd736a6c"",
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
                    ""id"": ""8c1cfbbc-eab8-4460-9110-09f1311904a6"",
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
                    ""id"": ""4f6a89f8-4c66-479f-bbdc-574e53b05125"",
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
                    ""id"": ""ff7b1573-4aff-49dc-919d-8ed2f0274325"",
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
                    ""id"": ""69048359-787e-402c-9774-1fc0505e0d3c"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""989f0cb8-8cfe-4f53-b7ee-47db150be3a4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4cc6a6ab-406d-43cc-b1ce-ac8ce7f246c3"",
                    ""path"": ""<Keyboard>/#(E)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
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
        m_MovePlayer_DUp = m_MovePlayer.FindAction("DUp", throwIfNotFound: true);
        m_MovePlayer_DDown = m_MovePlayer.FindAction("DDown", throwIfNotFound: true);
        m_MovePlayer_DLeft = m_MovePlayer.FindAction("DLeft", throwIfNotFound: true);
        m_MovePlayer_DRight = m_MovePlayer.FindAction("DRight", throwIfNotFound: true);
        m_MovePlayer_Join = m_MovePlayer.FindAction("Join", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_MouseMove = m_Menu.FindAction("MouseMove", throwIfNotFound: true);
        m_Menu_MouseClick = m_Menu.FindAction("MouseClick", throwIfNotFound: true);
        m_Menu_Cancel_Menu = m_Menu.FindAction("Cancel_Menu", throwIfNotFound: true);
        m_Menu_Join = m_Menu.FindAction("Join", throwIfNotFound: true);
        m_Menu_Move = m_Menu.FindAction("Move", throwIfNotFound: true);
        m_Menu_Interact = m_Menu.FindAction("Interact", throwIfNotFound: true);
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
    private readonly InputAction m_MovePlayer_DUp;
    private readonly InputAction m_MovePlayer_DDown;
    private readonly InputAction m_MovePlayer_DLeft;
    private readonly InputAction m_MovePlayer_DRight;
    private readonly InputAction m_MovePlayer_Join;
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
        public InputAction @DUp => m_Wrapper.m_MovePlayer_DUp;
        public InputAction @DDown => m_Wrapper.m_MovePlayer_DDown;
        public InputAction @DLeft => m_Wrapper.m_MovePlayer_DLeft;
        public InputAction @DRight => m_Wrapper.m_MovePlayer_DRight;
        public InputAction @Join => m_Wrapper.m_MovePlayer_Join;
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
                @DUp.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDUp;
                @DUp.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDUp;
                @DUp.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDUp;
                @DDown.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDDown;
                @DDown.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDDown;
                @DDown.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDDown;
                @DLeft.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDLeft;
                @DLeft.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDLeft;
                @DLeft.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDLeft;
                @DRight.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDRight;
                @DRight.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDRight;
                @DRight.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnDRight;
                @Join.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnJoin;
                @Join.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnJoin;
                @Join.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnJoin;
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
                @DUp.started += instance.OnDUp;
                @DUp.performed += instance.OnDUp;
                @DUp.canceled += instance.OnDUp;
                @DDown.started += instance.OnDDown;
                @DDown.performed += instance.OnDDown;
                @DDown.canceled += instance.OnDDown;
                @DLeft.started += instance.OnDLeft;
                @DLeft.performed += instance.OnDLeft;
                @DLeft.canceled += instance.OnDLeft;
                @DRight.started += instance.OnDRight;
                @DRight.performed += instance.OnDRight;
                @DRight.canceled += instance.OnDRight;
                @Join.started += instance.OnJoin;
                @Join.performed += instance.OnJoin;
                @Join.canceled += instance.OnJoin;
            }
        }
    }
    public MovePlayerActions @MovePlayer => new MovePlayerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_MouseMove;
    private readonly InputAction m_Menu_MouseClick;
    private readonly InputAction m_Menu_Cancel_Menu;
    private readonly InputAction m_Menu_Join;
    private readonly InputAction m_Menu_Move;
    private readonly InputAction m_Menu_Interact;
    public struct MenuActions
    {
        private @PlayerControls m_Wrapper;
        public MenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseMove => m_Wrapper.m_Menu_MouseMove;
        public InputAction @MouseClick => m_Wrapper.m_Menu_MouseClick;
        public InputAction @Cancel_Menu => m_Wrapper.m_Menu_Cancel_Menu;
        public InputAction @Join => m_Wrapper.m_Menu_Join;
        public InputAction @Move => m_Wrapper.m_Menu_Move;
        public InputAction @Interact => m_Wrapper.m_Menu_Interact;
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
                @Cancel_Menu.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel_Menu;
                @Cancel_Menu.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel_Menu;
                @Cancel_Menu.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel_Menu;
                @Join.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                @Join.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                @Join.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                @Move.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnInteract;
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
                @Cancel_Menu.started += instance.OnCancel_Menu;
                @Cancel_Menu.performed += instance.OnCancel_Menu;
                @Cancel_Menu.canceled += instance.OnCancel_Menu;
                @Join.started += instance.OnJoin;
                @Join.performed += instance.OnJoin;
                @Join.canceled += instance.OnJoin;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
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
        void OnDUp(InputAction.CallbackContext context);
        void OnDDown(InputAction.CallbackContext context);
        void OnDLeft(InputAction.CallbackContext context);
        void OnDRight(InputAction.CallbackContext context);
        void OnJoin(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnMouseMove(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnCancel_Menu(InputAction.CallbackContext context);
        void OnJoin(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
