using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Test test;
    [SerializeField] Text labelScore;

    private void OnEnable()
    {
        labelScore.text = $"�� �������� {test.ValueScore} ������\n�� {test.MaxScore} ���������";
    }
}
