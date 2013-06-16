using System;
public static class ExtensionMethods
{
    public static int RoundOff(this int i)
    {
        return ((int)Math.Round(i / 20.0)) * 20;
    }
}