using UnityEngine;

[System.Serializable]
public class Pendulum
{
    public Transform bobTr;
    public Bob bob;
    public Arm arm;
    public Tether tether;

    private Vector3 previousPos;
    public void Initialize()
    {
        bobTr.transform.parent = tether.tetherTr.transform;
        arm.length = Vector3.Distance(bobTr.transform.localPosition, tether.pos);
    }

    public Vector3 MoveBob(Vector3 pos, float time)
    {
        bob.velocity += GetConstrainedVelocity(pos, previousPos, time);
            
        bob.ApplyGravity();
        bob.ApplyDamping();
        bob.CapMaxSpeed();
        
        pos += bob.velocity * time;

        if (Vector3.Distance(pos, tether.pos) < arm.length)
        {
            pos = Vector3.Normalize(pos - tether.pos) * arm.length;
            arm.length = Vector3.Distance(pos, tether.pos);
            return pos;
        }

        previousPos = pos;
        
        return pos;
    }
    
    public Vector3 MoveBob(Vector3 pos, Vector3 prevPos, float time)
    {
        bob.velocity += GetConstrainedVelocity(pos, prevPos, time);
            
        bob.ApplyGravity();
        bob.ApplyDamping();
        bob.CapMaxSpeed();
        
        pos += bob.velocity * time;

        if (Vector3.Distance(pos, tether.pos) < arm.length)
        {
            pos = Vector3.Normalize(pos - tether.pos) * arm.length;
            arm.length = Vector3.Distance(pos, tether.pos);
            return pos;
        }

        previousPos = pos;
        
        return pos;
    }

    public Vector3 GetConstrainedVelocity(Vector3 currentPos, Vector3 previousPos, float time)
    {
        float distanceToTether;
        Vector3 constrainedPos;
        Vector3 predictedPos;

        distanceToTether = Vector3.Distance(currentPos, tether.pos);
        
        if (distanceToTether > arm.length)
        {
            constrainedPos = Vector3.Normalize(currentPos - tether.pos) * arm.length;
            predictedPos = (constrainedPos - previousPos) / time;
            return predictedPos;
        }

        return Vector3.zero;
    }

    public void SwitchTether(Vector3 newPos)
    {
        bobTr.transform.parent = null;
        tether.tetherTr.position = newPos;
        bobTr.transform.parent = tether.tetherTr;
        tether.pos = tether.tetherTr.InverseTransformPoint(newPos);
        arm.length = Vector3.Distance(bobTr.transform.localPosition,tether.pos);

    }

    public Vector3 Fall(Vector3 pos, float time)
    {
        bob.ApplyGravity();
        bob.ApplyDamping();
        bob.CapMaxSpeed();
        
        pos += bob.velocity * time;

        return pos;
    }
}
