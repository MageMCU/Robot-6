
using UnityEngine;

namespace Algebra
{

    // --------------------------------------------------
    // ----------------------------------------- CONSTANT
    // --------------------------------------------------
    public class Kinematics : Matrix
    {
        public Kinematics() { }

        // Transform Right-Hand to Left-Hand
        // Requires Further Study
        public Matrix RH_LH_Transform()
        {
            // Instaniate Array
            float[] _tuples = new float[9];

            // Assign Matrox with zeros
            for (int i = 0; i < 9; i++)
                _tuples[i] = 0f;

            // Assign Specific elements
            // 1 0 0
            // 0 0 1
            // 0 1 0
            _tuples[0] = 1f;
            _tuples[5] = 1f;
            _tuples[7] = 1f;

            return new Matrix(3, 3, _tuples);
        }

        // TODO: RotationZ_Transform is a future stub — body not yet implemented.
        public Matrix RotationZ_Transform()
        {
            // Instaniate Array
            float[] _tuples = new float[9];

            // Assign Matrox with zeros
            for (int i = 0; i < 9; i++)
                _tuples[i] = 0f;

            // Assign Specific elements
            // 1 0 0
            // 0 0 1
            // 0 1 0
            // _tuples[0] = 1f;
            // _tuples[5] = 1f;
            // _tuples[7] = 1f;

            return new Matrix(3, 3, _tuples);
        }
    }
};