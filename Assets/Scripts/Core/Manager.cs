using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;



    public delegate void MoveRotateAction(int _index);

    public static MoveRotateAction moveRotateAction;
    public static Action disableMovementRotation;

    public enum ManipulationType { Move, Rotate, None };

    public ManipulationType manipulationType;



    [SerializeField]
    GameObject cubePrefab;

    public GameObject instantiatedObject;
    [SerializeField]
    Camera camera;
    [SerializeField] Vector3 offSet;
    [SerializeField]
    AxisCheck.MyAxis axisMove;



    Vector3 mousePointerStartPosition, mousePositionVIACamera;




    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        ManageCubeInstantitaeAndControlInput();


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, 100.0f) && !instantiatedObject)
            {
                instantiatedObject = raycastHit.transform.gameObject;
                instantiatedObject.GetComponent<Outline>().enabled = true;
            }
        }
    }

    private void ManageCubeInstantitaeAndControlInput()
    {
        if (!Input.GetMouseButton(0))
        {
            RaycastHit raycastHit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, 100.0f) && instantiatedObject)
            {
                if (raycastHit.transform.GetComponent<AxisCheck>())
                    axisMove = raycastHit.transform.GetComponent<AxisCheck>().myAxis;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (instantiatedObject) instantiatedObject.GetComponent<Outline>().enabled = false;
            instantiatedObject = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
            instantiatedObject.GetComponent<Outline>().enabled = true;

        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            manipulationType = ManipulationType.Move;
            if (instantiatedObject) moveRotateAction.Invoke(0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            manipulationType = ManipulationType.Rotate;
            if (instantiatedObject) moveRotateAction.Invoke(1);


        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            manipulationType = ManipulationType.None;
            if (instantiatedObject) disableMovementRotation.Invoke();
            instantiatedObject = null;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)) mousePointerStartPosition = Input.mousePosition;
        if (instantiatedObject) DealCubesMovementAndRotation();

    }

    private void DealCubesMovementAndRotation()
    {
        if (Input.GetMouseButton(0))
        {
            mousePositionVIACamera = camera.ScreenPointToRay(Input.mousePosition).direction * 10f;

            if (manipulationType == ManipulationType.Move)
            {
                switch (axisMove)
                {
                    case AxisCheck.MyAxis.X:
                        instantiatedObject.transform.localPosition = new Vector3(mousePositionVIACamera.x - offSet.x, instantiatedObject.transform.localPosition.y, instantiatedObject.transform.localPosition.z);
                        break;
                    case AxisCheck.MyAxis.Y:
                        instantiatedObject.transform.localPosition = new Vector3(instantiatedObject.transform.localPosition.x, mousePositionVIACamera.y - offSet.y, instantiatedObject.transform.localPosition.z);
                        break;
                    /* NOT MOVNG ON Z AXIS, AS IT CAUSES ABNORMAL BEHAVIOUR  */
                    // case AxisCheck.MyAxis.Z:
                    //     instantiatedObject.transform.position = new Vector3(instantiatedObject.transform.position.x, instantiatedObject.transform.position.y, mousePositionVIACamera.z);
                    //     break; 
                    case AxisCheck.MyAxis.Whole:
                        instantiatedObject.transform.localPosition = new Vector3(mousePositionVIACamera.x, mousePositionVIACamera.y - offSet.y, 0f);
                        break;
                }
            }
            else if (manipulationType == ManipulationType.Rotate)
            {
                switch (axisMove)
                {
                    case AxisCheck.MyAxis.X:
                        instantiatedObject.transform.rotation = Quaternion.Euler(instantiatedObject.transform.rotation.x + (Input.mousePosition.y - mousePointerStartPosition.y), instantiatedObject.transform.rotation.y, instantiatedObject.transform.rotation.z);
                        break;
                    case AxisCheck.MyAxis.Y:
                        instantiatedObject.transform.rotation = Quaternion.Euler(instantiatedObject.transform.rotation.x, instantiatedObject.transform.rotation.y + (mousePointerStartPosition.x - Input.mousePosition.x), instantiatedObject.transform.rotation.z);
                        break;
                    case AxisCheck.MyAxis.Z:
                        instantiatedObject.transform.rotation = Quaternion.Euler(instantiatedObject.transform.rotation.x, instantiatedObject.transform.rotation.y, Input.mousePosition.x - mousePointerStartPosition.x);
                        break;
                        // case AxisCheck.MyAxis.Whole:
                        //     instantiatedObject.transform.rotation = Quaternion.Euler((Input.mousePosition.z - mousePointerStartPosition.z), (mousePointerStartPosition.y - Input.mousePosition.y), (Input.mousePosition.x - mousePointerStartPosition.x));
                        //     break;
                }

            }
        }
    }
}
