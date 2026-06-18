using UnityEngine;

namespace Algebra
{
    public class Vector
    {
        private float[] _tuples;

        // TESTME[√]
        public Vector()
        {
            _tuples = new float[2];
            _tuples[0] = 0f;
            _tuples[1] = 0f;
        }

        // TESTME[√] --------------------------------- NEW
        public Vector(float x, float y)
        {
            _tuples = new float[2];
            _tuples[0] = x;
            _tuples[1] = y;
        }

        // TESTME[√] --------------------------------- NEW
        public Vector(float x, float y, float z)
        {
            _tuples = new float[3];
            _tuples[0] = x;
            _tuples[1] = y;
            _tuples[2] = z;
        }

        // TESTME[√] --------------------------------- NEW
        public Vector(float x, float y, float z, float w)
        {
            _tuples = new float[4];
            _tuples[0] = x;
            _tuples[1] = y;
            _tuples[2] = z;
            _tuples[3] = w;
        }

        // TESTME[√]
        public Vector(int dimension)
        {
            if (dimension < 2)
            {
                Debug.LogError("Array requires 2 or more elements!");
                return;
            }
            _tuples = new float[dimension];
            for (int i = 0; i < dimension; i++)
            {
                _tuples[i] = 0f;
            }
        }

        // TESTME[√] --------------------------------- NEW
        public Vector(Vector b)
        {
            int size = b.Size;
            _tuples = new float[size];
            for (int i = 0; i < size; i++)
            {
                _tuples[i] = b.TupleGet(i);
            }
        }

        // TESTME[√]
        public Vector(params float[] array)
        {
            if (array == null)
            {
                Debug.LogError("Array is null!");
                return;
            }
            if (array.Length < 2)
            {
                Debug.LogError("Array requires 2 or more elements!");
                return;
            }
            int n = array.Length;
            _tuples = new float[n];
            for (int i = 0; i < n; i++)
            {
                _tuples[i] = array[i];
            }
        }

        // TESTME[√]
        public int Size
        {
            get
            {
                return _tuples.Length;
            }
        }

        // TESTME[√]
        public float TupleGet(int index)
        {
            if (index < 0 || index > (_tuples.Length - 1))
            {
                Debug.LogError("TupleGet ERROR!");
                return -1;
            }
            return _tuples[index];
        }

        // TESTME[√]
        public void TupleSet(int index, float value)
        {
            if (index < 0 || index > (_tuples.Length - 1))
            {
                Debug.LogError("TupleSet out of range!");
                return;
            }
            _tuples[index] = value;
        }

        // TESTME[√]
        public float X
        {
            get
            {
                int size = Size;
                if (_tuples == null)
                {
                    Debug.LogError("Vector is null!");
                    return /* Constant.Epsilon2; */ Mathf.Epsilon;
                }
                // size 2
                if (size < (2))
                {
                    Debug.LogError("Vector requires more than " + size.ToString() + " elements!");
                    return /* Constant.Epsilon2; */ Mathf.Epsilon;
                }
                // Tuple 0 - 1st element
                return _tuples[0];
            }
            set
            {
                int size = Size;
                if (_tuples == null)
                {
                    Debug.LogError("Vector is null!");
                    return;
                }
                // size 2
                if (size < (2))
                {
                    Debug.LogError("Vector requires more than " + size.ToString() + " elements!");
                    return;
                }
                // Tuple 0 - 1st element
                _tuples[0] = value;
            }
        }

        // TESTME[√]
        public float Y
        {
            get
            {
                int size = Size;
                if (_tuples == null)
                {
                    Debug.LogError("Vector is null!");
                    return /* Constant.Epsilon2; */ Mathf.Epsilon;
                }
                // Size 2
                if (size < (2))
                {
                    Debug.LogError("Vector requires more than " + size.ToString() + " elements!");
                    return /* Constant.Epsilon2; */ Mathf.Epsilon;
                }
                // Tuple 1 - 2nd element
                return _tuples[1];
            }
            set
            {
                int size = Size;
                if (_tuples == null)
                {
                    Debug.LogError("Vector is null!");
                    return;
                }
                // Size 2
                if (size < (2))
                {
                    Debug.LogError("Vector requires more than " + size.ToString() + " elements!");
                    return;
                }
                // Tuple 1 - 2nd element
                _tuples[1] = value;
            }
        }

