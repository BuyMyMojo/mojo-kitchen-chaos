using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    public event EventHandler OnPickedUpSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedcounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlteranteAction += GameInput_OnInteractAlteranteAction; ;
    }

    private void GameInput_OnInteractAlteranteAction(object sender, EventArgs e)
    {
        if (GameStateManager.Instace.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
        
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (GameStateManager.Instace.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one player instance!");
        }
        Instance = this;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    /// <summary>
    /// Handle object interactions.
    /// </summary>
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Kepp a consistently updating record of the last move direction.
        // This means you can interact without holding down a direction.
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        // fire a raycast forward to search for objects in the countersLayerMask and output result to raycastHit
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            // if the raycast hits an object of type BaseCounter
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // if selectedCounter is not equal to baseCounter update it
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            // else if no BaseCounter was hit and selectedCounter isn't null then set it to null
            else if (selectedCounter != null)
            {
                SetSelectedCounter(null);
            }
        }
        // else if no BaseCounter was hit and selectedCounter isn't null then set it to null
        else if (selectedCounter != null)
        {
            SetSelectedCounter(null);
        }

    }

    /// <summary>
    /// Handle the player movement and collision.
    /// </summary>
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        // fire a CapsuleCast (A 3D raycast in the shape of a capsule) to see if there is an object in the way of the player movement.
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // if the raycast hit something then this canMove is false and we attempt to move on eithery X or Z
        if (!canMove)
        {
            // cannot move towards moveDir

            // attempt only X move
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            // if able to move on X then change moveDir to X
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // cannot move on X so attempt to move on Z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                // if able to move on Z then change moveDir to Z
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    // cannot move at all so canMove is still false
                }
            }
        }

        if (canMove)
        {
            // add the time ajusted movement vector to current position which keep a consistent speed no matter the FPS
            transform.position += moveDir * moveDistance;
        }

        // set isWalking to true if moveDir is not (0,0,0)
        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        // smothly rotate towards moveDir with a Slerp smoothed operation.
        // Slerp is for like rotations while Lerp is for positions
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedcounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter,
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickedUpSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
