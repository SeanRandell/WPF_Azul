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
        private List<Tile> tilebag;
        private List<Tile> tileBin;

        public TileCollections()
        {
            tilebag = new List<Tile>();
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
                    var newTile = new Tile(tileType, 0, 0);
                    tilebag.Add(newTile);
                }
            }
            RandomizeBag();
        }

        public void RandomizeBag()
        {
            Random rng = new Random();
            int tilebagCount = tilebag.Count;

            while (tilebagCount > 1)
            {
                tilebagCount--;
                int k = rng.Next(tilebagCount + 1);
                Tile value = tilebag[k];
                tilebag[k] = tilebag[tilebagCount];
                tilebag[tilebagCount] = value;
            }
        }

        public Tile GetRandomTileFromBag()
        {
            if(tilebag.Count == 0)
            {
                RefillBagFromBin();
            }

            Random rng = new Random();
            int randomIndex = rng.Next(tilebag.Count);

            // Swap the random element with the last element
            Tile tempTile = tilebag[randomIndex];
            tilebag[randomIndex] = tilebag[tilebag.Count - 1];
            tilebag[tilebag.Count - 1] = tempTile;

            // Pop (remove) the last element and return it
            Tile poppedTile = tilebag[tilebag.Count - 1];
            tilebag.RemoveAt(tilebag.Count - 1);

            return poppedTile;
        }

        private void RefillBagFromBin()
        {
            tilebag.AddRange(tileBin);
            tileBin.Clear();
            RandomizeBag();
        }
    }
}
