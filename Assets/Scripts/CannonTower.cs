using UnityEngine;

public class CannonTower : MonoBehaviour
{
    public Transform currentEnemy = null;
    // Reference tower_crossbow_head GameObject
    [Header("Tower Setup")]
    [SerializeField] private Transform _towerHead;
    [SerializeField] private Transform _towerSlide;
    [SerializeField] private float rotationSpeed;
    [SerializeField] float _attackRange = 2.5f;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] GameObject gun_point;




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
        _towerSlide.rotation=Quaternion.Lerp(_towerSlide.rotation, Quaternion.Euler(-45f, lookRotation.eulerAngles.y, lookRotation.eulerAngles.z), rotationSpeed*Time.deltaTime);
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
}
