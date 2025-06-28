using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
        public Vector2 crossLine;   // 交差場所
        public Vector2 xMinYMin;
        public Vector2 xMinMax;
        public Vector2 xMaxYMin;
        public Vector2 xMaxYMax;
    }

    private List<LineSeg> _straightLineHistory = new List<LineSeg>();
    private StoragePoints _Points = new StoragePoints();      // 必要なポイントの情報を格納
    private int _curHistoryCnt = 0;                 // マウスの保存フレーム / 前のフレームを取得しやすくする為に、TimeではなくCountで行う
    private int _straightLineStreakCnt = 0;         // 直線が連続した場合のカウント
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
            _curHistoryCnt = 0;
            _straightLineHistory.Clear();
            _prevCursorPos = Vector2.zero;
            _curCursorPos = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _curCursorPos = Input.mousePosition;

            if (_straightLineHistory.Count > _deleteHistoryFrame)
            {
                _straightLineHistory.RemoveAt(0);
            }

            if (_curHistoryCnt >= 1)
            {
                _straightLineHistory.Add(new LineSeg(_curHistoryCnt, _prevCursorPos, _curCursorPos));
            }

            CheckAllIntersections();

            // 点の確認用ーーーーーーーーーーー
            //Debug.Log(_cursorPosHistory.Count);
            //Debug.Log(string.Join(", ", _cursorPosHistory));
            // 線の確認用ーーーーーーーーーーー
            Color lineColor = Color.red;
            // 直線をすべて描画
            foreach (LineSeg seg in _straightLineHistory)
            {
                Vector3 p0 = new Vector3(seg.start.x, seg.start.y, 0f);
                Vector3 p1 = new Vector3(seg.end.x, seg.end.y, 0f);

                // 第３引数が0なら線は無限に残る
                Debug.DrawLine(p0, p1, lineColor, 0f, false);
            }

            // 最後に
            _curHistoryCnt++;
            _prevCursorPos = _curCursorPos;
        }
    }

    // 線と線の当たり判定
    public bool IsLineSegmentIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
    {
        // ベクトル
        Vector2 r = p2 - p1;
        Vector2 s = q2 - q1;

        float rxs = r.x * s.y - r.y * s.x;       // r × s
        if (Mathf.Approximately(rxs, 0f)) return false;   // 平行 or 同一直線 → 交点なし

        Vector2 qp = q1 - p1;
        float t = (qp.x * s.y - qp.y * s.x) / rxs;
        float u = (qp.x * r.y - qp.y * r.x) / rxs;

        // 端点を除き 0 と 1 は含めない（純粋交差）
        const float Eps = 1e-6f;
        if (t <= Eps || t >= 1f - Eps) return false;
        if (u <= Eps || u >= 1f - Eps) return false;

        _Points.crossLine = p1 + t * r;   // 交点
        return true;
    }

    private bool CheckAllIntersections()
    {
        int maxCount = _straightLineHistory.Count;

        for (int firstLine = 0; firstLine < maxCount - 1; firstLine++)
        {
            var l1 = _straightLineHistory[firstLine];
            for (int nextLine = firstLine + 1; nextLine < maxCount; nextLine++)
            {
                var l2 = _straightLineHistory[nextLine];

                if (IsLineSegmentIntersect(l1.start, l1.end, l2.start, l2.end))
                {
                    // ヒット時の処理
                    Debug.Log($"Hit! #{l1.storageCount} と #{l2.storageCount}");
                    Debug.Log("交点：" + _Points.crossLine);
                    return true;
                }
            }
        }

        return false;
    }
}
