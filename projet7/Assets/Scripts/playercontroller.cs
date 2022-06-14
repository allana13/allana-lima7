using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class playercontroller : MonoBehaviour
{

  public float moveSpeed;

  private GameInput _gameInput;
  private PlayerInput _playerInput;
  private Camera _mainCamera;
  private Rigidbody _rigidbody;
  private Vector2 _moveImput;
  

  private void OnEnable()
  {
     //inicializacao de variavel
     _gameInput = new GameInput();

     //referencias dos componentes no mesmo objeto do unity
     _playerInput = GetComponent <PlayerInput>();
     _rigidbody = GetComponent<Rigidbody>();
     
     //referencias para a camera main guardada no class input
     _mainCamera = Camera.main;
     
     //Atribuindo um delegado ao 
     _playerInput.onActionTriggered += OnActionTriggered;
  }
  private void OnDisable()
  {
      _playerInput.onActionTriggered -= OnActionTriggered;
  }

  private void OnActionTriggered(InputAction.CallbackContext obj)
  {
      if (obj.action.name.CompareTo(_gameInput.Gameplay.Movement.name) == 0)
      {
          //atribuir ao moveImput o valor proveniente do input do jogador como Vector2
          _moveImput = obj.ReadValue<Vector2>();
      }
  }

 
  private void move()
  {
      //calcula o movimento no eixo da camera para movimento frente/tras
      Vector3 moveVertical = _mainCamera.transform.forward * _moveImput.y;
      // calcula o movimento no eixo da camera para movimento esquerda/direita
      Vector3 moveHorizontal = _mainCamera.transform.right * _moveImput.x;
        
      //Adicione a forca no objeto pelo rigidbody, com  intecidade dada por aveSpeed
      _rigidbody.AddForce((moveVertical + moveHorizontal) * moveSpeed * Time.fixedDeltaTime);


  }
  private void FixedUpdate() 
  {
      move();
  }

}





