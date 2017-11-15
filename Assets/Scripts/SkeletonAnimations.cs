using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimations : MonoBehaviour {

    public SkeletonBehaviour skeleton;

    public void ArmOn()
    {
        skeleton.ArmOn();
    }

	public void Hit()
    {
        skeleton.Hit();
    }
}