        // TESTME[√]
        public float Z
        {
            get
            {
                int size = Size;
                if (_tuples == null)
                {
                    Debug.LogError("Vector is null!");
                    return /* Constant.Epsilon2; */ Mathf.Epsilon;
                }
                // Size 3
                if (size < (3))
                {
                    Debug.LogError("Vector requires more than " + size.ToString() + " elements!");
                    return /* Constant.Epsilon2; */ Mathf.Epsilon;
                }
                // Tuple 2 - 3rd element
                return _tuples[2];
            }
            set
            {
                int size = Size;
                if (_tuples == null)
                {
                    Debug.LogError("Vector is null!");
                    return;
                }
                // Size 3
                if (size < (3))
                {
                    Debug.LogError("Vector requires more than " + size.ToString() + " elements!");
                    return;
                }
                // Tuple 2 - 3rd element
                _tuples[2] = value;
            }
        }

        // TESTME[√]
        public float W
        {
            get
            {
                int size = Size;
                if (_tuples == null)
                {
                    Debug.LogError("Vector is null!");
                    return /* Constant.Epsilon2; */ Mathf.Epsilon;
                }
                // Size 4
                if (size < (4))
                {
                    Debug.LogError("Vector requires more than " + size.ToString() + " elements!");
                    return /* Constant.Epsilon2; */ Mathf.Epsilon;
                }
                // Tuple 3 - 4th element
                return _tuples[3];
            }
            set
            {
                int size = Size;
                if (_tuples == null)
                {
                    Debug.LogError("Vector is null!");
                    return;
                }
                // Size 4
                if (size < (4))
                {
                    Debug.LogError("Vector requires more than " + size.ToString() + " elements!");
                    return;
                }
                // Tuple 3 - 4th element
                _tuples[3] = value;
            }
        }

        // TESTME[√]
        public Vector I2
        {
            get
            {
                float[] array = new float[2];
                array[0] = 1f;
                array[1] = 0f;
                return new Vector(array);
            }
        }

        // TESTME[√]
        public Vector J2
        {
            get
            {
                float[] array = new float[2];
                array[0] = 0f;
                array[1] = 1f;
                return new Vector(array);
            }

        }

        // TESTME[√]
        public Vector I3
        {
            get
            {
                float[] array = new float[3];
                array[0] = 1f;
                array[1] = 0f;
                array[2] = 0f;
                return new Vector(array);
            }

        }

        // TESTME[√]
        public Vector J3
        {
            get
            {
                float[] array = new float[3];
                array[0] = 0f;
                array[1] = 1f;
                array[2] = 0f;
                return new Vector(array);
            }

        }

        // TESTME[√]
        public Vector K3
        {
            get
            {
                float[] array = new float[3];
                array[0] = 0f;
                array[1] = 0f;
                array[2] = 1f;
                return new Vector(array);
            }

        }

        // TESTME[√]
        public float Value(int index)
        {
            if (_tuples == null)
            {
                Debug.LogError("Vector is null!");
                return 0f;
            }
            if (index < 0 || index >= Size)
            {
                Debug.LogError("Index out of range!");
                return 0f;
            }
            return _tuples[index];
        }

        // TESTME[√]
        public Vector Copy
        {
            get
            {
                if (_tuples == null)
                {
                    Debug.LogError("Vector is null!");
                    return null;
                }
                int size = Size;
                Vector v = new Vector(size);
                for (int i = 0; i < size; i++)
                {
                    v._tuples[i] = _tuples[i];
                }
                return v;
            }
        }

        // TESTME[√]
        public float Magnitude()
        {
            if (_tuples == null)
            {
                Debug.LogError("Vector is null!");
                return 0f;
            }
            float sum = 0f;
            int n = _tuples.Length;
            for (int i = 0; i < n; i++)
            {
                sum += _tuples[i] * _tuples[i];
            }
            return Mathf.Sqrt(sum);
        }

        // TESTME[√]
        public void Normalize()
        {
            float magnitude = Magnitude();
            int n = _tuples.Length;
            // BUGFIX - eliminated division by zero ERROR message...
            for (int i = 0; i < n; i++)
            {
                // magnitude is always positive
                if (magnitude > /* Constant.Epsilon2 */ Mathf.Epsilon)
                {
                    _tuples[i] /= magnitude;
                }
                else
                {
                    _tuples[i] /= 0f;
                }
            }
        }

