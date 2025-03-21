using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;

namespace GuyBad
{
    public class GuyBadPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Pos = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Move();
            }
        }

        public void Move()
        {
            SubmitPositionRequestRpc();
        }

        [ServerRpc]
        void SubmitPositionRequestRpc()
        {
            var randPos = GetRandomPositionOnPlane();
            transform.position = randPos;
            Pos.Value = randPos;
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        void Update()
        {
            transform.position = Pos.Value;
        }

    }
}


