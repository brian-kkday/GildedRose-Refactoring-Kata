using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        private readonly IList<Item> _items;
        public GildedRose(IList<Item> items)
        {
            _items = items;
        }

        public void UpdateQuality()
        {
            var factory = new Factory();

            foreach (var item in _items)
            {
                var itemFactory = factory.Create(item);
                itemFactory.Decline(item);
            }
        }

        public interface IItem
        {
            public Item Decline(Item item);
        }

        public class Factory
        {
            public IItem Create(Item item)
            {
                IItem result = null;

                switch (item.Name)
                {
                    case "Sulfuras, Hand of Ragnaros":
                        result = new Sulfuras();
                        break;
                    case "Backstage passes to a TAFKAL80ETC concert":
                        result = new BackstagePass();
                        break;
                    case "Aged Brie":
                        result = new AgedBrie();
                        break;
                    case "Conjured":
                        result = new Conjured();
                        break;
                    default:
                        result = new NormalItem();
                        break;
                }

                return result;
            }
        }

        public class NormalItem : IItem
        {
            public Item Decline(Item item)
            {
                var declineQuantityCount = 1;
                var declineQuantityRate = 1;


                if (item.SellIn <= 0) item.Quality -= declineQuantityCount * declineQuantityRate * 2;
                else
                    item.Quality -= declineQuantityCount * declineQuantityRate;

                if (item.Quality < 0) item.Quality = 0;

                item.SellIn -= 1;
                return item;
            }
        }

        public class Sulfuras : IItem
        {
            public Item Decline(Item item)
            {
                return item;
            }
        }

        public class BackstagePass : IItem
        {
            public Item Decline(Item item)
            {
                if (item.SellIn <= 0)
                {
                    item.SellIn -= 1;
                    item.Quality = 0;
                    return item;
                }

                item.Quality += 1;

                if (item.SellIn < 11 && item.SellIn > 5) item.Quality += 1;
                if (item.SellIn <= 5) item.Quality += 2;
                if (item.Quality > 50) item.Quality = 50;

                item.SellIn -= 1;
                return item;
            }
        }

        public class AgedBrie : IItem
        {
            public Item Decline(Item item)
            {
                item.Quality += 1;

                if (item.SellIn <= 0) item.Quality += 1;

                if (item.Quality > 50) item.Quality = 50;

                item.SellIn -= 1;
                return item;
            }
        }

        public class Conjured : IItem
        {
            public Item Decline(Item item)
            {
                item.Quality -= 1 * 2;

                if (item.SellIn <= 0) item.Quality -= 1 * 2;

                if (item.Quality < 0) item.Quality = 0;

                item.SellIn -= 1;

                return item;
            }
        }
    }
}
