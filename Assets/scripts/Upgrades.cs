// <copyright file="Upgrades.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// This class holds the data regarding the upgade staus for the current game session.
/// </summary>
public static class Upgrades
{
    // Glider = 0 when the glider upgrade has not been purchesed
    private static int glider;
    private static int booster;

    /// <summary>
    /// Gets or sets glider upgrades status.
    /// </summary>
    public static int Glider
    {
        get
        {
            return glider;
        }

        set
        {
            glider = value;
        }
    }

    /// <summary>
    /// Gets or sets booster upgrade status.
    /// </summary>
    public static int Booster
    {
        get
        {
            return booster;
        }

        set
        {
            booster = value;
        }
    }
}
