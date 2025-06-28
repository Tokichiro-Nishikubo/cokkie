using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
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
        public Vector2 crossPoint;   // �����ꏊ
        public Vector2 xMinYMin;
        public Vector2 xMinYMax;
        public Vector2 xMaxYMin;
        public Vector2 xMaxYMax;
    }

    private List<LineSeg> _straightLineHistory = new List<LineSeg>();
    private StoragePoints _Points = new StoragePoints();      // �K�v�ȃ|�C���g�̏����i�[
    private int _curHistoryCnt = 0;                 // �}�E�X�̕ۑ��t���[�� / �O�̃t���[�����擾���₷������ׂɁATime�ł͂Ȃ�Count�ōs��
    private int _straightLineStreakCnt = 0;         // �������A�������ꍇ�̃J�E���g / �œK���p
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
            ResetInfinitData();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _curCursorPos = Input.mousePosition;
            // �}�E�X�J�[�\���擾�̌�ɌĂяo��
            StoragekMinMaxPoint();

            if (_straightLineHistory.Count > _deleteHistoryFrame)
            {
                _straightLineHistory.RemoveAt(0);
            }

            if (_curHistoryCnt >= 1)
            {
                _straightLineHistory.Add(new LineSeg(_curHistoryCnt, _prevCursorPos, _curCursorPos));
            }
            else// ������W�o�^
            {
                _Points.xMinYMin = _curCursorPos;
                _Points.xMinYMax = _curCursorPos;
                _Points.xMaxYMin = _curCursorPos;
                _Points.xMaxYMax = _curCursorPos;
            }

            // _straightLineHistory�ǉ���ɏ�����
            _curHistoryCnt++;
            _prevCursorPos = _curCursorPos;

            // _curHistoryCnt++�̌�ɔz�u
            CheckAllIntersections();
        }
    }

    // ���Ɛ��̓����蔻��
    public bool IsLineSegmentIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
    {
        // �x�N�g��
        Vector2 r = p2 - p1;
        Vector2 s = q2 - q1;

        float rxs = r.x * s.y - r.y * s.x;
        // �����悤�ȕ����̏ꍇ�́A�������Ă��Ȃ�������s��
        if (Mathf.Approximately(rxs, 0f)) return false;  

        Vector2 qp = q1 - p1;
        float t = (qp.x * s.y - qp.y * s.x) / rxs;
        float u = (qp.x * r.y - qp.y * r.x) / rxs;

        // �[�_������ 0 �� 1 �͊܂߂Ȃ��i���������j
        const float Eps = 1e-6f;
        if (t <= Eps || t >= 1f - Eps) return false;
        if (u <= Eps || u >= 1f - Eps) return false;

        _Points.crossPoint = p1 + t * r;   // ��_
        // Debug.Log("����");

        return true;
    }
    // �S�Ă̐���{�����ăq�b�g������擾����
    private bool CheckAllIntersections()
    {
        int maxCount = _straightLineHistory.Count;

        // �������������^�C�~���O�ŁA�v�f�����ׂč폜�������̃G���[�Ώ��p
        if(maxCount <= 2) return false;

        for (int firstLine = 0; firstLine < maxCount - 1; firstLine++)
        {
            var l1 = _straightLineHistory[firstLine];
            for (int nextLine = firstLine + 1; nextLine < maxCount; nextLine++)
            {
                var l2 = _straightLineHistory[nextLine];

                if (IsLineSegmentIntersect(l1.start, l1.end, l2.start, l2.end))
                {
                    // �q�b�g���̏���
                    //Debug.Log($"Hit! #{l1.storageCount} �� #{l2.storageCount}");
                    if(CheckInfinit())
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
    // �|�C���g�̍ŏ��ő���i�[����
    private void StoragekMinMaxPoint()
    {
        // ����
        if(_curCursorPos.x <= _Points.xMinYMin.x && _curCursorPos.y <= _Points.xMinYMin.y)
        {
            _Points.xMinYMin = _curCursorPos;
            // Debug.Log("�����i�[");
        }
        // ����
        if(_curCursorPos.x <= _Points.xMinYMax.x && _curCursorPos.y >= _Points.xMinYMax.y)
        {
            _Points.xMinYMax = _curCursorPos;
            // Debug.Log("����i�[");
        }
        // �E��
        if (_curCursorPos.x >= _Points.xMaxYMin.x && _curCursorPos.y <= _Points.xMaxYMin.y)
        {
            _Points.xMaxYMin = _curCursorPos;
            // Debug.Log("�E���i�[");
        }
        // �E��
        if (_curCursorPos.x >= _Points.xMaxYMax.x && _curCursorPos.y >= _Points.xMaxYMax.y)
        {
            _Points.xMaxYMax = _curCursorPos;
            // Debug.Log("�E��i�[");
        }
    }

    // �������������Ă��邩�ǂ��� / StragePoint�ɂS�_���i�[����Ă��āAcrossPoint�Ƃ̔�����N���A�����true
    private bool CheckInfinit()
    {
        Vector2 crossPoint = _Points.crossPoint;
        //Debug.Log(crossPoint);
        // ����
        if (crossPoint.x <= _Points.xMinYMin.x || crossPoint.y <= _Points.xMinYMin.y)
        {
            //Debug.Log("����" + _Points.xMinYMin);
            return false;
        }
        // ����
        if (crossPoint.x <= _Points.xMinYMax.x || crossPoint.y >= _Points.xMinYMax.y)
        {
            //Debug.Log("����" + _Points.xMinYMax);
            return false;
        }
        // �E��
        if (crossPoint.x >= _Points.xMaxYMin.x || crossPoint.y <= _Points.xMaxYMin.y)
        {
            //Debug.Log("�E��" + _Points.xMaxYMin);
            return false;
        }
        // �E��
        if (crossPoint.x >= _Points.xMaxYMax.x || crossPoint.y >= _Points.xMaxYMax.y)
        {
            //Debug.Log("�E��" + _Points.xMaxYMax);
            return false;
        }
        //Debug.Log("����" + _Points.xMinYMin);
        //Debug.Log("����" + _Points.xMinYMax);
        //Debug.Log("�E��" + _Points.xMaxYMin);
        //Debug.Log("�E��" + _Points.xMaxYMax);

        //Debug.Log("��_�F" + _Points.crossPoint);
        // Debug.Log("�C���t�B�j�e�B�I");
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
