using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class PlatformController : BaseGameObject
    {
        Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Kill()
        {
            rb.isKinematic = false;
        }
    }
}