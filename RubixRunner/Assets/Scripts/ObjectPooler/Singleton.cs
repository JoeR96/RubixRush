using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private bool _onDestroy = false;

    static T m_instance;

    public static T instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<T>();

                if(m_instance == null)
                {
                    GameObject singleton = new GameObject((typeof(T).Name));
                    m_instance = singleton.AddComponent<T>();
                }
            }
            return m_instance;
        }
    }
    
    public virtual void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this as T;
                
            if(_onDestroy)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
