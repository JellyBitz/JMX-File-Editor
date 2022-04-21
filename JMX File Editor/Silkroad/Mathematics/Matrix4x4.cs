using System;

namespace JMXFileEditor.Silkroad.Mathematics
{
    /// <summary>
    /// Matrix on 3D space representation
    /// </summary>
    public class /*this should really be a struct*/ Matrix4x4 : IEquatable<Matrix4x4>
    {
        public static readonly Matrix4x4 Identity = new Matrix4x4(1.0f, 0.0f, 0.0f, 0.0f,
                                                                  0.0f, 1.0f, 0.0f, 0.0f,
                                                                  0.0f, 0.0f, 1.0f, 0.0f,
                                                                  0.0f, 0.0f, 0.0f, 1.0f);

        public float M11 { get; set; }
        public float M12 { get; set; }
        public float M13 { get; set; }
        public float M14 { get; set; }
        public float M21 { get; set; }
        public float M22 { get; set; }
        public float M23 { get; set; }
        public float M24 { get; set; }
        public float M31 { get; set; }
        public float M32 { get; set; }
        public float M33 { get; set; }
        public float M34 { get; set; }
        public float M41 { get; set; }
        public float M42 { get; set; }
        public float M43 { get; set; }
        public float M44 { get; set; }

        public Matrix4x4()
        {
        }

        public Matrix4x4(float m11, float m12, float m13, float m14,
                         float m21, float m22, float m23, float m24,
                         float m31, float m32, float m33, float m34,
                         float m41, float m42, float m43, float m44)
        {
            this.M11 = m11; this.M12 = m12; this.M13 = m13; this.M14 = m14;
            this.M21 = m21; this.M22 = m22; this.M23 = m23; this.M24 = m24;
            this.M31 = m31; this.M32 = m32; this.M33 = m33; this.M34 = m34;
            this.M41 = m41; this.M42 = m42; this.M43 = m43; this.M44 = m44;
        }

        public Matrix4x4(Matrix4x4 other)
        {
            this.M11 = other.M11; this.M12 = other.M12; this.M13 = other.M13; this.M14 = other.M14;
            this.M21 = other.M21; this.M22 = other.M22; this.M23 = other.M23; this.M24 = other.M24;
            this.M31 = other.M31; this.M32 = other.M32; this.M33 = other.M33; this.M34 = other.M34;
            this.M41 = other.M41; this.M42 = other.M42; this.M43 = other.M43; this.M44 = other.M44;
        }

        public static Matrix4x4 CreateTranslation(float x, float y, float z)
        {
            return new Matrix4x4(Identity)
            {
                M41 = x,
                M42 = y,
                M43 = z
            };
        }

        public static Matrix4x4 CreateTranslation(Vector3 value)
        {
            return new Matrix4x4(Identity)
            {
                M41 = value.X,
                M42 = value.Y,
                M43 = value.Z
            };
        }

        public static Matrix4x4 operator *(Matrix4x4 left, Matrix4x4 right)
        {
            return new Matrix4x4
            {
                M11 = (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31) + (left.M14 * right.M41),
                M12 = (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32) + (left.M14 * right.M42),
                M13 = (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33) + (left.M14 * right.M43),
                M14 = (left.M11 * right.M14) + (left.M12 * right.M24) + (left.M13 * right.M34) + (left.M14 * right.M44),

                M21 = (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31) + (left.M24 * right.M41),
                M22 = (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32) + (left.M24 * right.M42),
                M23 = (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33) + (left.M24 * right.M43),
                M24 = (left.M21 * right.M14) + (left.M22 * right.M24) + (left.M23 * right.M34) + (left.M24 * right.M44),

                M31 = (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31) + (left.M34 * right.M41),
                M32 = (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32) + (left.M34 * right.M42),
                M33 = (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33) + (left.M34 * right.M43),
                M34 = (left.M31 * right.M14) + (left.M32 * right.M24) + (left.M33 * right.M34) + (left.M34 * right.M44),

                M41 = (left.M41 * right.M11) + (left.M42 * right.M21) + (left.M43 * right.M31) + (left.M44 * right.M41),
                M42 = (left.M41 * right.M12) + (left.M42 * right.M22) + (left.M43 * right.M32) + (left.M44 * right.M42),
                M43 = (left.M41 * right.M13) + (left.M42 * right.M23) + (left.M43 * right.M33) + (left.M44 * right.M43),
                M44 = (left.M41 * right.M14) + (left.M42 * right.M24) + (left.M43 * right.M34) + (left.M44 * right.M44)
            };
        }

