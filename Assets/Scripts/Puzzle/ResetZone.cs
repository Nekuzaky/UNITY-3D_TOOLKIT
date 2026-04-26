using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Puzzle
{
    /// <summary>
    /// Zone that snapshots the position of registered objects on enter and
    /// restores them on ResetAll(). Useful for puzzle "back to start" buttons.
    /// </summary>
    public sealed class ResetZone : MonoBehaviour
    {
        private struct SnapshotEntry { public Transform Target; public Vector3 Position; public Quaternion Rotation; }
        private readonly List<SnapshotEntry> _snapshots = new List<SnapshotEntry>();

        public void Register(Transform t)
        {
            Debug.Assert(t != null, "[ResetZone.Register] t is null"); // R5
            if (t == null) return;
            _snapshots.Add(new SnapshotEntry { Target = t, Position = t.position, Rotation = t.rotation });
        }

        public void ResetAll()
        {
            int max = _snapshots.Count; // R2
            for (int i = 0; i < max; i++)
            {
                var s = _snapshots[i];
                if (s.Target == null) continue;
                var rb = s.Target.GetComponent<Rigidbody>();
                if (rb != null) rb.linearVelocity = Vector3.zero;
                s.Target.SetPositionAndRotation(s.Position, s.Rotation);
            }
        }
    }
}
