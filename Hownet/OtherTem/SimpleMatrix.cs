
/* autopano-sift, Automatic panorama image creation
 * Copyright (C) 2004 -- Sebastian Nowozin
 *
 * This program is free software released under the GNU General Public
 * License, which is included in this software package (doc/LICENSE).
 */

/* SimpleMatrix.cs
 *
 * Minimal two-dimensional matrix class implementation.
 *
 * (C) Copyright 2004 -- Sebastian Nowozin (nowozin@cs.tu-berlin.de)
 */

using System;
using System.Text;


// A very simple 2-dimensional Matrix class providing some basic operations.
public class SimpleMatrix : ICloneable
{
    double[,] values;
    int xDim, yDim;
    public int XDim
    {
        get
        {
            return (xDim);
        }
    }
    public int YDim
    {
        get
        {
            return (yDim);
        }
    }

    private SimpleMatrix()
    {
    }

    public SimpleMatrix(int yDim, int xDim)
    {
        this.xDim = xDim;
        this.yDim = yDim;
        values = new double[yDim, xDim];
    }

    public virtual object Clone()
    {
        SimpleMatrix cp = new SimpleMatrix(yDim, xDim);

        for (int y = 0; y < yDim; ++y)
            for (int x = 0; x < xDim; ++x)
                cp[y, x] = values[y, x];

        return (cp);
    }

    public double this[int y, int x]
    {
        get
        {
            return (values[y, x]);
        }
        set
        {
            values[y, x] = value;
        }
    }

    static public SimpleMatrix operator *(SimpleMatrix m1, SimpleMatrix m2)
    {
        if (m1.XDim != m2.YDim)
            throw (new ArgumentException
                ("Matrixes cannot be multiplied, dimension mismatch"));

        // vanilla!
        SimpleMatrix res = new SimpleMatrix(m1.YDim, m2.XDim);
        for (int y = 0; y < m1.YDim; ++y)
        {
            for (int x = 0; x < m2.XDim; ++x)
            {
                for (int k = 0; k < m2.YDim; ++k)
                    res[y, x] += m1[y, k] * m2[k, x];
            }
        }

        return (res);
    }

    public double Dot(SimpleMatrix m)
    {
        if (yDim != m.YDim || xDim != 1 || m.XDim != 1)
            throw (new InvalidOperationException
                ("Dotproduct only possible for two equal n x 1 matrices"));

        double sum = 0.0;

        for (int y = 0; y < yDim; ++y)
            sum += values[y, 0] * m[y, 0];

        return (sum);
    }

    public void Negate()
    {
        for (int y = 0; y < YDim; ++y)
        {
            for (int x = 0; x < XDim; ++x)
            {
                values[y, x] = -values[y, x];
            }
        }
    }

    public void Inverse()
    {
        if (xDim != yDim)
            throw (new InvalidOperationException
                ("Matrix x dimension != y dimension"));

        // Shipley-Coleman inversion, from
        // http://www.geocities.com/SiliconValley/Lab/4223/fault/ach03.html
        int dim = XDim;
        for (int k = 0; k < dim; ++k)
        {
            values[k, k] = -1.0 / values[k, k];

            for (int i = 0; i < dim; ++i)
            {
                if (i != k)
                    values[i, k] *= values[k, k];
            }

            for (int i = 0; i < dim; ++i)
            {
                if (i != k)
                {
                    for (int j = 0; j < dim; ++j)
                    {
                        if (j != k)
                            values[i, j] += values[i, k] * values[k, j];
                    }
                }
            }

            for (int i = 0; i < dim; ++i)
            {
                if (i != k)
                    values[k, i] *= values[k, k];
            }

        }

        for (int i = 0; i < dim; ++i)
        {
            for (int j = 0; j < dim; ++j)
                values[i, j] = -values[i, j];
        }
    }

    // The vector 'vec' is used both for input/output purposes. As input, it
    // contains the vector v, and after this method finishes it contains x,
    // the solution in the formula
    //    this * x = v
    // This matrix might get row-swapped, too.
    public void SolveLinear(SimpleMatrix vec)
    {
        if (xDim != yDim || yDim != vec.yDim)
            throw (new InvalidOperationException
                ("Matrix not quadratic or vector dimension mismatch"));

        // Gaussian Elimination Algorithm, as described by
        // "Numerical Methods - A Software Approach", R.L. Johnston

        // Forward elimination with partial pivoting
        for (int y = 0; y < (yDim - 1); ++y)
        {

            // Searching for the largest pivot (to get "multipliers < 1.0 to
            // minimize round-off errors")
            int yMaxIndex = y;
            double yMaxValue = Math.Abs(values[y, y]);

            for (int py = y; py < yDim; ++py)
            {
                if (Math.Abs(values[py, y]) > yMaxValue)
                {
                    yMaxValue = Math.Abs(values[py, y]);
                    yMaxIndex = py;
                }
            }

            // if a larger row has been found, swap with the current one
            SwapRow(y, yMaxIndex);
            vec.SwapRow(y, yMaxIndex);

            // Now do the elimination left of the diagonal
            for (int py = y + 1; py < yDim; ++py)
            {
                // always <= 1.0
                double elimMul = values[py, y] / values[y, y];

                for (int x = 0; x < xDim; ++x)
                    values[py, x] -= elimMul * values[y, x];

                // FIXME: do we really need this?
                vec[py, 0] -= elimMul * vec[y, 0];
            }
        }

        // Back substitution
        for (int y = yDim - 1; y >= 0; --y)
        {
            double solY = vec[y, 0];

            for (int x = xDim - 1; x > y; --x)
                solY -= values[y, x] * vec[x, 0];

            vec[y, 0] = solY / values[y, y];
        }
    }

    // Swap two rows r1, r2
    private void SwapRow(int r1, int r2)
    {
        if (r1 == r2)
            return;

        for (int x = 0; x < xDim; ++x)
        {
            double temp = values[r1, x];
            values[r1, x] = values[r2, x];
            values[r2, x] = temp;
        }
    }

    public override string ToString()
    {
        StringBuilder str = new StringBuilder();

        str.Append("( ");
        for (int y = 0; y < yDim; ++y)
        {
            if (y > 0)
                str.Append("\n  ");

            for (int x = 0; x < xDim; ++x)
            {
                if (x > 0)
                    str.Append("  ");

                str.AppendFormat("{0,3}", values[y, x]);
            }
        }
        str.Append(" )");

        return (str.ToString());
    }
}

