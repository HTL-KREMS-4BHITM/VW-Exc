using UnityEngine;
using UnityEngine.UIElements;

public class CrossbowTower : MonoBehaviour
{
    // Reference BasicEnemy GameObject
    public Transform currentEnemy;
    // Reference tower_crossbow_head GameObject
    [Header("Tower Setup")]
    [SerializeField]
    private Transform _towerHead;
    // Set rotationSpeed to 10
    [SerializeField] private float rotationSpeed;
    [SerializeField] float _attackRange = 2.5f;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] Transform defaultpos;

    void Start()
    {
        enemyMask = LayerMask.GetMask("Robot");

    }
    void Update() {

   
        if(currentEnemy != null){
            if(Vector3.Distance(currentEnemy.position, transform.position) > _attackRange){
                currentEnemy = FindRandomEnemyWithinRange();
            }
            RotateTowardsEnemy();
        }
        else{
            currentEnemy = defaultpos.transform;
            RotateTowardsEnemy();
        }

    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere (transform.position, _attackRange);
    }

    private void RotateTowardsEnemy() {
        // calculate the vector direction from the towers head to the current enemy.
        Vector3 directionToEnemy = (currentEnemy.position - _towerHead.position);

        // Create a Quaternion for the rotation towards the enemy, based on the direction vector
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        // Interpaltate smoothly between the current rotation of the towerss head and the deired look rotation
        // roationSpeed * Time.deltaTime adjusts the speed of  rotation to be frmewrate independent.
        Vector3 rotation = Quaternion.Lerp(_towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;

        // Apply the interplated rotation back toe the otwers head. This step vonverts the
        // Quaternion bach to Euler angles for straightforward applicat
        _towerHead.rotation = Quaternion.Euler(rotation);

        
    }

    private Transform FindRandomEnemyWithinRange(){
        Collider[] enemiesAround = Physics.OverlapSphere(transform.position, _attackRange, enemyMask);
        if(enemiesAround.Length > 0){
        return enemiesAround[Random.Range(0, enemiesAround.Length)].transform;
        }
        else{
            return defaultpos;
        }
    }
}
