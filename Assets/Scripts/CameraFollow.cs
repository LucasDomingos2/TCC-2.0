using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // O alvo que a câmera vai seguir, normalmente o personagem
    public Transform target;

    // Um deslocamento que será aplicado à posição da câmera
    public Vector3 offset = new Vector3(0, -3, -10);

    // A velocidade com que a câmera segue o personagem (mais suave ou mais rápido)
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // Posição desejada da câmera (a posição do alvo + o deslocamento)
        Vector3 desiredPosition = target.position + offset;

        // Suaviza o movimento da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplica a nova posição à câmera
        transform.position = smoothedPosition;
    }
}
