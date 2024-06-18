using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDFixes.Scripts
{
    internal class EmbarkTracker : MonoBehaviour
    {
        public List<Collider> embarkColliders;
        //public Collider collider;

        private void Awake()
        {
            embarkColliders = new List<Collider>();
        }

/*        public void OnTriggerEnter(Collider other)
        {
            //if (!Plugin.stickyFix.Value) return true;
            //Debug.Log(base.gameObject.name + " ENTERING trigger");
            if (other.CompareTag("EmbarkCol") && !embarkColliders.Contains(other))
            {
                //Debug.Log(base.gameObject.name + " ENTERING embark col.");
                embarkColliders.Add(other);

            }
        }

        public void OnTriggerExit(Collider other)
        {
            //if (!Plugin.stickyFix.Value) return;
            //Debug.Log(base.gameObject.name + " EXITING embark col.");
            embarkColliders.Remove(other);
        }*/

    }
}
