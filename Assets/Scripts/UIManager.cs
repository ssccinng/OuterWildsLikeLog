using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }


    public GameObject SpaceShipLog;

    bool _isDraggingSpaceShipLog = true;


    #region UI组件
    public Button SaveButton;
    #endregion


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveButton.onClick.AddListener(() =>
        {
            // Save the log
            var logNode = new LogNode
            {
                Name = "Node1",
                Description = "This is a node",
                Connections = new System.Collections.Generic.List<LogNode>(),
                IsDiscovered = false,
                Position = new (0, 0)
            };

            var json = JsonConvert.SerializeObject(logNode);
            Debug.Log(json);
        });
    }

    // Update is called once per frame
    void Update()
    {
        DragSpaceShipLog();

    }


    void DragSpaceShipLog()
    {
        if (_isDraggingSpaceShipLog)
        {
            // when mouse is clicked and dragged on the screen, move the spaceship log

            if (Input.GetMouseButton(0))
            {
                var spacePosition = SpaceShipLog.transform.position;
                // move the spaceship log , delta is the difference mouse position in the current frame and the previous frame
                SpaceShipLog.transform.position = new Vector3(spacePosition.x + Input.GetAxis("Mouse X") * 20, spacePosition.y + Input.GetAxis("Mouse Y") * 20, 0);
            }

            // when mouse wheel is scrolled, change SpaceShipLog scale
            if (Input.mouseScrollDelta.y != 0)
            {
                var scale = SpaceShipLog.transform.localScale;
                var mutilplier = 1 + Input.mouseScrollDelta.y / 10;
                Vector3 GetNewScale(Vector3 scale, float mutilplier)
                {
                    Debug.Log("scale.x: " + scale.x);
                    Debug.Log("mutilplier: " + mutilplier);
                    if (mutilplier > 1)
                    {
                        if (scale.x > 4)
                        {
                            return scale;
                        }
                    }
                    else
                    { // mutilplier < 1
                        if (scale.x < 0.5)
                        {
                            return scale;
                        }
                    }
                    return new Vector3(scale.x * mutilplier, scale.y * mutilplier, scale.z);
                }

                var newScale = GetNewScale(scale, mutilplier);

                SpaceShipLog.transform.localScale = newScale;
            }

        }

    }
}
