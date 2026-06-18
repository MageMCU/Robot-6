using UnityEngine;

namespace Algebra
{
    public class Matrix
    {
        // ROW MAJOR
        private float[] _tuples;
        // Length (max index = Length - 1)
        private int _rowsLength;
        // Length (max index = Length - 1)
        private int _colsLength;

        // TESTME[√]
        public Matrix()
        {
            _rowsLength = 2;
            // Debug.Log("_rowsLength " + _rowsLength.ToString());

            _colsLength = 2;
            // Debug.Log("_colsLength " + _colsLength.ToString());

            _tuples = new float[_rowsLength * _colsLength];
            // Debug.Log("_tuples.Length " + _tuples.Length.ToString());

            int index;
            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    _tuples[index] = 0f;
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + _tuples[index].ToString("F2"));
                }
            }
        }

        // TESTME[√]
        public Matrix(int rows, int cols)
        {
            if (rows < 1 || cols < 1)
            {
                Debug.LogError("Minimum Matrix dimension is 1 row by 1 column!");
            }
            _rowsLength = rows;
            // Debug.Log("rows " + rows.ToString());

            _colsLength = cols;
            // Debug.Log("cols " + cols.ToString());

            _tuples = new float[rows * cols];
            // Debug.Log("_tuples.Length " + _tuples.Length.ToString());

            int index;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    index = Index(i, j);
                    _tuples[index] = 0f;
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + _tuples[index].ToString("F2"));
                }
            }
        }

        // TESTME[√]
        public Matrix(int rows, int cols, params float[] a)
        {
            if (rows < 1 || cols < 1)
            {
                Debug.LogError("Minimum Matrix dimension is 1 row by 1 column!");
            }
            _rowsLength = rows;
            // Debug.Log("rows " + rows.ToString());

            _colsLength = cols;
            // Debug.Log("cols " + cols.ToString());

            _tuples = new float[rows * cols];
            // Debug.Log("_tuples.Length " + _tuples.Length.ToString());

            int index;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    index = Index(i, j);
                    _tuples[index] = a[index];
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + a[index].ToString("F2"));
                }
            }
        }

        // TESTME[√]
        public Matrix(Matrix Matrix_to_copy)
        {
            if (Matrix_to_copy == null)
                return;
            _rowsLength = Matrix_to_copy._rowsLength;
            // Debug.Log("_rowsLength " + _rowsLength.ToString());

            _colsLength = Matrix_to_copy._colsLength;
            // Debug.Log("_colsLength " + _colsLength.ToString());

            _tuples = new float[_colsLength * _rowsLength];
            // Debug.Log("_tuples.Length " + _tuples.Length.ToString());

            int index;
            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    _tuples[index] = Matrix_to_copy._tuples[index];
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + Matrix_to_copy._tuples[index].ToString("F2"));
                }
            }
        }

        // TESTME[√]
        public Matrix(Vector Vector, bool Row_Major = true)
        {
            int n = Vector.Size;
            if (Row_Major)
            {
                _rowsLength = n;
                _colsLength = 1;
            }
            else
            {
                // Column Major
                _rowsLength = 1;
                _colsLength = n;
            }
            _tuples = new float[n];

            for (int i = 0; i < n; i++)
            {
                _tuples[i] = Vector.TupleGet(i);
            }
        }

        // TESTME[√]
        public Matrix IdentityIn()
        {
            if (_rowsLength != _colsLength)
            {
                Debug.LogError("Matrix must be squared!");
                return this;
            }

            int index;
            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    if (i == j)
                    {
                        _tuples[index] = 1f;
                    }
                    else
                    {
                        _tuples[index] = 0f;
                    }
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + _tuples[index].ToString("F2"));
                }
            }
            return this;
        }

        // TESTME[√]
        public Matrix IdentityOut()
        {
            if (_rowsLength != _colsLength)
            {
                Debug.LogError("Matrix must be squared!");
                return this;
            }

            Matrix B = new Matrix(_rowsLength, _colsLength);

            int index;
            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    if (i == j)
                    {
                        B.TupleSet(index, 1f);
                    }
                    else
                    {
                        B.TupleSet(index, 0f);
                    }
                    Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + _tuples[index].ToString("F2"));
                }
            }
            return B;
        }

        // TESTME[√]
        public Matrix Transpose()
        {
            // Swap Rows & Cols
            Matrix B = new Matrix(_colsLength, _rowsLength);

            int indexIJ;

            // Loop 1
            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    indexIJ = Index(i, j);
                    B.TupleSet(j, i, _tuples[indexIJ]);

                    // FOR DEBUG PURPOSES ONLY
                    //Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + indexIJ.ToString() + "] = " + _tuples[indexIJ].ToString("F2"));
                }
            }

            // FOR DEBUG PURPOSES ONLY
            // For B.TupleSet(indexJI, _tuples[indexIJ]), the call Index(j, i) does not work... Why? Solution Use B.TupleGet(j, i) instead of its index.
            // ALSO FIX INDEX FOR - method(Matrix Argument)
            // Notice the indices for both the Use B.TupleGet(j, i) and index strings were swapped. The literals were not changed.
            //int rows = B.Rows;
            //int cols = B.Cols;
            //int size = B.Size;
            //int index;
            //Debug.Log("-------------------------------------");
            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; j < cols; j++)
            //    {
            //        index = size * rows + cols;
            //        Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + B.TupleGet(j, i).ToString("F2"));
            //    }
            //}

            return B;
        }

        // TESTME[√]
        public int Rows
        {
            get
            {
                return _rowsLength;
            }
            protected set
            {
                _rowsLength = value;
            }
        }

        // TESTME[√]
        public int Cols
        {
            get
            {
                return _colsLength;
            }
            protected set
            {
                _colsLength = value;
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
        // FIXME - by using Matrix type OR
        // have an IN & OUT....
        public int Index(int rowIndex, int colIndex)
        {
            int max = _colsLength * (_rowsLength - 1) + (_colsLength - 1);
            // Ref. 3DGEA p.50
            int index = _colsLength * rowIndex + colIndex;
            if (index < 0 || index > max)
            {
                Debug.LogError("Index ERROR!");
                return -1;
            }
            return index;
        }

        // TESTME[√]
        public float TupleGet(int rowIndex, int colIndex)
        {
            int rows = _rowsLength - 1;
            int cols = _colsLength - 1;
            int max = _colsLength * rows + cols;
            int index = Index(rowIndex, colIndex);
            if (index < 0 || index > max)
            {
                Debug.LogError("Index ERROR!");
                return -1;
            }
            return _tuples[index];
        }

        // TESTME[√]
        public float TupleGet(int index)
        {
            if (index < 0 || index > (_tuples.Length - 1))
            {
                Debug.LogError("Index ERROR!");
                return -1;
            }
            return _tuples[index];
        }

        // TESTME[√]
        public void TupleSet(int rowIndex, int colIndex, float value)
        {
            int rows = _rowsLength - 1;
            int cols = _colsLength - 1;
            int max = _colsLength * rows + cols;
            int index = Index(rowIndex, colIndex);
            if (index < 0 || index > max)
            {
                Debug.LogError("Index ERROR!");
            }
            _tuples[index] = value;
        }

        // TESTME[√]
        public void TupleSet(int index, float value)
        {
            if (index < 0 || index > (_tuples.Length - 1))
            {
                Debug.LogError("Index ERROR!");
            }
            _tuples[index] = value;
        }

        // TESTME[√]
        public void MultiplyScalarIn(float scalar)
        {
            int index;
            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    _tuples[index] *= scalar;
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + _tuples[index].ToString("F2"));
                }
            }
        }

        // TESTME[√]
        public Matrix MultiplyScalarOut(float scalar)
        {
            Matrix B = new Matrix(_rowsLength, _colsLength);
            int index;
            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = B.Index(i, j);
                    B._tuples[index] = _tuples[index] * scalar;
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + B.TupleGet(index).ToString("F2"));
                }
            }
            return B;
        }

        // TESTME[√]
        public void NegativeIn()
        {
            int index;
            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    _tuples[index] *= -1f;
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + _tuples[index].ToString("F2"));
                }
            }
        }

        // TESTME[√]
        public Matrix NegativeOut()
        {
            // Declare Matrix
            Matrix B = new Matrix(_rowsLength, _colsLength);
            float value;
            int index;

            // A & B have smae number of Rows
            for (int i = 0; i < _rowsLength; i++)
            {
                // A & B have smae number of Cols
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    // Keep A from changing
                    value = _tuples[index] * -1f;
                    // Set value to B.Tuple
                    B.TupleSet(index, value);
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + B.TupleGet(index).ToString("F2"));
                }
            }
            return B;
        }

        //  TESTME[√]
        public void AddIn(Matrix B)
        {
            int rowsA = this.Rows;
            int colsA = this.Cols;
            int rowsB = B.Rows;
            int colsB = B.Cols;
            //
            int index;
            if (rowsA != rowsB || colsA != colsB)
            {
                Debug.LogError("Matrix sizes are not same!");
                return;
            }

            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    _tuples[index] += B._tuples[index];
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + _tuples[index].ToString("F2"));
                }
            }
        }

        // TESTME[√]
        public Matrix AddOut(Matrix B)
        {
            int rowsA = this.Rows;
            int colsA = this.Cols;
            int rowsB = B.Rows;
            int colsB = B.Cols;
            //
            float value;
            int index;
            if (rowsA != rowsB || colsA != colsB)
            {
                Debug.LogError("Matrix sizes are not same!");
                return this;
            }

            Matrix C = new Matrix(_rowsLength, _colsLength);

            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    value = _tuples[index] + B._tuples[index];
                    C.TupleSet(index, value);
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + C.TupleGet(index).ToString("F2"));
                }
            }
            return C;
        }

        // TESTME[√]
        public void SubtractIn(Matrix B)
        {
            int rowsA = this.Rows;
            int colsA = this.Cols;
            int rowsB = B.Rows;
            int colsB = B.Cols;
            //
            int index;
            if (rowsA != rowsB || colsA != colsB)
            {
                Debug.LogError("Matrix sizes are not same!");
                return;
            }

            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    _tuples[index] -= B._tuples[index];
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + _tuples[index].ToString("F2"));
                }
            }
        }

        // TESTME[√]
        public Matrix SubtractOut(Matrix B)
        {
            int rowsA = this.Rows;
            int colsA = this.Cols;
            int rowsB = B.Rows;
            int colsB = B.Cols;
            //
            float value;
            int index;
            if (rowsA != rowsB || colsA != colsB)
            {
                Debug.LogError("Matrix sizes are not same!");
                return this;
            }

            Matrix C = new Matrix(_rowsLength, _colsLength);

            for (int i = 0; i < _rowsLength; i++)
            {
                for (int j = 0; j < _colsLength; j++)
                {
                    index = Index(i, j);
                    value = _tuples[index] - B._tuples[index];
                    C.TupleSet(index, value);
                    // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = a[" + index.ToString() + "] = " + C.TupleGet(index).ToString("F2"));
                }
            }
            return C;
        }

        // TESTME[√]
        // Multiplies both Square & non-square matrices.
        public Matrix Multiply(Matrix B)
        {
            // Limits A(aI, aK)
            int aI = this.Rows;
            int aK = this.Cols;
            // Limits B(bK, bJ)
            int bK = B.Rows;
            int bJ = B.Cols;
            // aK == bK
            if (aK != bK)
            {
                Debug.LogError("Multiply ERROR: k of A(ik) & k of B(kj) must be equal");
                return null;
            }
            // Debug.Log("aI: " + aI.ToString() + " aK: " + aK.ToString() + " bK: " + bK.ToString() + " bJ: " + bJ.ToString());

            int indexA;
            int indexB;
            int indexC;
            // Limits C(aK, bJ)
            Matrix C = new Matrix(aI, bJ);
            // ROWS FOR A & C (aI)
            for (int i = 0; i < aI; i++)
            {
                // COLS for B & C (bJ)
                for (int j = 0; j < bJ; j++)
                {
                    indexC = C.Index(i, j);
                    C._tuples[indexC] = 0f;
                    // Cols for A & Rows for B where(aK == bK)
                    for (int k = 0; k < aK; k++)
                    {
                        indexA = Index(i, k);
                        indexB = B.Index(k, j);

                        // THE LIMITS for C(aI,bJ), for A(aI,aK) & for B(bK,bJ)
                        C._tuples[indexC] += _tuples[indexA] * B._tuples[indexB];

                        // indexC
                        // Debug.Log("i(" + i.ToString() + ") j(" + j.ToString() + ") = C[" + indexC.ToString() + "] = " + C.TupleGet(indexC).ToString("F2"));
                        // indexA
                        // Debug.Log("i(" + i.ToString() + ") k(" + k.ToString() + ") = A[" + indexA.ToString() + "] = " + _tuples[indexA].ToString("F2"));
                        // indexB
                        // Debug.Log("k(" + k.ToString() + ") j(" + j.ToString() + ") = B[" + indexB.ToString() + "] = " + B.TupleGet(indexB).ToString("F2"));
                    }
                }
            }
            return C;
        }

        // TESTME[√]
        // TODO - Any Size Square Matrix
        public float Determinate3x3()
        {
            if (this.Rows != 3 || this.Cols != 3)
            {
                Debug.LogError("Matrix is 3 by 3");
                return 0f;
            }

            // Indices
            int i00 = Index(0, 0);
            int i01 = Index(0, 1);
            int i02 = Index(0, 2);
            int i10 = Index(1, 0);
            int i11 = Index(1, 1);
            int i12 = Index(1, 2);
            int i20 = Index(2, 0);
            int i21 = Index(2, 1);
            int i22 = Index(2, 2);

            //
            return (_tuples[i00] * _tuples[i11] * _tuples[i22] + _tuples[i01] * _tuples[i12] * _tuples[i20] +
            _tuples[i02] * _tuples[i10] * _tuples[i21] - _tuples[i20] * _tuples[i11] * _tuples[i02] -
            _tuples[i21] * _tuples[i12] * _tuples[i00] - _tuples[i22] * _tuples[i10] * _tuples[i01]);
        }

        // TODO
        public void MultiplyScalarToRowA(int rowA, int fromLeftColumn, int toRightColumn, Matrix L)
        {

        }

        // TODO
        public void MultiplyScalarToRowB(int rowB, int fromLeftColumn, int toRightColumn, Matrix L)
        {

        }

        // TODO
        public void SwapRows(int row1, int row2, ref Matrix P, ref Matrix L)
        {

        }

        // TODO - INPROCESS ---------------------------------------------------------------------------------- DO-LAST
        public Matrix LUP_Solve3x3(Matrix mL, Matrix mU, Matrix mP, Vector vb)
        {
            // Logic (Stable)
            // Ax = b, find x = inverse(A)b
            // PA = LU
            // LUx = Pb where P initially I
            // Ux = y  (1) Forward Substitution yi = {bpi[i] - SUM [j=(1)to(i-1)] L(ij)*y(j)}        ~ Mistake
            // Ly = Pb (2) Back Substituion     xi = {(yi - SUM [j=(1+1)to(n)] U(ij))*x(j)) / u(ii)} ~ Mistake

            // Column Vector
            Matrix mx = new Matrix(3, 1);
            Matrix my = new Matrix(3, 1);
            Matrix mb = new Matrix(vb);
            mP = new Matrix(3, 3).IdentityIn();
            mL = new Matrix(3, 3).IdentityIn();
            mU = new Matrix(3, 3); // Zeros

            // ??
            float bs;

            //
            int n = mL.Rows;

            // TESTME []
            // interpretation: FROM i=1 TO n
            string s1 = "";
            for (int i = 0; i < n; i++)
            {
                // Although NOT mentioned, P does play a big part...?
                // Review Text
                // Read immediate paragraph under FS p.816 -------------------- STOPPED HERE
                bs = mb.TupleGet(i);
                my.TupleSet(i, bs);
                // interpretation: FROM j=0 TO i-1
                for (int j = 0; j < i; j++)
                {
                    s1 += "(i: " + i.ToString() + " j: " + j.ToString() + ") ";
                }
            }

            // TESTME []
            // interpretation: FROM i=n DOWNTO 1
            string s2 = "";
            for (int i = (n - 1); i >= 0; i--)
            {
                // interpretation: FROM j=i+1 TO n
                for (int j = (i + 1); j < n; j++)
                {
                    s2 += "(i: " + i.ToString() + " j: " + j.ToString() + ") ";
                }
            }
            return mx;
        }

        // TODO - INPROCESS ---------------------------------------------------------------------------------- DO-FIRST
        public void LUP_Decomposition3x3(out Matrix L, out Matrix U, out Matrix P)
        {
            P = new Matrix(3, 3).IdentityIn();
            L = new Matrix(3, 3).IdentityIn();
            U = new Matrix(3, 3);

        }

        // TESTME[√]
        // LUP ??????????????
        // SOLVE ????????????
        public void LU_Decomposition3x3(out Matrix L, out Matrix U)
        {
            L = new Matrix(3, 3).IdentityIn();
            U = new Matrix(3, 3);
            int indexA1;
            int indexA2;
            int indexL;
            int indexU;
            for (int k = 0; k < 3; k++)
            {
                indexA1 = Index(k, k);
                indexU = U.Index(k, k);
                //
                U._tuples[indexU] = _tuples[indexA1];

                //
                for (int i = k + 1; i < 3; i++)
                {
                    indexA2 = Index(i, k);
                    indexL = L.Index(i, k);
                    L._tuples[indexL] = _tuples[indexA2] / _tuples[indexA1];

                    indexA2 = Index(k, i);
                    indexU = U.Index(k, i);
                    U._tuples[indexU] = _tuples[indexA2];
                }

                //
                for (int i = k + 1; i < 3; i++)
                {
                    for (int j = k + 1; j < 3; j++)
                    {
                        indexA2 = Index(i, j);
                        indexL = L.Index(i, k);
                        indexU = U.Index(k, j);
                        _tuples[indexA2] = _tuples[indexA2] - L._tuples[indexL] * U._tuples[indexU];
                    }
                }
            }
        }

        // TESTME[√]
        public void Show()
        {
            string s = "";
            // ROWS -> 1 to ROWS
            for (int i = 0; i < this.Rows; i++)
            {
                // Empty String on Column 1
                s = _tuples[Index(i, 0)].ToString();

                // COLS -> 2 to COLS
                for (int j = 1; j < this.Cols; j++)
                {
                    s += " " + _tuples[Index(i, j)].ToString();
                }
                Debug.Log(s);
            }
        }
    }
};
