using System;

namespace ComplexNumbers
{
    public struct DecimalComplex
    {
        public DecimalComplex(decimal value) : this(value, 0) { }

        public DecimalComplex(decimal real, decimal imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        static ulong fact(ulong v)
        {
            if (v == 0)
                return 1;
            if (v < 0)
                return 0;
            for (ulong i = v - 1; i > 1; --i)
                v *= i;
            return v;
        }

        static Tuple<int, int> frac(decimal v)
        {
            int e = 0;
            while (v % 1 != 0)
            {
                v *= 10;
                ++e;
            }
            int n = (int)v, d = (int)pwr(10, e);
            while (n % 2 == 0 && d % 2 == 0)
            {
                n /= 2;
                d /= 2;
            }
            while (n % 5 == 0 && d % 5 == 0)
            {
                n /= 5;
                d /= 5;
            }
            return Tuple.Create(n, d);
        }

        static decimal pwr(decimal b, decimal e)
        {
            if (b == 0)
                return 0;
            if (e == 0)
                return 1;
            if (e == 1)
                return b;
            if (e == -1)
                return 1 / b;
            if (e < 0)
                return 1 / pwr(b, -e);
            decimal c = 1;
            if (e % 1 == 0)
            {
                if (e > 0)
                    for (int j = 0; j < e; ++j)
                        c *= b;
                else
                    for (int j = 0; j < -e; ++j)
                        c /= b;
                return c;
            }
            var f = frac(Math.Abs(e));
            return root(pwr(b, f.Item1), f.Item2) * Math.Sign(e);
        }

        static decimal root(decimal v, int n)
        {
            if (v == 0)
                return 0;
            if (v == 1)
                return 1;
            int d = 0;
            if (v > 1)
                while (pwr(10, n * ++d) < v) ;
            else
                while (pwr(10, n * --d) > v) ;
            decimal _ = 0;
            decimal c = pwr(10, d - Math.Sign(d));
            for (int i = 0; i < 29; ++i)
            {
                while (pwr(_, n) <= v)
                    _ += c;
                _ -= c;
                if (pwr(_, n) == v)
                    break;
                c /= 10;
            }
            return _;
        }

        static decimal log(decimal v, decimal b)
        {
            int d = 0;
            if (v > 1)
                while (pwr(b, pwr(10, ++d)) < v) ;
            else
                while (pwr(b, pwr(10, --d)) > v) ;
            decimal _ = 0;
            decimal c = pwr(10, d - 1);
            for (int i = 0; i < 29; ++i)
            {
                while (pwr(b, _) < v)
                    _ += c;
                _ -= c;
                if (pwr(b, _) == v)
                    break;
                c /= 10;
            }
            return _;
        }

        static decimal pi => (decimal)Math.PI;

        static decimal _e => (decimal)Math.E;

        static decimal sin(decimal v) => v - v * v * v / 6 + v * v * v * v * v / 120 - v * v * v * v * v * v * v / 5040 + v * v * v * v * v * v * v * v * v / 362880 - v * v * v * v * v * v * v * v * v * v * v / 39916800 + v * v * v * v * v * v * v * v * v * v * v * v * v / 6227020800 - v * v * v * v * v * v * v * v * v * v * v * v * v * v * v / 1307674368000 + v * v * v * v * v * v * v * v * v * v * v * v * v * v * v * v * v / 355687428096000;

        static decimal cos(decimal v) => 1 - v * v / 2 + v * v * v * v / 24 - v * v * v * v * v * v / 720 + v * v * v * v * v * v * v * v / 40320 - v * v * v * v * v * v * v * v * v * v / 3628800 + v * v * v * v * v * v * v * v * v * v * v * v / 479001600 - v * v * v * v * v * v * v * v * v * v * v * v * v * v / 87178291200 + v * v * v * v * v * v * v * v * v * v * v * v * v * v * v * v / 20922789888000;

        static decimal tan(decimal v) => sin(v) / cos(v);

        static decimal asin(decimal v) => atan(v / root(1 - v * v, 2));

        static decimal acos(decimal v) => pi / 2 - root(v / root(1 - v * v, 2), 2);

        static decimal atan(decimal v) => Math.Abs(v) > 1 ? pi / -atan(1 / v) : v - v * v * v / 3 + v * v * v * v * v / 5 - v * v * v * v * v * v * v / 7 + v * v * v * v * v * v * v * v * v / 9 - v * v * v * v * v * v * v * v * v * v * v / 11 + v * v * v * v * v * v * v * v * v * v * v * v * v / 13 - v * v * v * v * v * v * v * v * v * v * v * v * v * v * v / 15 + v * v * v * v * v * v * v * v * v * v * v * v * v * v * v * v * v / 17;

        static decimal atan2(decimal y, decimal x)
        {
            if (x > 0)
                return atan(y / x);
            if (x < 0)
            {
                if (y >= 0)
                    return atan(y / x) + pi;
                else
                    return atan(y / x) - pi;
            }
            else
            {
                if (y > 0)
                    return pi / 2;
                if (y < 0)
                    return -pi / 2;
            }
            throw new DivideByZeroException();
        }

        static decimal sin(decimal v, ushort n)
        {
            decimal r = 0;
            for (uint i = 0; i < n; ++i)
            {
                decimal c = 1;
                for (int j = 0; j < 4 * i + 1; ++j)
                    c *= v / (j + 1);
                r += c;
                c = 1;
                for (int j = 0; j < 4 * i + 3; ++j)
                    c *= v / (j + 1);
                r -= c;
            }
            return r;
        }

        static decimal cos(decimal v, ushort n)
        {
            decimal r = 0;
            for (uint i = 0; i < n; ++i)
            {
                decimal c = 1;
                for (int j = 0; j < 4 * i; ++j)
                    c *= v / (j + 1);
                r += c;
                c = 1;
                for (int j = 0; j < 4 * i + 2; ++j)
                    c *= v / (j + 1);
                r -= c;
            }
            return r;
        }

        static decimal tan(decimal v, ushort n) => sin(v, n) / cos(v, n);

        static decimal asin(decimal v, ushort n) => atan(v / root(1 - v * v, 2), n);

        static decimal atan(decimal v, ushort n)
        {
            if (Math.Abs(v) > 1)
                return pi / -atan(1 / v, n);
            decimal r = 0;
            for (uint i = 0; i < n; ++i)
                r += pwr(v, 4 * i + 1) / (4 * i + 1) - pwr(v, 4 * i + 3) / (4 * i + 3);
            return r;
        }

        static decimal atan2(decimal y, decimal x, ushort n)
        {
            if (x > 0)
                return atan(y / x, n);
            if (x < 0)
            {
                if (y >= 0)
                    return atan(y / x, n) + pi;
                else
                    return atan(y / x, n) - pi;
            }
            else
            {
                if (y > 0)
                    return pi / 2;
                if (y < 0)
                    return -pi / 2;
            }
            throw new DivideByZeroException();
        }

        public static DecimalComplex Zero => 0;

        public static DecimalComplex One => 1;

        public decimal Real { get; set; }

        public decimal Imaginary { get; set; }

        public decimal Magnitude => root(Real * Real + Imaginary * Imaginary, 2);

        public decimal Phase => atan2(Imaginary, Real, 100);

        public static implicit operator DecimalComplex(decimal value) => new DecimalComplex(value);

        public override bool Equals(object obj) => obj is DecimalComplex && (DecimalComplex)obj == this;

        public override int GetHashCode() => Real.GetHashCode() ^ Imaginary.GetHashCode();

        public override string ToString() => Imaginary == 0 ? Real.ToString() : Real == 0 ? (Math.Abs(Imaginary) == 1 ? Imaginary == 1 ? "" : "-" : Imaginary.ToString()) + "i" : $"{Real} + {Imaginary}i";

        public static bool operator ==(DecimalComplex left, DecimalComplex right) => left.Real == right.Real && left.Imaginary == right.Imaginary;

        public static bool operator !=(DecimalComplex left, DecimalComplex right) => !(left == right);

        public static DecimalComplex operator +(DecimalComplex left, DecimalComplex right) => new DecimalComplex(left.Real + right.Real, left.Imaginary + right.Imaginary);

        public static DecimalComplex operator -(DecimalComplex left, DecimalComplex right) => new DecimalComplex(left.Real - right.Real, left.Imaginary - right.Imaginary);

        public static DecimalComplex operator *(DecimalComplex left, DecimalComplex right) => new DecimalComplex(left.Real * right.Real - left.Imaginary * right.Imaginary, left.Real * right.Imaginary + left.Imaginary * right.Real);

        public static DecimalComplex operator /(DecimalComplex left, DecimalComplex right)
        {
            decimal a = left.Real, b = left.Imaginary, c = right.Real, d = right.Imaginary;
            if (Math.Abs(d) < Math.Abs(c))
            {
                decimal doc = d / c;
                return new DecimalComplex((a + b * doc) / (c + d * doc), (b - a * doc) / (c + d * doc));
            }
            else
            {
                decimal cod = c / d;
                return new DecimalComplex((b + a * cod) / (d + c * cod), (-a + b * cod) / (d + c * cod));
            }
        }

        public static DecimalComplex FromPolar(decimal magnitude, decimal phase) => new DecimalComplex(cos(phase, 100) * magnitude, sin(phase, 100) * magnitude);

        public static DecimalComplex Sqrt(DecimalComplex v)
        {
            if (v.Imaginary == 0)
            {
                if (v.Real > 0)
                    return root(v.Real, 2);
                if (v.Real < 0)
                    return new DecimalComplex(0, root(-v.Real, 2));
                return 0;
            }
            bool invert = v.Imaginary < 0;
            decimal m = v.Magnitude;
            decimal q = root(m, 2);
            v /= q;
            v.Real += (q - v.Real) / 2;
            v.Imaginary = v.Imaginary / 2;
            v *= m / v.Magnitude;
            if (invert)
                v *= -1;
            return v;
        }

        public static DecimalComplex Power(DecimalComplex v, DecimalComplex e)
        {
            if (e == 0)
                return 1;
            if (v == 0)
                return 0;
            if (e.Imaginary == 0)
            {
                if (v.Imaginary == 0 && v.Real > 0)
                    return pwr(v.Real, e.Real);
                decimal a = v.Real, b = v.Imaginary, m = v.Magnitude;
                var f = frac(e.Real);
                DecimalComplex r = 1;
                for (int i = 0; i < f.Item1; ++i)
                    r *= v;
                int d = f.Item2;
                while (d % 2 == 0)
                {
                    d /= 2;
                    r = Sqrt(r);
                }
                return FromPolar(root(r.Magnitude, d), r.Phase / d);
            }
            return Power(v, e.Real) * (cos(e.Imaginary, 100) + new DecimalComplex(0, 1) * sin(e.Imaginary, 100));
        }
    }
}