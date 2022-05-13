using UnityEngine;
using System.Collections;

public class WarpChara : MonoBehaviour
{

    public enum WarpCharaState
    {
        GoToWarpPoint,
    };

    private WarpCharaState state;
    private Transform waitPoint;
    private Transform warpPoint;

    public float goToWaitPointSpeed;

    void Start()
    {
    }

    void Update()
    {
        if (state == WarpCharaState.GoToWarpPoint)
        {
            GoToWarpWaitPoint();
        }
    }
    void GoToWarpWaitPoint()
    {

        if (Vector3.Distance(transform.position, waitPoint.position) > 0.1f
            || Quaternion.Angle(transform.rotation, waitPoint.rotation) >= 5f
        )
        {
            Debug.Log("移動中");
            transform.position = Vector3.Lerp(transform.position, waitPoint.position, goToWaitPointSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, waitPoint.rotation, goToWaitPointSpeed * Time.deltaTime);
        }
        else if (transform.position != waitPoint.position
          //&& Quaternion.Angle(transform.rotation, waitPoint.rotation) < 5f
          )
        {
            Debug.Log("きっちり位置と角度を合わせる");
            transform.position = waitPoint.position;
            transform.rotation = waitPoint.rotation;
            //　3秒後にワープ
            Invoke("Warp", 3f);
        }
    }
    void Warp()
    {
        transform.position = warpPoint.position;
        transform.rotation = warpPoint.rotation;
    }
    public void SetState(WarpCharaState state, Transform waitPoint = null, Transform warpPoint = null)
    {
        this.state = state;
        this.waitPoint = waitPoint;
        this.warpPoint = warpPoint;
    }
}