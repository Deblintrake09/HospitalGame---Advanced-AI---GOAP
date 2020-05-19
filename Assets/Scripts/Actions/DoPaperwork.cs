using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoPaperwork : GAction
{

    public override bool PrePerform()
    {
        //obtenemos una oficina
        target = GWorld.Instance.GetResourceQueue("Offices").RemoveResourse();
        GWorld.Instance.GetWorld().ModifyState("freeOffice", -1);
        //si no había oficina, falla
        if (target == null) return false;
        //como habia oficina libre, la agregamos al inventario
        inventory.AddItem(target);
        return true;
    }

    public override bool PostPerform()
    {
        //devuelve la oficina al mundo
        GWorld.Instance.GetResourceQueue("Offices").AddResourse(target);
        GWorld.Instance.GetWorld().ModifyState("freeOffice", 1);
        //la remueve del inventario
        inventory.RemoveItem(target);
        return true; 
    }
}
