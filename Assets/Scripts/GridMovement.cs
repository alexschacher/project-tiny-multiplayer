using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 originPos, targetPos;
    [SerializeField] private float timeToMoveOrtho = 0.2f;

    private void Update()
    {
        if (!isMoving)
        {
            PollForInput();
        }
    }

    private void PollForInput()
    {
        float horizontalInput = 0, verticalInput = 0;

        if (Input.GetKey(KeyCode.W)) verticalInput += 1;
        if (Input.GetKey(KeyCode.S)) verticalInput -= 1;
        if (Input.GetKey(KeyCode.A)) horizontalInput -= 1;
        if (Input.GetKey(KeyCode.D)) horizontalInput += 1;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            DetermineMovement(horizontalInput, verticalInput);
        }
    }

    private void DetermineMovement(float horizontalInput, float verticalInput)
    {
        Vector3 dir = VectorMath.ConvertInputVectorUsingCamera(horizontalInput, verticalInput, Camera.main.transform);
        dir = VectorMath.RoundNormalizedVectorTo45DegreeGridMovement(dir);
        StartCoroutine(MovePlayer(dir));
    }

    private IEnumerator MovePlayer(Vector3 dir)
    {
        isMoving = true;
        originPos = transform.position;
        targetPos = originPos + dir;

        float elapsedTime = 0f;
        float timeToMove = timeToMoveOrtho;
        if (dir.x != 0 && dir.z != 0)
        {
            timeToMove = timeToMoveOrtho * 1.414f;
        }

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, elapsedTime/ timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}