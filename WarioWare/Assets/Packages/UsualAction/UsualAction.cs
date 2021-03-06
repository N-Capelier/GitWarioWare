﻿using System;
using UnityEngine;


namespace SD_UsualAction
{

    

    public delegate void ActionRef<T>(ref T item);

    public static class ActionExtention
    {
       
        public static T Next<T>(this T src) where T : struct
        {
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;

            return (Arr.Length == j) ? Arr[Arr.Length-1] : Arr[j];
        }
        public class ActionWithDefault
        {
            Action defaultAction;
            Action currentAction;

            public void Do()
            {
                if (defaultAction == null)
                {
                    Debug.LogError("You have forgotted to attribute the default action.");
                    return;
                }
                if (currentAction == null)
                {
                    currentAction = defaultAction;
                }

                currentAction.Invoke();
                Reset();
            }
            public void Reset()
            {
                currentAction = defaultAction;
            }
            public void Set(Action action)
            {
                currentAction = action;
            }
            public ActionWithDefault(params Action[] defaultActions)
            {
                foreach (var action in defaultActions)
                {
                    this.defaultAction += action;
                }
            }
        }

        public class ActionWithDefault<T>
        {
            Action<T> defaultAction;
            Action<T> currentAction;

            public void Do(T param)
            {
                PersistantDo(param);
                Reset();
            }

            public void PersistantDo(T param)
            {
                if (defaultAction == null)
                {
                    Debug.LogError("You have forgotted to attribute the default action");
                    return;
                }
                if (currentAction == null)
                {
                    currentAction = defaultAction;
                }

                currentAction.Invoke(param);
            }

            public void Reset()
            {
                currentAction = defaultAction;
            }
            public void Set(Action<T> action)
            {
                currentAction = action;
            }

            public void Initialize(params Action<T>[] defaultActions)
            {
                foreach (var action in defaultActions)
                {
                    this.defaultAction += action;
                }
            }
        }

        public static void TryDo(this Action action)
        {
            if (action != null)
            {
                action.Invoke();
            }

        }

    }

}
public static class AudioSourceExtension
{
    public static AudioSource PlaySecured(this AudioSource audioSource)
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
            return audioSource;
        }
        else
        {
            Debug.LogError("Your audio Source doesn't have any clip, please use ApplyAudioClip of the sound Manager");
            return null;
        }
    }
    
}