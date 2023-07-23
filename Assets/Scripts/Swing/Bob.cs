using UnityEngine;

[System.Serializable]
public class Bob
{
    public Vector3 velocity;
    public float gravity = 20f;
    public Vector3 gravityDirection = new Vector3(0, 1, 0);

    private Vector3 dampingDirection;
    public float drag;

    public float maxSpeed;

    public void ApplyGravity()
    {
        velocity -= gravityDirection * gravity * Time.deltaTime;
    }

    public void ApplyDamping()
    {
        dampingDirection = -velocity;
        dampingDirection *= drag;
        velocity += dampingDirection;
    }

    public void CapMaxSpeed()
    {
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }
}
