using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

[RequireComponent(typeof(LineRenderer))]

// �J�[�\���̏ꏊ�擾��60PFS�Œ�ōs��
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

    // ���t���[�����Ƃɔ�r���ĂS�_�̐��l���i�[
    struct StoragePoints
    {
        public Vector2 crossLine;   // �����ꏊ
        public Vector2 xMinYMin;
        public Vector2 xMinMax;
        public Vector2 xMaxYMin;
        public Vector2 xMaxYMax;
    }

    private List<Vector2> _cursorPosHistory = new List<Vector2>();
    private List<LineSeg> _straightLineHistory = new List<LineSeg>();
    private StoragePoints _Points = new StoragePoints();      // �K�v�ȃ|�C���g�̏����i�[
    private int _curHistoryCnt = 0;                 // �}�E�X�̕ۑ��t���[�� / �O�̃t���[�����擾���₷������ׂɁATime�ł͂Ȃ�Count�ōs��
    private int _straightLineStreakCnt = 0;         // �������A�������ꍇ�̃J�E���g
    private Vector2 _curCursorPos = new Vector2();  // ���݂̃J�[�\�����W / ��������p
    private Vector2 _prevCursorPos = new Vector2(); // �O�t���[���̃J�[�\�����W / ��������p

    [Tooltip("�ۑ������J�[�\�����W���̍폜�t���[��")]
    [SerializeField] private int _deleteHistoryFrame = 300;
    [Tooltip("�`��p�ɐ�����������Renderer")]
    [SerializeField] private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Fixed���ƃN���b�N�𗣂������肪���Ȃ��\��������̂ł�����
        if (Input.GetMouseButtonUp(0))
        {
            _curHistoryCnt = 0;
            _cursorPosHistory.Clear();
            _prevCursorPos = Vector2.zero;
            _curCursorPos = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _curCursorPos = Input.mousePosition;
            _cursorPosHistory.Add(_curCursorPos);

            if (_cursorPosHistory.Count > _deleteHistoryFrame)
            {
                _cursorPosHistory.RemoveAt(0);
            }

            if (_curHistoryCnt >= 1)
            {
                _straightLineHistory.Add(new LineSeg(_curHistoryCnt, _prevCursorPos, _curCursorPos));
            }

            CheckAllIntersections();

            // �_�̊m�F�p�[�[�[�[�[�[�[�[�[�[�[
            //Debug.Log(_cursorPosHistory.Count);
            //Debug.Log(string.Join(", ", _cursorPosHistory));
            // ���̊m�F�p�[�[�[�[�[�[�[�[�[�[�[
            Color lineColor = Color.red;
            // ���������ׂĕ`��
            foreach (LineSeg seg in _straightLineHistory)
            {
                Vector3 p0 = new Vector3(seg.start.x, seg.start.y, 0f);
                Vector3 p1 = new Vector3(seg.end.x, seg.end.y, 0f);

                // ��R������0�Ȃ���͖����Ɏc��
                Debug.DrawLine(p0, p1, lineColor, 0f, false);
            }

            // �Ō��
            _curHistoryCnt++;
            _prevCursorPos = _curCursorPos;
        }
    }

    // ���Ɛ��̓����蔻��
    public bool IsLineSegmentIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
    {
        float Cross(Vector2 a, Vector2 b) => a.x * b.y - a.y * b.x;
        float d1 = Cross(p2 - p1, q1 - p1);
        float d2 = Cross(p2 - p1, q2 - p1);
        float d3 = Cross(q2 - q1, p1 - q1);
        float d4 = Cross(q2 - q1, p2 - q1);

        // �����̂݋��F���������S�ɔ��� (<0) �łȂ���� false
        return (d1 * d2 < 0) && (d3 * d4 < 0);
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
                    // �q�b�g���̏���
                    Debug.Log($"Hit! #{l1.storageCount} �� #{l2.storageCount}");
                    _Points.crossLine = 
                    return true;
                }
            }
        }

        return false;
    }
}
