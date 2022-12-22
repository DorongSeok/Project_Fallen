using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterimageParticleContoller : MonoBehaviour
{
    public ParticleSystem parSys;

    private void FixedUpdate()
    {
        SyncParticles();
    }
    void SyncParticles()
    {
        var main = parSys.main;
        main.startRotationZ = transform.rotation.eulerAngles.z * -Mathf.Deg2Rad;
    }
}
