using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attendReception : GAction
{

    public override bool PrePerform()
    {
        GWorld.Instance.GetWorld().ModifyState("receptionAttended",1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("receptionAttended",-1);
        return true; 
    }
}
