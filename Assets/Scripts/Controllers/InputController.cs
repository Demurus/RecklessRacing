using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    [SerializeField] private float UserDelay;
    [SerializeField] private float TimeToCountAsHold = 0.1f;

    public bool Interactable { get; set; }
    public UnityAction<bool> OnHold;
    public UnityAction OnTap;
    public UnityAction<bool> OnUserDelay;
    private bool isHolding;
    private float holdTime;
    private float userDelay = 0;
    public void Init()
    {
        Interactable = false;
        isHolding = false;
        holdTime = 0;
    }

    void Update()
    {
        if (Interactable)
        {
            if (!isHolding)
                userDelay += Time.deltaTime;

            if (OnTap != null)
            {
                if (Input.GetMouseButtonUp(0) && !isHolding)
                {
                    //Debug.Log("Click");
                    OnTap?.Invoke();
                    holdTime = 0;
                    userDelay = 0;
                    if (UserDelay != 0)
                        OnUserDelay?.Invoke(false);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (!isHolding)
                {
                    holdTime += Time.deltaTime;
                    if (holdTime > TimeToCountAsHold)
                    {
                        //Debug.Log("Hold Start");
                        isHolding = true;
                        OnHold?.Invoke(isHolding);
                        if (OnHold != null)
                        {
                            userDelay = 0;
                            if (UserDelay != 0)
                                OnUserDelay?.Invoke(false);
                        }
                    }
                }
            }
            else if (isHolding)
            {
                //Debug.Log("Hold End");
                isHolding = false;
                holdTime = 0;
                OnHold?.Invoke(isHolding);
                userDelay = 0;
            }
            //if (UserDelay != 0 && userDelay > UserDelay)
            // {
            //     userDelay = 0;
            //     OnUserDelay?.Invoke(true);
            // }
        }
    }

    public Ray GetCameraRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }


#if UNITY_EDITOR
    private void OnGUI()
    {
        string msg = "I" + (Interactable ? " + " : " - ");
        if (Interactable && UserDelay > 0)
        {
            msg += "UD" + (userDelay > UserDelay ? "+" : "-") + " : ";
        }
        Vector3 start = Camera.main.transform.position;
        Vector3 pos = Input.mousePosition;
        msg += pos;
        GUI.Label(new Rect(300, 0, 600, 100), msg, new GUIStyle() { fontSize = 40 });
        /*if(Interactable)
        {
            Debug.DrawLine(start, pos);
        }*/
    }
#endif
}