        public static Matrix4x4 operator -(Matrix4x4 left)
        {
            return new Matrix4x4
            {
                M11 = -left.M11,
                M12 = -left.M12,
                M13 = -left.M13,
                M14 = -left.M14,
                M21 = -left.M21,
                M22 = -left.M22,
                M23 = -left.M23,
                M24 = -left.M24,
                M31 = -left.M31,
                M32 = -left.M32,
                M33 = -left.M33,
                M34 = -left.M34,
                M41 = -left.M41,
                M42 = -left.M42,
                M43 = -left.M43,
                M44 = -left.M44
            };
        }

        public static Matrix4x4 operator -(Matrix4x4 left, Matrix4x4 right)
        {
            return new Matrix4x4
            {
                M11 = left.M11 - right.M11,
                M12 = left.M12 - right.M12,
                M13 = left.M13 - right.M13,
                M14 = left.M14 - right.M14,
                M21 = left.M21 - right.M21,
                M22 = left.M22 - right.M22,
                M23 = left.M23 - right.M23,
                M24 = left.M24 - right.M24,
                M31 = left.M31 - right.M31,
                M32 = left.M32 - right.M32,
                M33 = left.M33 - right.M33,
                M34 = left.M34 - right.M34,
                M41 = left.M41 - right.M41,
                M42 = left.M42 - right.M42,
                M43 = left.M43 - right.M43,
                M44 = left.M44 - right.M44
            };
        }

        public static Matrix4x4 operator *(Matrix4x4 left, float right)
        {
            return new Matrix4x4
            {
                M11 = left.M11 * right,
                M12 = left.M12 * right,
                M13 = left.M13 * right,
                M14 = left.M14 * right,
                M21 = left.M21 * right,
                M22 = left.M22 * right,
                M23 = left.M23 * right,
                M24 = left.M24 * right,
                M31 = left.M31 * right,
                M32 = left.M32 * right,
                M33 = left.M33 * right,
                M34 = left.M34 * right,
                M41 = left.M41 * right,
                M42 = left.M42 * right,
                M43 = left.M43 * right,
                M44 = left.M44 * right
            };
        }

        public static bool operator ==(Matrix4x4 left, Matrix4x4 right)
        {
            return left.M11 == right.M11 || left.M12 == right.M12 || left.M13 == right.M13 || left.M14 == right.M14 ||
                   left.M21 == right.M21 || left.M22 == right.M22 || left.M23 == right.M23 || left.M24 == right.M24 ||
                   left.M31 == right.M31 || left.M32 == right.M32 || left.M33 == right.M33 || left.M34 == right.M34 ||
                   left.M41 == right.M41 || left.M42 == right.M42 || left.M43 == right.M43 || left.M44 == right.M44;
        }

        public static bool operator !=(Matrix4x4 left, Matrix4x4 right)
        {
            return left.M11 != right.M11 || left.M12 != right.M12 || left.M13 != right.M13 || left.M14 != right.M14 ||
                   left.M21 != right.M21 || left.M22 != right.M22 || left.M23 != right.M23 || left.M24 != right.M24 ||
                   left.M31 != right.M31 || left.M32 != right.M32 || left.M33 != right.M33 || left.M34 != right.M34 ||
                   left.M41 != right.M41 || left.M42 != right.M42 || left.M43 != right.M43 || left.M44 != right.M44;
        }

        public bool Equals(Matrix4x4 other) => this == other;

        public override bool Equals(object obj) => (obj is Matrix4x4 other) && this.Equals(other);

        public override int GetHashCode()
        {
            int hashCode = -1955208504;
            hashCode = hashCode * -1521134295 + this.M11.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M12.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M13.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M14.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M21.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M22.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M23.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M24.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M31.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M32.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M33.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M34.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M41.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M42.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M43.GetHashCode();
            hashCode = hashCode * -1521134295 + this.M44.GetHashCode();
            return hashCode;
        }
    }
}