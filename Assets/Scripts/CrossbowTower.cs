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
    [SerializeField] GameObject gun_point;
    [SerializeField] CrossbowVisuals crossbowVisuals;
    private RaycastHit hitInfo;

    void Awake()
    {
        crossbowVisuals = GetComponent<CrossbowVisuals>();
    }

    void Start()
    {
        // Set The Default value => this code does not do what it is intendet to do one can remove it
        enemyMask = LayerMask.GetMask("Robot");
    }
    void Update() {
 
   

        
        RotateTowardsEnemy();
        if(currentEnemy == null){
            // sets the current currentEnemy we're looking at if we curently dont have one 
            currentEnemy = FindRandomEnemyWithinRange();
        } 
        else{
            // if we have an currentEnemy we check if his position is further away than our _attackRange
            if(Vector3.Distance(currentEnemy.position, transform.position) > _attackRange){
                // if the currentEnemy is out of _attackRange we set him to null
                currentEnemy = null;
            }
            else{
                Attack();

            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere (transform.position, _attackRange);
    }

    private void RotateTowardsEnemy() {
        Vector3 directionToEnemy = Vector3.forward; // sets default rotation direction 
        if(currentEnemy != null){
            // changes rotation direction to the enemy if there is an enemy 
            directionToEnemy = (currentEnemy.position - _towerHead.position);
        }


        // Pani
        // Create a Quaternion for the rotation towards the enemy, based on the direction vector
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        // Interpaltate smoothly between the current rotation of the towerss head and the deired look rotation
        // roationSpeed * Time.deltaTime adjusts the speed of  rotation to be frmewrate independent.
        Vector3 rotation = Quaternion.Lerp(_towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;

        // Apply the interplated rotation back toe the otwers head. This step vonverts the
        // Quaternion bach to Euler angles for straightforward applicat
        _towerHead.rotation = Quaternion.Euler(rotation);
        // Pani ende

    }

    private Transform FindRandomEnemyWithinRange(){
        // checks if there are any coliders in the range of the _attackRange with the Layer enemyMask
        Collider[] enemiesAround = Physics.OverlapSphere(transform.position, _attackRange, enemyMask);
        if(enemiesAround.Length > 0){
            // returnes an random enemy in the given range if there are any
            return enemiesAround[Random.Range(0, enemiesAround.Length)].transform;
        }

        else{
            // if there are no enemies we return null
            return null;
        }
    }
    protected Vector3 CalculateDirectionVector(Transform begin, Transform end){
        return (end.position - begin.position).normalized;
    }
    void Attack(){
        Vector3 directionToEnimey = CalculateDirectionVector(gun_point.transform, currentEnemy);
        if(Physics.Raycast(gun_point.transform.position, directionToEnimey, out hitInfo, _attackRange)){
            crossbowVisuals.RenderLaserVisuals(gun_point.transform.position, hitInfo.point);
        }
    }
}
