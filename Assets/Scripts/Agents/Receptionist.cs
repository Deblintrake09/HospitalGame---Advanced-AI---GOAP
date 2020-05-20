using UnityEngine;

public class Receptionist
    : GAgent {

    new void Start() {

        // Call base Start method
        base.Start();
    
        SubGoal s1 = new SubGoal("receptionAttended", 1, false);
        goals.Add(s1, 1);
        // Resting goal
        SubGoal s2 = new SubGoal("rested", 1, false);
        goals.Add(s2, 3);
        // Toilet  goal
        SubGoal s3 = new SubGoal("emptyBladder", 1, false);
        goals.Add(s3, 5);

        // Call the GetTired() method for the first time
        Invoke(nameof(GetTired), Random.Range(30.0f, 50.0f));
        Invoke(nameof(NeedToPee), Random.Range(60.0f, 80.0f));
    }

    void GetTired() {

        beliefs.ModifyState("exhausted", 1);
        //call the get tired method over and over at random times to make de doctor
        //get tired again
        Invoke(nameof(GetTired), Random.Range(100.0f, 200.0f));
    }
    
    void NeedToPee() {

        beliefs.ModifyState("needToPee", 1);
        //call the get tired method over and over at random times to make de doctor
        //get tired again
        Invoke(nameof(NeedToPee), Random.Range(75, 100.0f));
    }

}