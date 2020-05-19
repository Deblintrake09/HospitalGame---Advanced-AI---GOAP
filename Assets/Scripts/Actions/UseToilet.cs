using UnityEngine;

public class UseToilet : GAction {
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetResourceQueue("Toilets").RemoveResourse();
        GWorld.Instance.GetWorld().ModifyState("freeToilet",-1);
        if (target == null) return false;
        inventory.AddItem(target);
        return true;
    }

    public override bool PostPerform() {

        //the agent will no longer believe they need a rest
        beliefs.RemoveState("needToPee");
        GWorld.Instance.GetResourceQueue("Toilets").AddResourse(target);
        GWorld.Instance.GetWorld().ModifyState("freeToilet",1);
        inventory.RemoveItem(target);
        Debug.Log("Peed! Yay!");
        return true;
    }
}
