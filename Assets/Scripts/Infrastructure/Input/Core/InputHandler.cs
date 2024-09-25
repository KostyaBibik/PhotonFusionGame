﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class InputActionInfo
{
    public string Name;
    public string[] KeyCodes; 
}

public class InputHandler : MonoBehaviour, IDisposable
{
    [SerializeField] private InputActionAsset inputActions = null;

    private Dictionary<Type, InputValueContextBase> _actions;
    private List<InputActionInfo> _actionsInfo;

    public List<InputActionInfo> GetActionsInfo()
    {
        return _actionsInfo;
    }

    public T GetContext<T>() where T : InputValueContextBase
    {
        return (T)_actions[typeof(T)];
    }

    private void Initialization()
    {
        _actions = new Dictionary<Type, InputValueContextBase>();
        _actionsInfo = new List<InputActionInfo>();
        
        AddNewAction(new MovementContext());
        //AddNewAction(new MouseLookContext());
    }

    private void Awake()
    {
        transform.parent = null;
        Initialization();
    }

    private void AddNewAction(InputValueContextBase context, params InputValueContextBase[] additionalContextes)
    {
        var actionInfo = new InputActionInfo();
        var keyCodes = new List<string>();

        #region Register additional context
        foreach (var additionalContext in additionalContextes)
        {
            var additionalActionName = GetActionName(additionalContext);
            var additionalAction = inputActions.FindAction(additionalActionName, true);
            keyCodes.AddRange(GetKeyCodes(additionalAction));
            RegisterAction(additionalContext, additionalAction);
        }
        #endregion

        #region Register main context
        var actionName = GetActionName(context);
        var action = inputActions.FindAction(actionName, true);
        keyCodes.AddRange(GetKeyCodes(action));
        RegisterAction(context, action);
        #endregion

        actionInfo.Name = actionName;
        actionInfo.KeyCodes = keyCodes.ToArray();

        _actionsInfo.Add(actionInfo);
    }

    private string GetActionName(InputValueContextBase context)
    {
        return context.GetType().Name.Replace("Context", string.Empty);
    }

    private string[] GetKeyCodes(params InputAction[] actions)
    {
        return actions.SelectMany(x => x.bindings.AsEnumerable()
            .Select(x => InputControlPath.ToHumanReadableString(x.path, InputControlPath.HumanReadableStringOptions.OmitDevice))).ToArray();
    }

    private void RegisterAction(InputValueContextBase contextBase, InputAction action)
    {
        _actions.Add(contextBase.GetType(), contextBase);

        action.actionMap.actionTriggered += (context) =>
        {
            if (context.action == action)
            {
                contextBase.SetValue(context);
            }
        };

        if (!action.enabled)
        {
            action.Enable();
        }
    }

    public void Dispose()
    {
        _actions.Clear();
        //Object.Destroy(gameObject);
    }
}