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

        public List<Transform> TryFindGameobject(Vector3 startPosition, float visionLength, int accuracy, string tag)
        {
            List<Transform> findedList = new List<Transform>();
            if (string.IsNullOrEmpty(tag)) return null;
            Vector3 direction, endPoint;
            for (int i = 0; i < accuracy; i++)
            {
                float x = Mathf.Cos(2 * Mathf.PI * i / accuracy) * visionLength + startPosition.x;
                float z = Mathf.Sin(2 * Mathf.PI * i / accuracy) * visionLength + startPosition.z;
                endPoint = new Vector3(x, startPosition.y, z);
                direction = (endPoint - startPosition).normalized;
#if DEBUG_MODE
				Debug.DrawRay(startPosition, direction, Color.green, 0.2f);
#endif
                RaycastHit hit;
				if (Physics.Raycast(startPosition, direction, out hit, visionLength))
                {
                    if (hit.transform.CompareTag(tag))
                    {
#if DEBUG_MODE
                        Debug.Log($"FINDED {hit.transform.gameObject.name}");
#endif
                        findedList.Add(hit.transform);
                    }
                }
            }
            return findedList;
        }

        public List<Transform> TryFindGameobject(Vector3 startPosition, float visionLength, int accuracy, string[] tags)
        {
            List<Transform> findedList = new List<Transform>();
            if (tags.Length == 0) return null;
            Vector3 direction, endPoint;
            foreach (string tag in tags)
            {
                findedList.AddRange(TryFindGameobject(startPosition, visionLength, accuracy, tag));
            }
            return findedList;
        }

        public Transform FindNearest(Vector3 position, List<Transform> transformList)
        {
            if (transformList.Count == 0) return null;
            Transform nearestTransform = null;
            float findedDistance = 100f;
            foreach (Transform _tr in transformList)
            {
                float _d = LengthBetween(position, _tr.position);
                if (findedDistance > _d)
                {
                    findedDistance = _d;
                    nearestTransform = _tr;
                }
            }
            return nearestTransform;
        }

        private float LengthBetween(Vector3 A, Vector3 B)
        {
            return Mathf.Sqrt(((B.x - A.x) * (B.x - A.x)) + ((B.y - A.y) * (B.y - A.y)) + ((B.z - A.z) * (B.z - A.z)));
        }
    }
}