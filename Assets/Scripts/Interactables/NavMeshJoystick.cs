using UnityEngine;

public class NavMeshJoystick : SimpleHingeInteractable
{
    [SerializeField] NavMeshRobot robot = null;
    [SerializeField] Transform rotationParentObject = null;
    [SerializeField] Transform trackedObject = null;
    [SerializeField] Transform trackingObject = null;

    protected override void Update()
    {
        base.Update();
        if (isSelected)
        {
            MoveRobot();
        }
    }

    private void MoveRobot()
    {
        trackingObject.position = new Vector3(trackedObject.position.x, trackingObject.position.y, trackedObject.position.z);
        rotationParentObject.rotation = Quaternion.identity;

        robot.MoveAgent(trackingObject.localPosition);
    }

    protected override void ResetHinge()
    {
        robot.StopAgent();
    }
}
