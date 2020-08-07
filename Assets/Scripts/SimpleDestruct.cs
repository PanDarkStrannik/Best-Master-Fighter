using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestruct : MonoBehaviour
{
    [SerializeField] DamagebleParamSum paramSum;
    [SerializeField] List<DamagebleParam.ParamType> paramTypes;
    [SerializeField] List<Rigidbody> objectsForForce;
    [SerializeField] List<GameObject> objectsForDeactive;
    [SerializeField] float timeToDeactive=1f;
    [SerializeField] float explousionForce = 1f;
    [SerializeField] float explousionRadius = 1f;
    [SerializeField] Transform forceTransform;



    private void OnEnable()
    {
        paramSum.Initialize();

        paramSum.ParamNull += CheckType;
    }

    private void OnDisable()
    {
        paramSum.Unsubscribe();
    }

    private void CheckType(DamagebleParam.ParamType type)
    {
        if (paramTypes.Count > 0)
        {
            foreach (var myType in paramTypes)
            {
                if (type == myType)
                {
                    foreach (var obj in objectsForForce)
                    {
                        obj.isKinematic = false;
                        obj.AddExplosionForce(explousionForce, forceTransform.position, explousionRadius);
                    }

                    Invoke("ToDeactive", timeToDeactive);
                    break;
                }
            }
        }
    }

    private void ToDeactive()
    {
        foreach(var obj in objectsForDeactive)
        {
            obj.SetActive(false);
        }
    }

}
