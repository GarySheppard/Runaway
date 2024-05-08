using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBreak : MonoBehaviour
{
    public GameObject brokenAlt;

    /* Destroys object and replaces itself with a broken copy */
    public GameObject Break()
    {
        Destroy(gameObject);
        return Instantiate(brokenAlt, transform.position, transform.rotation);
    }
}
