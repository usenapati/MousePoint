using UnityEngine;

namespace Environment
{
    public class RoomTemplates : MonoBehaviour
    {
        public GameObject[] topRooms;
        public GameObject[] rightRooms;
        public GameObject[] bottomRooms;
        public GameObject[] leftRooms;
        public GameObject closedRoom;

        [SerializeField] public int roomLimit;
        private int _roomCounter = 1;

        public bool AddRoom()
        {
            if (_roomCounter <= roomLimit)
            {
                _roomCounter++; 
                return true;
            }

            return false;
        }
    }
}
