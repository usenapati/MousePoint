using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _cameraDir;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _cameraDir = _camera.transform.forward;
        _cameraDir.y = 0;

        transform.rotation = Quaternion.LookRotation(_cameraDir);
    }
}
