using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker: AbstractKinematic
{
    BlendedSteering flocking;
    Arrive arrive;
    LookWhereGoing look;
    Seperation seperate;
    SteeringOutput blendedSteeringType;
    // Start is called before the first frame update
    void Start()
    {
        arrive = new Arrive();
        look = new LookWhereGoing();
        seperate = new Seperation();
        flocking = new BlendedSteering();
        arrive.target = target;
        arrive.ai = this;
        look.target = target;
        look.ai = this;
        seperate.ai = this;
        AbstractKinematic[] kBirds;
        GameObject[] birds = GameObject.FindGameObjectsWithTag("Bird");
        kBirds = new AbstractKinematic[birds.Length - 1];
        int j = 0;
        for (int i = 0; i < birds.Length - 1; i++) {
            if (birds[i] == this)
               continue;
            kBirds[j++] = birds[i].GetComponent<AbstractKinematic>();
        }
        Debug.Log(kBirds);
        seperate.targets = kBirds;
        flocking.behaviors = new BehaviorAndWeight[3];
        BehaviorAndWeight behavior1 = new BehaviorAndWeight();
        behavior1.behavior = arrive;
        behavior1.weight = 1f;
        flocking.behaviors[0] = behavior1;
        BehaviorAndWeight behavior2 = new BehaviorAndWeight();
        behavior2.behavior = look;
        behavior2.weight = 1f;
        flocking.behaviors[1] = behavior2;
        BehaviorAndWeight behavior3 = new BehaviorAndWeight();
        behavior3.behavior = seperate;
        behavior3.weight = 0.1f;
        flocking.behaviors[2] = behavior3;
    }

    // Update is called once per frame
    public override void Update()
    {
        mySteering = flocking.GetSteering();
        base.Update();
    }
}
