using System;
using UnityEngine;

namespace Environment
{
    public class RespawnTrigger : MonoBehaviour
    {
        private BoxCollider _boxCollider;
        public Vector3 spawnPosition { get; set; }
        [SerializeField] private int spawnOrder;

        // Start is called before the first frame update
        private void Start()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        private void OnCollisionEnter(Collision other)
        {
            throw new NotImplementedException();
            // Check if player collides with box collider, if so update respawn manager.
            // Manager should track spawn order (Should switch last respawn point if order is higher value)
        }
    }
}
