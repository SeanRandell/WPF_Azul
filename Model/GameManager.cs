using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPF_Azul.Model
{
    public class GameManager
    {
        //To insert, simply place at the end, and increment the size. 
        private List<Tile> tilebag;

        const int tileTypeAmount = 20;
        const int factoryAmount = 5;
        private int[] droppedTileCost = [-1, -1, -2, -2, -2, -3, -3];
        private int droppedTileLength;

        public GameManager()
        {
            tilebag = new List<Tile>();
            CreateRandomTileBag();
        }

        private void CreateRandomTileBag()
        {
            foreach (TileType tileType in Enum.GetValues(typeof(TileType)))
            {
                if(tileType == TileType.StartingPlayerMarker)
                {
                    continue;
                }

                for (int i = 0; i < tileTypeAmount; i++)
                {
                    var newTile = new Tile(tileType);
                    tilebag.Add(newTile);
                }
            }
            RandomizeBag();
        }

        public void AddTile(Tile tile)
        {
            tilebag.Add(tile);
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
            Random rng = new Random();
            int randomIndex = rng.Next(tilebag.Count);

            // Swap the random element with the last element
            Tile temp = tilebag[randomIndex];
            tilebag[randomIndex] = tilebag[tilebag.Count - 1];
            tilebag[tilebag.Count - 1] = temp;

            // Pop (remove) the last element and return it
            Tile poppedValue = tilebag[tilebag.Count - 1];
            tilebag.RemoveAt(tilebag.Count - 1);

            return poppedValue;
        }
    }
}
