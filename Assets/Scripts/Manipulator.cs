using UnityEngine;

public class Manipulator : MonoBehaviour
{
    public GameObject movementHandle;
    public GameObject rotationHandle;

    public Outline outline;


    public void OnEnable()
    {


        if (Manager.instance.manipulationType == Manager.ManipulationType.Move)
        {
            movementHandle.SetActive(true);
        }
        else if (Manager.instance.manipulationType == Manager.ManipulationType.Rotate)
        {
            rotationHandle.SetActive(true);
        }




        Manager.moveRotateAction += EnableMovementOrRotation;
        Manager.disableMovementRotation += DisableMovementAndRotation;


    }

    public void OnDisable()
    {
        Manager.moveRotateAction -= EnableMovementOrRotation;
        Manager.disableMovementRotation -= DisableMovementAndRotation;

    }



    public void EnableMovementOrRotation(int _index)
    {
        switch (_index)
        {
            case 0:
                movementHandle.SetActive(true);
                rotationHandle.SetActive(false);
                break;
            case 1:
                movementHandle.SetActive(false);
                rotationHandle.SetActive(true);
                break;
        }

    }

    public void DisableMovementAndRotation()
    {
        movementHandle.SetActive(false);
        rotationHandle.SetActive(false);
        outline.enabled = false;
    }
}
