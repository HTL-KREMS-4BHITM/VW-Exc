using System.Collections.Generic;
using UnityEngine;

public class WayPointBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform[] tag_targets;

    public Transform[] GetWayPoints(){
        return tag_targets;
    }
}
