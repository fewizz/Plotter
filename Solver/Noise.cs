using OpenGL;
using System;
using static System.Math;

namespace Plotter
{
    public static class VE {
        public static Vertex2f add(this Vertex2f v, float f) => new Vertex2f(v.x + f, v.y + f);
        public static Vertex2f add(this Vertex2f v, Vertex2f f) => new Vertex2f(v.x + f.x, v.y + f.y);
        public static Vertex3f add(this Vertex3f v, Vertex3f f) => new Vertex3f(v.x + f.x, v.y + f.y, v.z + f.z);
        public static Vertex3f add(this Vertex3f v, float f) => new Vertex3f(v.x + f, v.y + f, v.z + f);
        public static Vertex4f add(this Vertex4f v, float f) => new Vertex4f(v.x + f, v.y + f, v.z + f, v.w + f);
        public static Vertex4f add(this Vertex4f v, Vertex4f f) => new Vertex4f(v.x + f.x, v.y + f.y, v.z + f.z, v.w + f.w);

        public static Vertex3f sub(this Vertex3f v, float f)
            => new Vertex3f(v.x - f, v.y - f, v.z - f);
        public static Vertex3f sub(this float f, Vertex3f v)
            => new Vertex3f(f - v.x, f - v.y, f - v.z);
        public static Vertex4f sub(this float f, Vertex4f v)
            => new Vertex4f(f - v.x, f - v.y, f - v.z, f - v.w);
        public static Vertex2f sub(this Vertex2f f, Vertex2f v)
            => new Vertex2f(f.x - v.x, f.y - v.y);
        public static Vertex3f sub(this Vertex3f f, Vertex3f v)
            => new Vertex3f(f.x - v.x, f.y - v.y, f.z - v.z);
        public static Vertex4f sub(this Vertex4f f, Vertex4f v)
            => new Vertex4f(f.x - v.x, f.y - v.y, f.z - v.z, f.w - v.w);

        public static Vertex4f div(this Vertex4f v, float f) => new Vertex4f(v.x / f, v.y / f, v.z / f, v.w / f);

        public static Vertex4f sub_xy(this Vertex4f v, Vertex2f f) {
            v.x -= f.x;
            v.y -= f.y;
            return v;
        }

        /*public static void a_mul(this Vertex3f v, Vertex3f f)
        {
            v.x *= f.x;
            v.y *= f.y;
            v.z *= f.z;
        }*/

        public static Vertex3f a_yz(this Vertex3f v, Vertex2f v0)
        {
            v.y = v0.x;
            v.z = v0.y;
            return v;
        }

        public static Vertex4f mul(this Vertex4f v, float f) => new Vertex4f(v.x * f, v.y * f, v.z * f, v.w * f);
        public static Vertex2f mul(this Vertex2f v0, Vertex2f v1)
            => new Vertex2f(v0.x * v1.x, v0.y * v1.y);
        public static Vertex3f mul(this Vertex3f v0, Vertex3f v1)
            => new Vertex3f(v0.x * v1.x, v0.y * v1.y, v0.z * v1.z);
        public static Vertex4f mul(this Vertex4f v0, Vertex4f v1)
            => new Vertex4f(v0.x * v1.x, v0.y * v1.y, v0.z * v1.z, v0.w * v1.w);

        public static Vertex3f xxx(this Vertex2f v) => new Vertex3f(v.x, v.x, v.x);
        public static Vertex3f yyy(this Vertex2f v) => new Vertex3f(v.y, v.y, v.y);
        public static Vertex4f xyxy(this Vertex2f v) => new Vertex4f(v.x, v.y, v.x, v.y);
        public static Vertex2f yz(this Vertex3f v) => new Vertex2f(v.y, v.z);
        public static Vertex3f yzx(this Vertex3f v) => new Vertex3f(v.y, v.z, v.x);
        public static Vertex3f xyz(this Vertex3f v) => new Vertex3f(v.x, v.y, v.z);
        public static Vertex3f zxy(this Vertex3f v) => new Vertex3f(v.z, v.x, v.y);
        public static Vertex4f yyyy(this Vertex3f v) => new Vertex4f(v.y, v.y, v.y, v.y);
        public static Vertex4f xxzz(this Vertex4f v) => new Vertex4f(v.x, v.x, v.z, v.z);
        public static Vertex4f xxyy(this Vertex4f v) => new Vertex4f(v.x, v.x, v.y, v.y);
        public static Vertex4f zzww(this Vertex4f v) => new Vertex4f(v.z, v.z, v.w, v.w);
        public static Vertex4f xzyw(this Vertex4f v) => new Vertex4f(v.x, v.z, v.y, v.w);
        public static Vertex2f xy(this Vertex4f v) => new Vertex2f(v.x, v.y);
        public static Vertex2f zw(this Vertex4f v) => new Vertex2f(v.z, v.w);
        public static Vertex3f www(this Vertex4f v) => new Vertex3f(v.w, v.w, v.w);
        public static Vertex3f wyz(this Vertex4f v) => new Vertex3f(v.w, v.y, v.z);
        public static Vertex3f xzx(this Vertex4f v) => new Vertex3f(v.x, v.z, v.x);
        public static Vertex2f yz(this Vertex4f v) => new Vertex2f(v.y, v.z);
        public static Vertex2f yw(this Vertex4f v) => new Vertex2f(v.y, v.w);
        public static Vertex2f xz(this Vertex4f v) => new Vertex2f(v.x, v.z);
        public static Vertex2f xx(this Vertex4f v) => new Vertex2f(v.x, v.x);
        public static Vertex2f yy(this Vertex4f v) => new Vertex2f(v.y, v.y);
    }
    class Noise
    {
        static Vertex2f floor(Vertex2f v) => new Vertex2f((float)Floor(v.x), (float)Floor(v.y));
        static Vertex3f floor(Vertex3f v) => new Vertex3f((float)Floor(v.x), (float)Floor(v.y), (float)Floor(v.z));
        static Vertex4f floor(Vertex4f v) => new Vertex4f((float)Floor(v.x), (float)Floor(v.y), (float)Floor(v.z), (float)Floor(v.w));

