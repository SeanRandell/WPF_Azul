using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class TileCollections
    {
        const int tileTypeAmount = 20;

        //To insert, simply place at the end, and increment the size. 
        internal List<Tile> tileBag;
        internal List<Tile> tileBin;

        public TileCollections()
        {
            tileBag = new List<Tile>();
            tileBin = new List<Tile>();
            CreateRandomTileBag();
        }

        private void CreateRandomTileBag()
        {
            foreach (TileType tileType in Enum.GetValues(typeof(TileType)))
            {
                if (tileType == TileType.StartingPlayerMarker)
                {
                    continue;
                }

                for (int i = 0; i < tileTypeAmount; i++)
                {
                    var newTile = new Tile(tileType);
                    tileBag.Add(newTile);
                }
            }
            RandomizeBag();
        }

        public void RandomizeBag()
        {
            Random rng = new Random();
            int tilebagCount = tileBag.Count;

            while (tilebagCount > 1)
            {
                tilebagCount--;
                int k = rng.Next(tilebagCount + 1);
                Tile value = tileBag[k];
                tileBag[k] = tileBag[tilebagCount];
                tileBag[tilebagCount] = value;
            }
        }

        public Tile GetRandomTileFromBag()
        {
            if(tileBag.Count == 0)
            {
                RefillBagFromBin();
            }

            Random rng = new Random();
            int randomIndex = rng.Next(tileBag.Count);

            // Swap the random element with the last element
            Tile tempTile = tileBag[randomIndex];
            tileBag[randomIndex] = tileBag[tileBag.Count - 1];
            tileBag[tileBag.Count - 1] = tempTile;

            // Pop (remove) the last element and return it
            Tile poppedTile = tileBag[tileBag.Count - 1];
            tileBag.RemoveAt(tileBag.Count - 1);

            return poppedTile;
        }

        private void RefillBagFromBin()
        {
            tileBag.AddRange(tileBin);
            tileBin.Clear();
            RandomizeBag();
        }

        internal void AddTilesToTileBin(List<Tile> tilesToBin)
        {
            tileBin.AddRange(tilesToBin);
        }
    }
}
