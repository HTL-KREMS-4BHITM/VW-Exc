using System.Collections.Generic;
using UnityEngine;

public class CannonTower : MonoBehaviour
{
    public Transform currentEnemy = null;
    // Reference tower_crossbow_head GameObject
    [Header("Tower Setup")]
    [SerializeField] private Transform _towerHead;
    [SerializeField] private Transform _towerSlide;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float _attackRange = 2.5f;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject gun_point;
    [SerializeField] private float launchAngle;
    [SerializeField] private float launchSpeed;
    [SerializeField] private int trajectoryPoints;
    private LineRenderer lineRenderer;
    private float gravity = -9.81f;






    void Awake()
    {
        lineRenderer=GetComponent<LineRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        
        RotateTowardsEnemy();
        if(currentEnemy == null){
            // sets the current currentEnemy we're looking at if we curently dont have one 
            currentEnemy = FindRandomEnemyWithinRange();
        }
        else{
            
            if(Vector3.Distance(transform.position, currentEnemy.position) > _attackRange){
                // if the currentEnemy is out of _attackRange we set him to null
                currentEnemy = null;
            }
            else{
                Debug.DrawLine(gun_point.transform.position, currentEnemy.transform.position);
                DrawTrajectory();
            }
        }















    }



    private void RotateTowardsEnemy() {
    Vector3 directionToEnemy = Vector3.forward; // sets default rotation direction 
    if(currentEnemy != null){
        // changes rotation direction to the enemy if there is an enemy 
        directionToEnemy = (currentEnemy.position - _towerHead.position);
    }


    // Pani
    // Create a Quaternion for the rotation towards the enemy, based on the direction vector
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToEnemy.x, 0, directionToEnemy.z));

    // Interpaltate smoothly between the current rotation of the towerss head and the deired look rotation
    // roationSpeed * Time.deltaTime adjusts the speed of  rotation to be frmewrate independent.
    Vector3 rotation = Quaternion.Lerp(_towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;

    // Apply the interplated rotation back toe the otwers head. This step vonverts the
    // Quaternion bach to Euler angles for straightforward applicat
    _towerHead.rotation = Quaternion.Euler(rotation);
    if(currentEnemy!= null){
        _towerSlide.rotation=Quaternion.RotateTowards(_towerSlide.rotation, lookRotation * Quaternion.Euler(launchAngle, 0, 0), rotationSpeed*Time.deltaTime);
    }

    // Pani ende

    }

    private Transform FindRandomEnemyWithinRange(){
        // checks if there are any coliders in the range of the _attackRange with the Layer enemyMask
        Collider[] enemiesAround = Physics.OverlapSphere(transform.position, _attackRange, enemyMask);
        if(enemiesAround.Length > 0){
            Debug.Log("enemy Found");
            // returnes an random enemy in the given range if there are any
            return enemiesAround[Random.Range(0, enemiesAround.Length)].transform;
        }

        else{
            // if there are no enemies we return null
            return null;
        }
    }




    private void OnDrawGizmos() {
    Gizmos.DrawWireSphere (transform.position - new Vector3(0, 1.25f, 0), _attackRange);
    }


    void DrawTrajectory()
    {
        List<Vector3> pointsList = new List<Vector3>(); // Use a list instead of an array
        float timeStep = 0.1f;

        float angleRad = launchAngle * Mathf.Deg2Rad;
        float initialVelocityX = launchSpeed * Mathf.Cos(angleRad);
        float initialVelocityY = launchSpeed * Mathf.Sin(angleRad);

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeStep;
            float x = initialVelocityX * t;
            float y = initialVelocityY * t - 0.5f * gravity * t * t;

            // Calculate world position
            Vector3 pos = new Vector3(
                gun_point.transform.position.x + x,
                gun_point.transform.position.y - y,
                gun_point.transform.position.z
            );

            pointsList.Add(pos);

            // Stop if the projectile hits the ground (world-space Y check)
            if (pos.y < 0) // Use actual ground height here (e.g., `pos.y < groundHeight`)
                break;
        }

        // Instantiate prefabs ONLY for valid points


        // Optional: Update LineRenderer with the list
        lineRenderer.positionCount = pointsList.Count;
        lineRenderer.SetPositions(pointsList.ToArray());
    }
}

