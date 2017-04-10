using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Finish : BaseGameObject
    {
        int numGoldLeft;
        public int NumGoldLeft
        {
            set
            {
                if (value == 0)
                    OpenExit();

                numGoldLeft = value;
            }
            get
            {
                return numGoldLeft;
            }
        }

        MeshRenderer meshRenderer;
        bool isOpen = false;

        private void Awake()
        {
            numGoldLeft = 0;
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            Color c = meshRenderer.sharedMaterial.color;
            c.a = .5f;
            meshRenderer.sharedMaterial.color = c;
        }

        public void OpenExit()
        {
            Color c = meshRenderer.sharedMaterial.color;
            c.a = 1f;
            meshRenderer.sharedMaterial.color = c;
            isOpen = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && isOpen)
            {
                GameManager.instance.NextLevel();
                Destroy(gameObject);
            }
        }
    }
}