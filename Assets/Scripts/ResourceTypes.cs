using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceTypes 
{
    Untagged,Patient,Toilet,Cubicle,Office,Puddle
}
public enum ResourceQueueEnum 
{
    Patients,Toilets,Cubicles,Offices,Puddles
}

public enum ResourceStates 
{
    nullState,freeToilet,freeCubicle,freeOffice,puddles
}
