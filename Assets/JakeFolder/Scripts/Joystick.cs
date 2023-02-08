using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //키보드,마웃,터치를 이벤트로 오브젝트에원 보낼수 있는 기능을 지원

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform m_lever;
    private RectTransform m_rectTransform;

    [SerializeField]
    [Range(10f, 150f)]
    private float m_fLeverRange;

    [SerializeField]
    private Player m_player;

    private Vector2 m_inputDir;
    private bool m_bIsInput;



    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        JoystickControll(eventData);
        m_bIsInput = true;

        //오브젝트를 클릭해서 드래그하는 도중에 들어오는 이벤트
        //하지만 클릭을 유지한채로 마우스를 멈추면 이벤트가 들어오지않음


    }

    public void OnDrag(PointerEventData eventData)
    {

        JoystickControll(eventData);


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_lever.anchoredPosition = Vector2.zero;
        m_bIsInput = false;
        //m_playerController.Accelate(Vector2.zero);
        m_player.Move(Vector2.zero);

    }

    private void JoystickControll(PointerEventData eventData)
    {
       // Debug.Log(string.Format("{0} - {1}", eventData.position, m_rectTransform.anchoredPosition));
        var inputPos = eventData.position * (800f / Screen.width) - m_rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < m_fLeverRange ? inputPos : inputPos.normalized * m_fLeverRange;
        m_lever.anchoredPosition = inputVector;
        m_inputDir = inputVector / m_fLeverRange;

        // 나누는이유: 이 인풋데이터는 해상도로 만들어진값이라 너무 크다. 이 값을 0과 1사이의 정규화된 값으로 변환하고
        // 해상도가 다른 환경에서도 같은 인풋값을 받기 위함.
    }

    private void InputControlVector()
    {
        m_player.Move(m_inputDir);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (true == m_bIsInput)
        {
            InputControlVector();
        }
    }
}


