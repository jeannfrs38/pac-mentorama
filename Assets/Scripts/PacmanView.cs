using UnityEngine;

public class PacmanView : MonoBehaviour
{
    public CharacterMotor CharacterMotor;
    public Animator Animator;
    void Start()
    {
        CharacterMotor.OnDirectionChanged += CharacterMotor_OnDirectionChanged;
    }

    private void CharacterMotor_OnDirectionChanged(Direction direction)
    {
        switch (direction)
        {
            case Direction.None:
                Animator.SetBool("Moving", false);
                break;

            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                Animator.SetBool("Moving", true);

                break;

            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                Animator.SetBool("Moving", true);

                break;

            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                Animator.SetBool("Moving", true);

                break;

            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Animator.SetBool("Moving", true);

                break;
        }
    }


}
