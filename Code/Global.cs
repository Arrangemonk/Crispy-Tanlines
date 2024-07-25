using Godot;
using System;
using System.Linq;

public class Global : Node
{
    public static float Unease(float from, float to, float weight)
    {
        weight = Mathf.Min(1, Mathf.Max(0, 0.5f * (Mathf.Sqrt(weight) + weight * weight)));
        return from + (to - from) * weight;
    }

    public static Vector2 Unease(Vector2 from, Vector2 to, float weight)
    {
        weight = Mathf.Min(1, Mathf.Max(0, 0.5f * (Mathf.Sqrt(weight) + weight * weight)));
        return from + (to - from) * weight;
    }

    public static float Lerp(float from, float to, float weight)
    {
        weight = Mathf.Min(1, Mathf.Max(0, weight));
        return from + (to - from) * weight;
    }

    public static Vector3 Lerp(Vector3 from, Vector3 to, float weight)
    {
        weight = Mathf.Min(1, Mathf.Max(0, weight));
        return from + (to - from) * weight;
    }

    public static Vector2 Lerp(Vector2 from, Vector2 to, float weight)
    {
        weight = Mathf.Min(1, Mathf.Max(0, weight));
        return from + (to - from) * weight;
    }

    public static float SmoothStep(float from, float to, float weight)
    {
        weight = weight * weight * (3f - 2f * weight);
        return from + (to - from) * weight;
    }

    public static Vector3 SmoothStep(Vector3 from, Vector3 to, float weight)
    {
        weight = weight * weight * (3f - 2f * weight);
        return from + (to - from) * weight;
    }

    public static Vector2 SmoothStep(Vector2 from, Vector2 to, float weight)
    {
        weight = weight * weight * (3f - 2f * weight);
        return from + (to - from) * weight;
    }

    public static float OvershootSmoothStep(float from, float to, float weight)
    {
        weight = 1.25f * (0.1f * Mathf.Cos(3 * Mathf.Pi * weight) - 0.5f * Mathf.Cos(Mathf.Pi * weight) + 0.4f);
        return from + (to - from) * weight;
    }

    public static Vector2 OvershootSmoothStep(Vector2 from, Vector2 to, float weight)
    {
        weight = 1.25f * (0.1f * Mathf.Cos(3 * Mathf.Pi * weight) - 0.5f * Mathf.Cos(Mathf.Pi * weight) + 0.4f);
        return from + (to - from) * weight;
    }

    public static Vector3 OvershootSmoothStep(Vector3 from, Vector3 to, float weight)
    {
        weight = 1.25f * (0.1f * Mathf.Cos(3 * Mathf.Pi * weight) - 0.5f * Mathf.Cos(Mathf.Pi * weight) + 0.4f);
        return from + (to - from) * weight;
    }

    public static float Easin(float from, float to, float weight)
    {
        weight = Mathf.Min(1, Mathf.Max(0, weight * weight * weight));
        return from + (to - from) * weight;
    }

    public static Vector2 Easin(Vector2 from, Vector2 to, float weight)
    {
        weight = Mathf.Min(1, Mathf.Max(0, weight * weight * weight));
        return from + (to - from) * weight;
    }

    public static Vector3 Easin(Vector3 from, Vector3 to, float weight)
    {
        weight = Mathf.Min(1, Mathf.Max(0, weight * weight * weight));
        return from + (to - from) * weight;
    }

    public static Vector3 Bounce(Vector3 from, Vector3 to, float weight)
    {
        weight = Mathf.Max(0, Mathf.Min(3, weight)) * 2;
        var x = weight * weight * Mathf.Pi;
        weight = Mathf.Min(1, Mathf.Max(0, Mathf.Abs(Mathf.Sin(x)) / x));
        return from + (to - from) * weight;
    }

    public static int Mod(int input, int modulator)
    {
        return (input % modulator + modulator) % modulator;
    }

    public static float Mod(float input, float modulator)
    {
        return (input % modulator + modulator) % modulator;
    }

    //https://stackoverflow.com/questions/12838007/c-sharp-linear-interpolation
    public static float Linear(float x0, float x1, float y0, float y1, float x)
    {
        if ((x1 - x0) == 0)
        {
            return (y0 + y1) / 2;
        }
        return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
    }
    public static float Linear((float x, float y) _0, (float x, float y) _1, float x)
    {
        return Linear(_0.x, _1.x, _0.y, _1.y, x);
    }

    public static float Qlerp(float x0, float x1, float x2, float y0, float y1, float y2, float x)
    {
        //sort the points from smallest to largest
        if (x0 > x1)
        {
            (x0, x1) = (x1, x0);
            (y0, y1) = (y1, y0);
        }

        if (x0 > x2)
        {
            (x0, x2) = (x2, x0);
            (y0, y2) = (y2, y0);
        }

        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
            (y1, y2) = (y2, y1);
        }

        var tmp1 = Linear(x0, x1, y0, y1, x);
        var tmp2 = Linear(x1, x2, y1, y2, x);
        return x < x1
            ? Lerp(tmp1, tmp2, (x - x0) / (x1 - x0))
            : Lerp(tmp1, tmp2, (x - x1) / (x2 - x1));
    }

    public static float Qlerp((float x, float y) _0, (float x, float y) _1, (float x, float y) _2, float x)
    {
        return Qlerp(_0.x, _1.x, _2.x, _0.y, _1.y, _2.y, x);
    }

    public static float Spine1d((float x, float y)[] points, float x)
    {
        var length = points.Length;
        if (length < 3)
            return Linear(points[0], points[1], x);
        points = points.OrderBy(p => p.x).ToArray();
        var last = 0;
        for (var i = 0; i < length; i++)
        {
            if (points[i].x < x)
            {
                last = i;
                continue;
            }
            break;
        }
        last = Math.Min(last, length - 3);
        return Qlerp(points[last], points[last + 1], points[last + 2], x);

    }
}
