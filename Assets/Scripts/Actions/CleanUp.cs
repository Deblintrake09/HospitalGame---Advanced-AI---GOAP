public class CleanUp : GAction {
    public override bool PrePerform() {

        target = GWorld.Instance.GetResourceQueue("Puddles").RemoveResourse();
        if (target == null) return false;
        return true;
    }

    public override bool PostPerform() 
    {
        GWorld.Instance.GetWorld().ModifyState("puddles", -1);
        Destroy(target.gameObject);
        return true;
    }
}
