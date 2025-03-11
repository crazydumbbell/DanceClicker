using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    public Canvas canvas;  // UI가 포함된 캔버스
    public GraphicRaycaster raycaster; // UI 터치 감지용
    public GameObject tapPanel; // TapTapTap 패널

    // 터치 위치 반환 (UI 내부 터치면 위치 반환, 아니면 기본값)
    public Vector3 GetTouchPosition()
    {
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
        {
            Vector2 touchPos = Touchscreen.current.touches[0].position.ReadValue();

            if (IsTouchOnUI(touchPos)) // UI 위를 터치했는지 확인
            {
                RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    tapPanel.GetComponent<RectTransform>(), touchPos, canvas.worldCamera, out Vector3 worldPosition
                );

                return worldPosition; // UI 내부 터치 위치 반환
            }
        }

        return new Vector3(555, 1777, 0); // 터치가 없거나 UI 바깥을 터치한 경우
    }

    // UI 위를 터치했는지 여부 확인 (true: UI 터치, false: UI 바깥 터치)
    public bool IsTouchOnUI(Vector2 touchPos)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = touchPos };
        var results = new System.Collections.Generic.List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject == tapPanel) // TapTapTap 패널 터치 여부 확인
            {
                return true; // UI 터치
            }
        }
        return false; // UI 바깥 터치
    }
}
