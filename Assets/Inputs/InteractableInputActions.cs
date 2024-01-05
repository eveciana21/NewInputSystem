//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Inputs/InteractableInputActions.inputactions
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

public partial class @InteractableInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InteractableInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InteractableInputActions"",
    ""maps"": [
        {
            ""name"": ""InteractableZone"",
            ""id"": ""77feae58-24d2-4be8-8987-6d8bb74be414"",
            ""actions"": [
                {
                    ""name"": ""Press E"",
                    ""type"": ""Button"",
                    ""id"": ""fa543229-ed49-4c5b-a02f-45469325e29d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""32d4095c-ed5c-4729-8072-5dac10d6759c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InteractableZone
        m_InteractableZone = asset.FindActionMap("InteractableZone", throwIfNotFound: true);
        m_InteractableZone_PressE = m_InteractableZone.FindAction("Press E", throwIfNotFound: true);
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

    // InteractableZone
    private readonly InputActionMap m_InteractableZone;
    private List<IInteractableZoneActions> m_InteractableZoneActionsCallbackInterfaces = new List<IInteractableZoneActions>();
    private readonly InputAction m_InteractableZone_PressE;
    public struct InteractableZoneActions
    {
        private @InteractableInputActions m_Wrapper;
        public InteractableZoneActions(@InteractableInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @PressE => m_Wrapper.m_InteractableZone_PressE;
        public InputActionMap Get() { return m_Wrapper.m_InteractableZone; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractableZoneActions set) { return set.Get(); }
        public void AddCallbacks(IInteractableZoneActions instance)
        {
            if (instance == null || m_Wrapper.m_InteractableZoneActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InteractableZoneActionsCallbackInterfaces.Add(instance);
            @PressE.started += instance.OnPressE;
            @PressE.performed += instance.OnPressE;
            @PressE.canceled += instance.OnPressE;
        }

        private void UnregisterCallbacks(IInteractableZoneActions instance)
        {
            @PressE.started -= instance.OnPressE;
            @PressE.performed -= instance.OnPressE;
            @PressE.canceled -= instance.OnPressE;
        }

        public void RemoveCallbacks(IInteractableZoneActions instance)
        {
            if (m_Wrapper.m_InteractableZoneActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInteractableZoneActions instance)
        {
            foreach (var item in m_Wrapper.m_InteractableZoneActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InteractableZoneActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InteractableZoneActions @InteractableZone => new InteractableZoneActions(this);
    public interface IInteractableZoneActions
    {
        void OnPressE(InputAction.CallbackContext context);
    }
}