        // TESTME[√]
        public float Dot(Vector b)
        {
            float sum = 0f;
            int n = b._tuples.Length;
            if (n == _tuples.Length)
            {
                for (int i = 0; i < n; i++)
                {
                    sum += _tuples[i] * b._tuples[i];
                }

            }
            return sum;
        }

        // TESTME[√]
        public Vector Cross3x3(Vector b)
        {
            int n = b._tuples.Length;
            int _n = _tuples.Length;
            float[] array = new float[3];
            if (n == _n && n == 3)
            {
                array[0] = this.Y * b.Z - this.Z * b.Y;
                array[1] = this.Z * b.X - this.X * b.Z;
                array[2] = this.X * b.Y - this.Y * b.X;
                return new Vector(array);
            }
            else
            {
                Debug.LogError("Vectors not 3x3 dimensionally aligned!");
            }
            return null;
        }

        // TESTME[√]
        public float CrossZ(Vector b)
        {
            int n = b.Size;
            int _n = Size;
            // BUGFIX - include both vectors with 2 & 3 elements.
            if (n == _n && n > 1)
            {
                return (this.X * b.Y - this.Y * b.X);
            }
            else
            {
                Debug.LogError("Vectors requires vectors dimensionally aligned with no more than 3 elements!");
            }
            return 0f;
        }

        // TESTME[]
        public void MultiplyIn(float scalar)
        {
            int n = _tuples.Length;
            for (int i = 0; i < n; i++)
            {
                _tuples[i] *= scalar;
            }
        }

        // TESTME[]
        public Vector MultiplyOut(float scalar)
        {
            int n = _tuples.Length;
            float[] array = new float[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = _tuples[i] * scalar;
            }
            return new Vector(array);
        }

        // TESTME[]
        public void AddIn(Vector b)
        {
            int n = b._tuples.Length;
            int _n = _tuples.Length;
            if (n == _n)
            {
                for (int i = 0; i < n; i++)
                {
                    _tuples[i] += b._tuples[i];
                }
            }
        }

        // TESTME[]
        public Vector AddOut(Vector b)
        {
            int n = b._tuples.Length;
            int _n = _tuples.Length;
            float[] array;
            if (n == _n)
            {
                array = new float[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = _tuples[i] + b._tuples[i];
                }
                return new Vector(array);
            }

            return null;
        }

        // TESTME[]
        public void SubtractIn(Vector b)
        {
            int n = b._tuples.Length;
            int _n = _tuples.Length;
            if (n == _n)
            {
                for (int i = 0; i < n; i++)
                {
                    _tuples[i] -= b._tuples[i];
                }
            }
        }

        // TESTME[]
        public Vector SubtractOut(Vector b)
        {
            int n = b._tuples.Length;
            int _n = _tuples.Length;
            float[] array;
            if (n == _n)
            {
                array = new float[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = _tuples[i] - b._tuples[i];
                }
                return new Vector(array);
            }

            return null;
        }

        // TESTME[]
        public void RandomUnitVectorIn2()
        {
            // Randomize Goal Vector
            float x = Random.Range(-1f, 1f);
            this.X = x;
            this.Y = (Mathf.Sqrt(1 - (x * x))) * (Mathf.Sign(Random.Range(-1f, 1f)));
        }

        // TESTME[]
        public Vector RandomUnitVectorOut2()
        {
            Vector heading = new Vector(2);
            // Randomize Gola Vector
            heading = new Vector(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            heading.Normalize();
            return heading;
        }

        // TESTME[]
        public Vector RandomUnitVectorXY0()
        {
            Vector heading = new Vector(3);
            // Randomize Gola Vector
            heading = new Vector(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            heading.Normalize();
            return heading;
        }

        // TESTME[]
        public Vector RandomUnitVectorXYZ()
        {
            Vector heading = new Vector(3);
            // Randomize Gola Vector
            heading = new Vector(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            heading.Normalize();
            return heading;
        }

        // TESTME[√]
        public void Show()
        {
            int n = _tuples.Length;
            string s = _tuples[0].ToString();
            for (int i = 1; i < n; i++)
            {
                s += ", " + _tuples[i].ToString();
            }
            s += " This is neither a ROW nor a COL Vector. Try using the Matrix_f Class!";
            Debug.Log(s);
        }
    }
};
