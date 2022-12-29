using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public enum RotationAxis{
        X, nX,
        Y, nY,
        Z, nZ
    }
    public MeshRenderer Renderer;
    Material material;
    private RotationAxis rotationAxis;
    [SerializeField] private float rotationSpeed = 30.0f;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private Vector3 goalPosition;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float scaleSpeed = 2.0f;
    [SerializeField] private float goalScale = 2.0f;
    private float current, target, scaleCurrent, scaleTarget, opacityCurrent, opacityTarget, opacityGoal = 1.0f;

    private Color slime = new Color(0.5f, 1.0f, 0.3f, 0.4f);
    private Color blue = new Color(0.0f, 0.0f, 1.0f, 0.4f);
    private Color red = new Color(1.0f, 0.0f, 0.0f, 0.4f);
    private Color yellow = new Color(1.0f, 1.0f, 0.0f, 0.4f);
    void Start()
    {
        goalPosition = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;
        
        material = Renderer.material;
        
        material.color = slime;

        rotationAxis = RotationAxis.X;
    }
    
    void Update()
    {

        // Press the 'Q' key to change the position of the cube
        if(Input.GetKeyDown(KeyCode.Q)){
            target = target == 0 ? 1 : 0;
        }
        current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);
        transform.position = Vector3.Lerp(Vector3.zero, goalPosition, curve.Evaluate(current));

        // Press the 'W' key to change the scale of the cube
        if(Input.GetKeyDown(KeyCode.W)){
            scaleTarget = scaleTarget == 0 ? 1 : 0;
        }
        scaleCurrent = Mathf.MoveTowards(scaleCurrent, scaleTarget, scaleSpeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * goalScale, scaleCurve.Evaluate(scaleCurrent));

        // Using the keys Alpha1-6, rotate the cube depending on the axis.
        CubeRotation();

        // Using the keys S, A, D, F, change the color of the cube.
        CubeColor();

        if(Input.GetKeyDown(KeyCode.Space)){
            opacityTarget = opacityTarget == 0 ? 1 : 0;
        }
        opacityCurrent = Mathf.MoveTowards(opacityCurrent, opacityTarget, speed * Time.deltaTime);
        material.color = new Color(material.color.r, material.color.g, material.color.b, Mathf.Lerp(0.0f, opacityGoal, curve.Evaluate(opacityCurrent)));
            

    }

    void CubeColor(){
        Material material = Renderer.material;
        if(Input.GetKeyDown(KeyCode.S)){
            material.color = slime;
            CancelInvoke();
        }
        if(Input.GetKeyDown(KeyCode.A)){
            material.color = blue;
            CancelInvoke();
        }
        if(Input.GetKeyDown(KeyCode.D)){
            material.color = red;
            CancelInvoke();
        }
        if(Input.GetKeyDown(KeyCode.F)){
            material.color = yellow;
            CancelInvoke();
        }
        if(Input.GetKeyDown(KeyCode.G))
            //isColorChanging = true;
            InvokeRepeating("ChangeColor", 0.0f, 0.5f);
            
    }
    void ChangeColor(){
        Color newColor = new Color(Random.value, Random.value, Random.value);
        material.color = newColor;
    }
    void CubeRotation(){
        float _xAxisRotation = 0.0f, _yAxisRotation = 0.0f, _zAxisRotation = 0.0f;
        if(Input.GetKeyDown(KeyCode.Alpha1))
            rotationAxis = RotationAxis.X;
        if(Input.GetKeyDown(KeyCode.Alpha2))
            rotationAxis = RotationAxis.Y;
        if(Input.GetKeyDown(KeyCode.Alpha3))
            rotationAxis = RotationAxis.Z;
        if(Input.GetKeyDown(KeyCode.Alpha4))
            rotationAxis = RotationAxis.nX;
        if(Input.GetKeyDown(KeyCode.Alpha5))
            rotationAxis = RotationAxis.nY;
        if(Input.GetKeyDown(KeyCode.Alpha6))
            rotationAxis = RotationAxis.nZ;

        switch(rotationAxis){
            case RotationAxis.X:
                _xAxisRotation = rotationSpeed * Time.deltaTime;
                _yAxisRotation = 0.0f;
                _zAxisRotation = 0.0f;
                break;
            case RotationAxis.nX:
                _xAxisRotation = -rotationSpeed * Time.deltaTime;
                _yAxisRotation = 0.0f;
                _zAxisRotation = 0.0f;
                break;
            case RotationAxis.Y:
                _yAxisRotation = rotationSpeed * Time.deltaTime;
                _zAxisRotation = 0.0f;
                _xAxisRotation = 0.0f;
                break;
            case RotationAxis.nY:  
                _yAxisRotation = -rotationSpeed * Time.deltaTime;
                _zAxisRotation = 0.0f;
                _xAxisRotation = 0.0f;
                break;
            case RotationAxis.Z:
                _zAxisRotation = rotationSpeed * Time.deltaTime;
                _yAxisRotation = 0.0f;
                _xAxisRotation = 0.0f;
                break;
            case RotationAxis.nZ:
                _zAxisRotation = -rotationSpeed * Time.deltaTime;
                _yAxisRotation = 0.0f;
                _xAxisRotation = 0.0f;
                break;
            default:
            break;
        }
        transform.Rotate(_xAxisRotation, _yAxisRotation, _zAxisRotation);
    }
}
