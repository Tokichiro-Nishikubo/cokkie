using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.HableCurve;

[RequireComponent(typeof(LineRenderer))]

// カーソルの場所取得は60PFS固定で行う
public class InfinitMouseRotate : MonoBehaviour
{
    struct LineSeg
    {
        public int storageCount;
        public Vector2 start;
        public Vector2 end;
        public LineSeg(int curCount,Vector2 s, Vector2 e) 
        {
            storageCount = curCount;
            start = s; 
            end = e; 
        }
    }

    // 毎フレームごとに比較して４点の数値を格納
    struct StoragePoints
    {
        public Vector2 crossPoint;   // 交差場所
        public Vector2 xMinYMin;
        public Vector2 xMinYMax;
        public Vector2 xMaxYMin;
        public Vector2 xMaxYMax;
    }

    private List<LineSeg> _straightLineHistory = new List<LineSeg>();
    private StoragePoints _Points = new StoragePoints();      // 必要なポイントの情報を格納
    private int _curHistoryCnt = 0;                 // マウスの保存フレーム / 前のフレームを取得しやすくする為に、TimeではなくCountで行う
    private int _straightLineStreakCnt = 0;         // 直線が連続した場合のカウント / 最適化用
    private Vector2 _curCursorPos = new Vector2();  // 現在のカーソル座標 / 直線判定用
    private Vector2 _prevCursorPos = new Vector2(); // 前フレームのカーソル座標 / 直線判定用

    [Tooltip("保存したカーソル座標情報の削除フレーム")]
    [SerializeField] private int _deleteHistoryFrame = 300;
    [Tooltip("描画用に線を可視化するRenderer")]
    [SerializeField] private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Fixedだとクリックを離した判定が取れない可能性があるのでここで
        if (Input.GetMouseButtonUp(0))
        {
            ResetInfinitData();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _curCursorPos = Input.mousePosition;
            // マウスカーソル取得の後に呼び出し
            StoragekMinMaxPoint();

            if (_straightLineHistory.Count > _deleteHistoryFrame)
            {
                _straightLineHistory.RemoveAt(0);
            }

            if (_curHistoryCnt >= 1)
            {
                _straightLineHistory.Add(new LineSeg(_curHistoryCnt, _prevCursorPos, _curCursorPos));
            }
            else// 初回座標登録
            {
                _Points.xMinYMin = _curCursorPos;
                _Points.xMinYMax = _curCursorPos;
                _Points.xMaxYMin = _curCursorPos;
                _Points.xMaxYMax = _curCursorPos;
            }

            // _straightLineHistory追加後に初期化
            _curHistoryCnt++;
            _prevCursorPos = _curCursorPos;

            // _curHistoryCnt++の後に配置
            CheckAllIntersections();
        }
    }

    // 線と線の当たり判定
    public bool IsLineSegmentIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
    {
        // ベクトル
        Vector2 r = p2 - p1;
        Vector2 s = q2 - q1;

        float rxs = r.x * s.y - r.y * s.x;
        // 同じような方向の場合は、交差していない判定を行う
        if (Mathf.Approximately(rxs, 0f)) return false;  

        Vector2 qp = q1 - p1;
        float t = (qp.x * s.y - qp.y * s.x) / rxs;
        float u = (qp.x * r.y - qp.y * r.x) / rxs;

        // 端点を除き 0 と 1 は含めない（純粋交差）
        const float Eps = 1e-6f;
        if (t <= Eps || t >= 1f - Eps) return false;
        if (u <= Eps || u >= 1f - Eps) return false;

        _Points.crossPoint = p1 + t * r;   // 交点
        // Debug.Log("交差");

        return true;
    }
    // 全ての線を捜査してヒット判定を取得する
    private bool CheckAllIntersections()
    {
        int maxCount = _straightLineHistory.Count;

        // ∞が完成したタイミングで、要素をすべて削除した時のエラー対処用
        if(maxCount <= 2) return false;

        for (int firstLine = 0; firstLine < maxCount - 1; firstLine++)
        {
            var l1 = _straightLineHistory[firstLine];
            for (int nextLine = firstLine + 1; nextLine < maxCount; nextLine++)
            {
                var l2 = _straightLineHistory[nextLine];

                if (IsLineSegmentIntersect(l1.start, l1.end, l2.start, l2.end))
                {
                    // ヒット時の処理
                    //Debug.Log($"Hit! #{l1.storageCount} と #{l2.storageCount}");
                    if(CheckInfinit())
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
    // ポイントの最小最大を格納する
    private void StoragekMinMaxPoint()
    {
        // 左下
        if(_curCursorPos.x <= _Points.xMinYMin.x && _curCursorPos.y <= _Points.xMinYMin.y)
        {
            _Points.xMinYMin = _curCursorPos;
            // Debug.Log("左下格納");
        }
        // 左上
        if(_curCursorPos.x <= _Points.xMinYMax.x && _curCursorPos.y >= _Points.xMinYMax.y)
        {
            _Points.xMinYMax = _curCursorPos;
            // Debug.Log("左上格納");
        }
        // 右下
        if (_curCursorPos.x >= _Points.xMaxYMin.x && _curCursorPos.y <= _Points.xMaxYMin.y)
        {
            _Points.xMaxYMin = _curCursorPos;
            // Debug.Log("右下格納");
        }
        // 右上
        if (_curCursorPos.x >= _Points.xMaxYMax.x && _curCursorPos.y >= _Points.xMaxYMax.y)
        {
            _Points.xMaxYMax = _curCursorPos;
            // Debug.Log("右上格納");
        }
    }

    // 無限が完成しているかどうか / StragePointに４点が格納されていて、crossPointとの判定をクリアするとtrue
    private bool CheckInfinit()
    {
        Vector2 crossPoint = _Points.crossPoint;
        //Debug.Log(crossPoint);
        // 左下
        if (crossPoint.x <= _Points.xMinYMin.x || crossPoint.y <= _Points.xMinYMin.y)
        {
            //Debug.Log("左下" + _Points.xMinYMin);
            return false;
        }
        // 左上
        if (crossPoint.x <= _Points.xMinYMax.x || crossPoint.y >= _Points.xMinYMax.y)
        {
            //Debug.Log("左上" + _Points.xMinYMax);
            return false;
        }
        // 右下
        if (crossPoint.x >= _Points.xMaxYMin.x || crossPoint.y <= _Points.xMaxYMin.y)
        {
            //Debug.Log("右上" + _Points.xMaxYMin);
            return false;
        }
        // 右上
        if (crossPoint.x >= _Points.xMaxYMax.x || crossPoint.y >= _Points.xMaxYMax.y)
        {
            //Debug.Log("右下" + _Points.xMaxYMax);
            return false;
        }
        //Debug.Log("左下" + _Points.xMinYMin);
        //Debug.Log("左上" + _Points.xMinYMax);
        //Debug.Log("右上" + _Points.xMaxYMin);
        //Debug.Log("右下" + _Points.xMaxYMax);

        //Debug.Log("交点：" + _Points.crossPoint);
        // Debug.Log("インフィニティ！");
        ResetInfinitData();
        return true;
    }

    private void ResetInfinitData()
    {
        _curHistoryCnt = 0;
        _straightLineHistory.Clear();
        _prevCursorPos = Vector2.zero;
        _curCursorPos = Vector2.zero;
        _Points = default;
    }
}
