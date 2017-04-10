using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class BaseGameObject : MonoBehaviour
    {
        public void WaitForSeconds(Action action, float time)
        {
            StartCoroutine(CoWaitForSeconds(action, time));
        }

        private IEnumerator CoWaitForSeconds(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}
