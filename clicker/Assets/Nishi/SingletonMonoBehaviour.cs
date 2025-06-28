using UnityEngine;

namespace Utils
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        static private T _instance;
        static readonly private object _lockObject = new object();
        static private bool _isApplicationIsQuitting = false;

        /// <summary>
        /// インスタンス
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
        /// 起動時
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
        /// 破壊時
        /// </summary>
        virtual protected void OnDestroy()
        {
            if (_instance == this)
            {
                _isApplicationIsQuitting = true;
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        virtual protected void Initialize()
        {
            // 処理なし
        }
    }
}