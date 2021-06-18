using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Player : MonoBehaviour {

	private MazeCell currentCell;

	private MazeDirection currentDirection;

    private float rotationFactor = 1.0f;
    private float rotationSpeed = 6.0f;
    private Quaternion lastRotation = Quaternion.identity;
    private Quaternion currRotation = Quaternion.identity;

	public void SetLocation (MazeCell cell) {
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
		//transform.localPosition.y+=1;
	}

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = currentCell.GetEdge(direction);
		if (edge is MazePassage) {
			SetLocation(edge.otherCell);
		}
	}

	private void Look (MazeDirection direction) {
		rotationFactor = 0.0f;
        lastRotation = transform.localRotation;
        currRotation = direction.ToRotation();
		currentDirection = direction;
	}

	private void Update () {
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