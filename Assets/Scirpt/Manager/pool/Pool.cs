
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Pool
{
    public GameObject Prefab => prefab;
    public int Size => size;


    public int RuntimeSize => queue.Count;

    [SerializeField] GameObject prefab;
    [SerializeField] int size = 10;

    public Queue<GameObject> queue;

    Transform parent;


    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        this.parent = parent;
        for (var i = 0; i < size; i++)
        {
            var go = Copy();
            queue.Enqueue(go);
            go.name += i;

        }
    }

    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab, parent);
        copy.name = copy.name;
        copy.SetActive(false);

        return copy;
    }

    GameObject AvailableObject()
    {
        GameObject availableObject = null;

        if (queue.Count > 0 && !queue.Peek().activeSelf)
        {
            availableObject = queue.Dequeue();


        }
        else
        {

            var go = Copy();
            availableObject = go;
            go.name += size;
            size++;

#if UNITY_EDITOR
            Debug.LogWarning(go.name); //用于检查哪一种对象池类型数目不够需要在运行的时候生成
#endif
        }

        queue.Enqueue(availableObject);
        return availableObject;
    }

    public GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();


        preparedObject.SetActive(true);
        preparedObject.transform.position = position;

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position, Quaternion rotation)
    {
        GameObject preparedObject = AvailableObject();


        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        GameObject preparedObject = AvailableObject();


        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;

        return preparedObject;
    }

    /// <summary>
    /// GameObject: 需要入队的类型   isSetActive：是否需要设置不可见 默认开启 (当使用特效对象池的时候可以直接在检查器中设置当粒子生命周期结束的时候设置粒子状态)
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="isSetActive"></param>
    public void PoolRecycle(GameObject gameObject, bool isSetActive = true)
    {

        if (isSetActive == true)
        {
            gameObject.SetActive(false);
        }
        queue.Enqueue(gameObject);
    }
}