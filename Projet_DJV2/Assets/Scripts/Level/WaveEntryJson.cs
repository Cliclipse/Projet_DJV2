using System;

namespace Level
{
    /// <summary>
    /// Une entrée dans le fichier json des vagues (contient l'identifiant de l'ennemi et le nombre à spawn)
    /// </summary>
    [Serializable]
    public class WaveEntryJson
    {
        public string id;
        public int count;
    }
}