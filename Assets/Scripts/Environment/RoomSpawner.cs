using System;
using JetBrains.Annotations;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

namespace Environment
{
   public class RoomSpawner : MonoBehaviour
   {
      private enum OpeningDirection
      {
         Top = 0, Right, Bottom, Left
      }
      [SerializeField] private OpeningDirection openingDirection;

      private RoomTemplates _roomTemplates;

      [SerializeField] private bool spawned;

      private void Awake()
      {
         _roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
         Invoke(nameof(GenerateNextRoom), .1f);
      }

      private void GenerateNextRoom()
      {
         if (!spawned && _roomTemplates.AddRoom())
         {
            int rand;
            switch (openingDirection)
            {
               case (OpeningDirection.Top):
                  rand = Random.Range(0, _roomTemplates.topRooms.Length);
                  Instantiate(_roomTemplates.topRooms[rand], transform.position, Quaternion.identity);
                  break;
               case (OpeningDirection.Right):
                  rand = Random.Range(0, _roomTemplates.rightRooms.Length);
                  Instantiate(_roomTemplates.rightRooms[rand], transform.position, Quaternion.identity);
                  break;
               case (OpeningDirection.Bottom):
                  rand = Random.Range(0, _roomTemplates.bottomRooms.Length);
                  Instantiate(_roomTemplates.bottomRooms[rand], transform.position, Quaternion.identity);
                  break;
               case (OpeningDirection.Left):
                  rand = Random.Range(0, _roomTemplates.leftRooms.Length);
                  Instantiate(_roomTemplates.leftRooms[rand], transform.position, Quaternion.identity);
                  break;
            }
            spawned = true;
         }
         else if ((!spawned && !_roomTemplates.AddRoom()))
         {
            Instantiate(_roomTemplates.closedRoom, transform.position, Quaternion.identity);
         }
         
      }

      private void OnTriggerEnter([NotNull] Collider other)
      {
         if (!other.CompareTag("RoomSpawner")) return;
         try
         {
            if (other != null && spawned == false && other.GetComponent<RoomSpawner>().spawned == false)
            {
               Instantiate(_roomTemplates.closedRoom, transform.position, Quaternion.identity);
               Destroy(gameObject);
            }
         }
         catch (Exception e)
         {
            Debug.WriteLine(e);
         }
         spawned = true;
      }
   }
   
}
