using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectsGenerator
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        bool InitObjectsDisabled = false;
        [SerializeField]
        GameObject baseObject;

        [SerializeField]
        GameObject[] pool;

        private bool isInited = false;

        // Start is called before the first frame update
        void Awake()
        {

        }

        private void InitObjects()
        {
            Vector3 vectorForInstantiate = new Vector3(0f, 0.7f, 0f);
            for (int i = 0; i < pool.Length; i++)
            {
                if (pool[i] == null)
                {
                    //TODO: Use factory fot this!
                    pool[i] = Instantiate(baseObject, vectorForInstantiate, baseObject.transform.rotation);
                    if (InitObjectsDisabled)
                    {
                        pool[i].SetActive(false);
                    }
                }
            }
            isInited = true;
        }

        public GameObject GetObject()
        {
            if (!isInited) InitObjects();
            foreach (GameObject gObj in pool)
            {
                if (!gObj.activeInHierarchy)
                {
                    return gObj;
                }
            }
            return null;
        }

        public int size
        {
            get
            {
                return pool.Length;
            }
        }

        void OnEnable()
        {
            if (!isInited)
                InitObjects();
        }
    }
}