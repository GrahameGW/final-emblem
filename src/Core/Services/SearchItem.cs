using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class SearchItem
    {
        public Tile Tile { get; set; }
        public SearchItem PathFrom { get; set; }
        public int Distance = int.MaxValue;
        public int Priority { get => Distance + Heuristic; }
        public SearchItem NextWithSamePriority { get; set; }
        public int Heuristic { get; set; }
    }

    public class SearchItemPriorityQueue
    {
        public int Count { get; private set; }

        private List<SearchItem> list = new();
        private int minimum = int.MaxValue;


        public void Enqueue(SearchItem item)
        {
            Count += 1;
            int priority = item.Priority;
            if (priority < minimum)
            {
                minimum = priority;
            }
            while (priority >= list.Count)
            {
                list.Add(null);
            }
            item.NextWithSamePriority = list[priority];
            list[priority] = item;
        }

        public SearchItem Dequeue()
        {
            Count -= 1;
            for (; minimum < list.Count; minimum++)
            {
                var item = list[minimum];
                if (item != null)
                {
                    list[minimum] = item.NextWithSamePriority;
                    return item;
                }
            }
            return null;
        }

        public void Change(SearchItem item, int oldPriority)
        {
            var current = list[oldPriority];
            var next = current.NextWithSamePriority;
            if (current == item)
            {
                list[oldPriority] = next;
            }
            else
            {
                while (next != item)
                {
                    current = next;
                    next = current.NextWithSamePriority;
                }
                current.NextWithSamePriority = item.NextWithSamePriority;
            }
            Enqueue(item);
            Count -= 1;
        }

        public void Clear()
        {
            list.Clear();
            Count = 0;
            minimum = int.MaxValue;
        }
    }

}
