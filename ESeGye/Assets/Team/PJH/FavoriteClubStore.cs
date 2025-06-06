using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FavoriteClubStore
{
    public static List<ClubData> Favorites = new List<ClubData>();

    public static void AddFavorite(ClubData club)
    {
        if (!Favorites.Contains(club))
        {
            Favorites.Add(club);
        }
    }

    public static void RemoveFavorite(ClubData club)
    {
        if (Favorites.Contains(club))
        {
            Favorites.Remove(club);
        }
    }

    public static void Clear()
    {
        Favorites.Clear();
    }
}
