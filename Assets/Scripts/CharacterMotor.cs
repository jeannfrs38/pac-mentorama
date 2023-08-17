using System;
using UnityEngine;


public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right
}
public class CharacterMotor : MonoBehaviour
{
    public float MoveSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _diseredMovementDirection;
    private Vector2 _currentMovementDirection;
    private Vector2 _boxSize;

    public event Action OnAlignedWithGrid;
    public event Action<Direction> OnDirectionChanged;
    private LayerMask _collisionLayerMask;
    public LayerMask CollisionLayerMask
    {
        get => _collisionLayerMask;
    }

    public Direction CurrentDirection
    {
        get
        {
            //Up
            if (_currentMovementDirection.y > 0)
            {
                return Direction.Up;
            }

            //Left
            if (_currentMovementDirection.x < 0)
            {
                return Direction.Left;
            }
            //Down
            if (_currentMovementDirection.y < 0)
            {
                return Direction.Down;
            }
            //Right
            if (_currentMovementDirection.x > 0)
            {
                return Direction.Right;
            }

            return Direction.None;
        }
    }
    public void SetMoveDirection(Direction newMoveDirection)
    {
        switch (newMoveDirection)
        {
            default:
            case Direction.None:
                break;

            case Direction.Up:
                _diseredMovementDirection = Vector2.up;
                break;

            case Direction.Down:
                _diseredMovementDirection = Vector2.down;
                break;

            case Direction.Left:
                _diseredMovementDirection = Vector2.left;
                break;

            case Direction.Right:
                _diseredMovementDirection = Vector2.right;
                break;
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxSize = GetComponent<BoxCollider2D>().size;
        _collisionLayerMask = LayerMask.GetMask(new string[] { "Level", "Gates" });
    }



    private void FixedUpdate()
    {
        var moveDistance = MoveSpeed * Time.fixedDeltaTime;
        var nextMovePosition = _rigidbody.position + _currentMovementDirection * moveDistance;
        //Up
        if (_currentMovementDirection.y > 0)
        {
            var maxY = Mathf.CeilToInt(_rigidbody.position.y);

            if (nextMovePosition.y >= maxY)
            {
                transform.position = new Vector2(_rigidbody.position.x, maxY);
                moveDistance = nextMovePosition.y - maxY;
            }
        }

        //Left
        if (_currentMovementDirection.x < 0)
        {
            var minX = Mathf.FloorToInt(_rigidbody.position.x);

            if (nextMovePosition.x <= minX)
            {
                transform.position = new Vector2(minX, _rigidbody.position.y);
                moveDistance = minX - nextMovePosition.x;
            }
        }
        //Down
        if (_currentMovementDirection.y < 0)
        {
            var minY = Mathf.FloorToInt(_rigidbody.position.y);

            if (nextMovePosition.y <= minY)
            {
                transform.position = new Vector2(_rigidbody.position.x, minY);
                moveDistance = minY - nextMovePosition.y;
            }
        }
        //Right
        if (_currentMovementDirection.x > 0)
        {
            var maxX = Mathf.CeilToInt(_rigidbody.position.x);

            if (nextMovePosition.x >= maxX)
            {
                transform.position = new Vector2(maxX, _rigidbody.position.y);
                moveDistance = nextMovePosition.x - maxX;
            }
        }

        Physics2D.SyncTransforms();


        //Verifica Alinhamento
        if (_rigidbody.position.x == Mathf.CeilToInt(_rigidbody.position.x) && _rigidbody.position.y == Mathf.CeilToInt(_rigidbody.position.y) || _currentMovementDirection == Vector2.zero)
        {
            OnAlignedWithGrid?.Invoke();
            if (_currentMovementDirection != _diseredMovementDirection)
            {
                if (!Physics2D.BoxCast(_rigidbody.position, _boxSize, 0, _diseredMovementDirection, 1f, _collisionLayerMask))
                {
                    _currentMovementDirection = _diseredMovementDirection;
                    OnDirectionChanged?.Invoke(CurrentDirection);
                }

            }
            if (Physics2D.BoxCast(_rigidbody.position, _boxSize, 0, _currentMovementDirection, 1f, _collisionLayerMask))
            {
                _currentMovementDirection = Vector2.zero;
                OnDirectionChanged?.Invoke(CurrentDirection);
            }

        }


        _rigidbody.MovePosition(_rigidbody.position + _currentMovementDirection * moveDistance);
    }


}
