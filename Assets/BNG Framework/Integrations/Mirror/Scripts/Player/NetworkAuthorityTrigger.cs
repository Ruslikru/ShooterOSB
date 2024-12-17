using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BNG {
    public class NetworkAuthorityTrigger : NetworkBehaviour {
        void OnTriggerEnter(Collider other) {
            //if (!isOwned) {
            //    return;
            //}
            Debug.Log("Trigger enter for " + other.transform.name + " " + isOwned);
            Rigidbody colRb = other.transform.root.GetComponentInChildren<Rigidbody>();

            // if there isn't a rigidbody, then it isn't a physics object, no need to continue
            if (colRb == null) {
                return;
            }

            NetworkIdentity netId = other.gameObject.GetComponent<NetworkIdentity>();
            List<NetworkGrabbable> netGrabs = new();
            netGrabs.AddRange(other.transform.root.GetComponentsInChildren<NetworkGrabbable>());

            // check if the object is a network grabbable and if its being held, if it is, then do nothing
            // do not take authority over an object if another player is holding it
            if (netGrabs.Count > 0) {
                // check if the object is being held, if it is, do nothing
                if (netGrabs[0].holdingStatus == true) {
                    return;
                }
            }

            // if its not being held and we hit it take ownership so it can be moved on collision
            if (netId != null && netId.isOwned == false) {
                Debug.Log(other.transform.name);
                Debug.Log("Request ownership for " + other.transform.name);

                CmdRequestOwnership(netId);
            }
        }

        [Command(requiresAuthority = false)]
        void CmdRequestOwnership(NetworkIdentity targetNetId) {
            Rigidbody rb = targetNetId.GetComponentInChildren<Rigidbody>();

            if (rb) {
                ResetInteractableVelocity(rb);
            }

            // Check if the object is not owned by anyone or the server, then assign authority
            if (targetNetId != null && targetNetId.isServer) {
                targetNetId.RemoveClientAuthority();
                targetNetId.AssignClientAuthority(connectionToClient);
            }

            // remove the client authority once the object has stopped moving
            // StartCoroutine(RemoveClientAuthority(rb, targetNetId));
        }

        public virtual void ResetInteractableVelocity(Rigidbody rb) {
            // Without this you may notice some pickups rapidly fall through the floor
            if (rb != null) {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}

