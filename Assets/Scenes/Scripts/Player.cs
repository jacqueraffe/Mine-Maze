using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Player : MonoBehaviour {

	private MazeCell currentCell;

	private MazeDirection currentDirection;

    private float translationFactor = 1.0f;
    private float translationSpeed = 6.0f;
    private Vector3 lastPosition;
    private Vector3 currPosition;

    private float rotationFactor = 1.0f;
    private float rotationSpeed = 6.0f;
    private Quaternion lastRotation = Quaternion.identity;
    private Quaternion currRotation = Quaternion.identity;

	public void SetLocation (MazeCell cell) {
		lastPosition = cell.transform.localPosition;
		currPosition = lastPosition;
		transform.localPosition = lastPosition;
        translationFactor = 1.0f;
		currentCell = cell;
	}
	
	private void SetLocationLerp(MazeCell cell) {
		lastPosition = transform.localPosition;
		currPosition = cell.transform.localPosition;
        translationFactor = 0.0f;
		currentCell = cell;
	}

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = currentCell.GetEdge(direction);
		if (edge is MazePassage) {
			SetLocationLerp(edge.otherCell);
		}
	}

	private void Look (MazeDirection direction) {
		rotationFactor = 0.0f;
        lastRotation = transform.localRotation;
        currRotation = direction.ToRotation();
		currentDirection = direction;
	}

	private void Update () {
		translationFactor += Time.deltaTime * translationSpeed;
		transform.localPosition = Vector3.Lerp(lastPosition,currPosition, translationFactor);

		rotationFactor += Time.deltaTime * rotationSpeed;
        transform.localRotation = Quaternion.Lerp(lastRotation, currRotation, rotationFactor);

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			Move(currentDirection);
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(currentDirection.GetNextClockwise());
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			Move(currentDirection.GetOpposite());
		}
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(currentDirection.GetNextCounterclockwise());
		}
		else if (Input.GetKeyDown(KeyCode.Q)) {
			Look(currentDirection.GetNextCounterclockwise());
		}
		else if (Input.GetKeyDown(KeyCode.E)) {
			Look(currentDirection.GetNextClockwise());
		}
	}
}