        static Vertex2f mod(Vertex2f x, float y) => x.sub(floor(x / y) * y);
        static Vertex3f mod(Vertex3f x, float y) => x.sub(floor(x / y) * y);
        static Vertex4f mod(Vertex4f x, float y) => x.sub(floor(x.div(y)).mul(y));

        static Vertex3f fract(Vertex3f v) => v - floor(v);

        static Vertex3f max(Vertex3f x, float y) =>
            new Vertex3f(Max(x.x, y), Max(x.y, y), Max(x.z, y));
        static Vertex4f max(Vertex4f x, float y) =>
            new Vertex4f(Max(x.x, y), Max(x.y, y), Max(x.z, y), Max(x.w, y));
        static Vertex3f max(Vertex3f x, Vertex3f y) =>
            new Vertex3f(Max(x.x, y.x), Max(x.y, y.y), Max(x.z, y.z));
        static Vertex4f max(Vertex4f x, Vertex4f y) =>
            new Vertex4f(Max(x.x, y.x), Max(x.y, y.y), Max(x.z, y.z), Max(x.w, y.w));
        static Vertex3f min(Vertex3f x, Vertex3f y) =>
            new Vertex3f(Min(x.x, y.x), Min(x.y, y.y), Min(x.z, y.z));

        static Vertex3f abs(Vertex3f x) => new Vertex3f(Abs(x.x), Abs(x.y), Abs(x.z));

        static Vertex4f abs(Vertex4f x) => new Vertex4f(Abs(x.x), Abs(x.y), Abs(x.z), Abs(x.w));

        static Vertex3f permute(Vertex3f x) {
            return mod(
                (x * 34.0F).add(1.0F).mul(x),
                289.0F
            );
        }

        static float dot(Vertex2f v0, Vertex2f v1) => v0.x * v1.x + v0.y * v1.y;
        static float dot(Vertex3f v0, Vertex3f v1) => v0.x * v1.x + v0.y * v1.y + v0.z * v1.z;
        static float dot(Vertex4f v0, Vertex4f v1) => v0.x * v1.x + v0.y * v1.y + v0.z * v1.z + v0.w * v1.w;

        static Vertex3f step(Vertex3f edge, Vertex3f x)
        {
            Vertex3f t = x - edge;
            return new Vertex3f(t.x < 0 ? 0 : 1, t.y < 0 ? 0 : 1, t.z < 0 ? 0 : 1);
        }

        static Vertex4f step(Vertex4f edge, Vertex4f x)
        {
            Vertex4f t = x.sub(edge);
            return new Vertex4f(t.x < 0 ? 0 : 1, t.y < 0 ? 0 : 1, t.z < 0 ? 0 : 1, t.w < 0 ? 0 : 1);
        }

        public static float noise(Vertex2f v)
        {
            Vertex4f C = new Vertex4f(0.211324865405187F, 0.366025403784439F, -0.577350269189626F, 0.024390243902439F);
            Vertex2f i = floor(v.add(dot(v, C.yy())));
            Vertex2f x0 = (v - i).add(dot(i, C.xx()));
            Vertex2f i1;
            i1 = (x0.x > x0.y) ? new Vertex2f(1.0F, 0.0F) : new Vertex2f(0.0F, 1.0F);
            Vertex4f x12 = x0.xyxy().add(C.xxzz());
            x12 = x12.sub_xy(i1);
            i = mod(i, 289.0F);
            Vertex3f p = permute(
                permute(
                    new Vertex3f(0.0F, i1.y, 1.0F).add(i.y)
                ).add(i.x) + new Vertex3f(0.0F, i1.x, 1.0F)
            );
            Vertex3f m = max(
                0.5F.sub(new Vertex3f(
                    dot(x0, x0),
                    dot(x12.xy(), x12.xy()),
                    dot(x12.zw(), x12.zw()))),
                0.0F
            );
            m = m.mul(m);
            m = m.mul(m);
            Vertex3f x = (fract( p.mul(C.www()) ) * 2).sub(1.0F);
            Vertex3f h = abs(x).sub(0.5F);
            Vertex3f ox = floor(x.add(0.5F));
            Vertex3f a0 = x.sub(ox);
            m = m.mul(1.79284291400159F.sub((a0.mul(a0) + h.mul(h))* 0.85373472095314F));
            Vertex3f g = new Vertex3f();
            g.x = a0.x * x0.x + h.x * x0.y;
            g = g.a_yz(
                a0.yz().mul(x12.xz())
                .add(
                    h.yz().mul(x12.yw())
                )
            );
            return 130.0F * dot(m, g);
        }

