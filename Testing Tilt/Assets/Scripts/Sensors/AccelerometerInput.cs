using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour {

    private Matrix4x4 calibrationMatrix;

	// Update is called once per frame
	void Update ()
    {
        //Vector3 acceleration = Input.acceleration;
        //Vector3 fixedAcceleration = fixAcceleration(acceleration);
        Debug.Log(Input.acceleration.z);
        transform.Translate(Input.acceleration.x, 0, -(Input.acceleration.z + 0.6f));
        //transform.Translate(fixedAcceleration.x, 0, fixedAcceleration.z * 0.1f);
    }

    //Used to calibrate the initial iPhoneIput.acceleration input
    public void calibrateAccelerometer()
    {
        Vector3 wantedDeadZone = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, 1f), wantedDeadZone);

        //create identity matrix ... rotate our matrix to match up with down vec
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f));
        //get the inverse of the matrix
        calibrationMatrix = matrix.inverse;

        Debug.Log("calibrateAccelerometer function called");
    }

    //Whenever you need an accelerator value from the user
    //call this function to get the 'calibrated' value
    Vector3 fixAcceleration(Vector3 accelerator)
    {
        Vector3 accel = calibrationMatrix.MultiplyVector(accelerator);
        return accel;
    }

}
