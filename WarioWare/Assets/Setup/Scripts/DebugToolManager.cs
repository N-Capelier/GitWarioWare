using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugToolManager : Singleton<DebugToolManager>
{
    [SerializeField] private DebugingTool debugingTool;
    private void Awake()
    {
        CreateSingleton(true);
    }

    public int  ChangeVariableValue(object variable) 
    {
        bool canAttriute = false;
        int _currentVariable = 0;
        for (int i = 0; i < debugingTool.names.Count; i++)
        {
            if(debugingTool.names[i] == variable.ToString())
            {
                _currentVariable = debugingTool.values[i];
                canAttriute = true;
                break;
            }
        }
        if(canAttriute)
            return _currentVariable;
        else
        {
            Debug.LogError("The variable " +variable.ToString() +" is not referenced or not well written");
            return 0;
        }    
    }
}
