using UnityEngine;

namespace Utils
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        static private T _instance;
        static readonly private object _lockObject = new object();
        static private bool _isApplicationIsQuitting = false;

        /// <summary>
        /// �C���X�^���X
        /// </summary>
        static public T Instance
        {
            get
            {
                if (_isApplicationIsQuitting)
                {
                    return null;
                }

                lock (_lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));
                        if (_instance == null)
                        {
                            return null;
                        }
                    }
                    return _instance;
                }
            }
        }

        /// <summary>
        /// �N����
        /// </summary>
        virtual protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
                Initialize();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// �j��
        /// </summary>
        virtual protected void OnDestroy()
        {
            if (_instance == this)
            {
                _isApplicationIsQuitting = true;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        virtual protected void Initialize()
        {
            // �����Ȃ�
        }
    }
}