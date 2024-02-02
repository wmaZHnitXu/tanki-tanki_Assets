using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform camTrans;
    private PlayerController controller;
    private Tank playerTank;
    
    public void Initialize(PlayerController controller, Tank playerTank) {
        this.controller = controller;
        this.playerTank = playerTank;
    }

    void Update()
    {
        if (controller == null || playerTank == null) return;

        camTrans.position = new Vector3(playerTank.position.x, playerTank.position.y, camTrans.position.z);
        Vector2 lookPos = controller.GetLookVector();
        
    }
}
