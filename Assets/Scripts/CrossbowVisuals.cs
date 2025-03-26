using System.Collections.Generic;
using UnityEngine;

public class CrossbowVisuals : MonoBehaviour
{
    [SerializeField] LineRenderer _attackVisuals;
    [SerializeField] float duration = 0.1f;
    public void RenderLaserVisuals(Vector3 startPoint, Vector3 endPoint){
        // Definieren Sie für den LineRenderer startPoint und endPoint, als die
        // ersten 2 Punkte zwischen denen eine Linie gezogen werden soll.
        // Verwenden Sie die SetPosition(index, vector3) Methode. Der Index
        // beginnt mit 0.

        _attackVisuals.enabled = true;
        _attackVisuals.SetPosition(0, startPoint);
        _attackVisuals.SetPosition(1, endPoint);


        StartCoroutine(DisableLaserbeamRoutine());
    }

    private System.Collections.IEnumerator DisableLaserbeamRoutine() {
        // Der Effekt soll 0.1 Sekunden dauern

        // Schalten Sie anschließend den _lineRenderer aus
        yield return new WaitForSeconds(duration); 
        _attackVisuals.enabled = false;

    }
}
