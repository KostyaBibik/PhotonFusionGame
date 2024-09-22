using System;
using Infrastructure.Input.Core;

public class ButtonInputContext : InputValueContext<float>
{
    /// <summary>
    ///     Указывает зажата ли кнопка в текущий момент времени
    /// </summary>
    public bool IsHold => Value >= 0.5F;

    public void RegisterButtonPressCallback(Action callback, EGameSystemType type)
    {
        RegisterValueChangingCallback(() =>
            {
                if (IsHold)
                    callback?.Invoke();
            },
            type);
    }

    public void RegisterButtonReleaseCallback(Action callback, EGameSystemType type)
    {
        RegisterValueChangingCallback(() =>
            {
                if (!IsHold) 
                    callback?.Invoke();
            },
            type);
    }
}