        public static float noise(float x, float z)
        {
            return noise(new Vertex2f(x, z));
        }

        //	Simplex 3D Noise 
        //	by Ian McEwan, Ashima Arts
        //
        static Vertex4f permute(Vertex4f x) { return mod((x.mul(34F)).add(1.0F).mul(x), 289.0F); }
        static Vertex4f taylorInvSqrt(Vertex4f r) { return 1.79284291400159F.sub(r.mul(0.85373472095314F)); }

        public static float noise(Vertex3f v)
        {
            Vertex2f C = new Vertex2f(1F / 6F, 1F / 3F);
            Vertex4f D = new Vertex4f(0F, 0.5F, 1.0F, 2.0F);

            // First corner
            Vertex3f i = floor(v.add(dot(v, C.yyy())));
            Vertex3f x0 = (v - i).add(dot(i, C.xxx()));

            // Other corners
            Vertex3f g = step(x0.yzx(), x0.xyz());
            Vertex3f l = 1.0F.sub(g);
            Vertex3f i1 = min(g.xyz(), l.zxy());
            Vertex3f i2 = max(g.xyz(), l.zxy());

            //  x0 = x0 - 0. + 0.0 * C 
            Vertex3f x1 = x0.sub(i1).add(C.xxx());
            Vertex3f x2 = x0.sub(i2).add(C.xxx() * 2F);
            Vertex3f x3 = x0.sub(1F).add(C.xxx() * 3F);

            // Permutations
            i = mod(i, 289.0F);
            Vertex4f p = permute(
                permute(
                    permute(
                        new Vertex4f(0F, i1.z, i2.z, 1F).add(i.z)
                    )
                    .add(i.y).add(new Vertex4f(0F, i1.y, i2.y, 1F))
                )
                .add(i.x).add(new Vertex4f(0F, i1.x, i2.x, 1F))
            );

            // Gradients
            // ( N*N points uniformly over a square, mapped onto an octahedron.)
            float n_ = 1F / 7F; // N=7
            Vertex3f ns = (D.wyz()*n_).sub(D.xzx());

            Vertex4f j = p.sub(floor(p.mul(ns.z * ns.z)).mul(49F));  //  mod(p,N*N)

            Vertex4f x_ = floor(j.mul(ns.z));
            Vertex4f y_ = floor(j.sub(x_.mul(7F)));    // mod(j,N)

            Vertex4f x = x_.mul(ns.x).add(ns.yyyy());
            Vertex4f y = y_.mul(ns.x).add(ns.yyyy());
            Vertex4f h = 1.0F.sub(abs(x)).sub(abs(y));

            Vertex4f b0 = new Vertex4f(x.x, x.y, y.x, y.y);
            Vertex4f b1 = new Vertex4f(x.z, x.w, y.z, y.w);

            Vertex4f s0 = floor(b0).mul(2F).add(1F);
            Vertex4f s1 = floor(b1).mul(2F).add(1F);
            Vertex4f sh = -step(h, new Vertex4f(0F));

            Vertex4f a0 = b0.xzyw().add(s0.xzyw().mul(sh.xxyy()));
            Vertex4f a1 = b1.xzyw().add(s1.xzyw().mul(sh.zzww()));

            Vertex3f p0 = new Vertex3f(a0.x, a0.y, h.x);
            Vertex3f p1 = new Vertex3f(a0.z, a0.w, h.y);
            Vertex3f p2 = new Vertex3f(a1.x, a1.y, h.z);
            Vertex3f p3 = new Vertex3f(a1.z, a1.w, h.w);

            //Normalise gradients
            Vertex4f norm = taylorInvSqrt(new Vertex4f(dot(p0, p0), dot(p1, p1), dot(p2, p2), dot(p3, p3)));
            p0 *= norm.x;
            p1 *= norm.y;
            p2 *= norm.z;
            p3 *= norm.w;

            // Mix final noise value
            Vertex4f m = max(
                0.6F.sub(
                    new Vertex4f(dot(x0, x0), dot(x1, x1), dot(x2, x2), dot(x3, x3))
                ),
                0F
            );
            m = m.mul(m);
            return dot(m.mul(m).mul(42F), new Vertex4f(dot(p0, x0), dot(p1, x1), dot(p2, x2), dot(p3, x3)));
        }

        public static float noise(float x, float y, float z)
        {
            return noise(new Vertex3f(x, y, z));
        }

    }
}
