using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapScanner
{
    public class SearchForGameobject
    {
        public SearchForGameobject()
        {

        }

        void TryFindGameobject(string tag, Vector3 startPosition, float visionLength)
        {
            if (string.IsNullOrEmpty(tag)) return;
            List<Transform> findedList = new List<Transform>();
            Vector3 direction, endPoint;
            endPoint = new Vector3(0, 0.5f, 0);
            for (int i = 0; i < 20; i++)
            {
                endPoint = new Vector3(1, 0, 1);
                direction = (endPoint - startPosition).normalized;
#if DEBUG_MODE
                Debug.DrawRay(startPosition, direction, Color.green, 0.1f);
#endif
                RaycastHit hit;
                if (Physics.Raycast(startPosition, direction, out hit, visionLength))
                {
                    if (hit.transform.CompareTag(tag))
                    {
                        findedList.Add(hit.transform);
                    }
                }
            }
        }
    }
}