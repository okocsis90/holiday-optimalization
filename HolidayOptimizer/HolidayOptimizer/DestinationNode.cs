namespace HolidayOptimizer
{
    public class DestinationNode
    {
        public string Name { get; set; }
        public DestinationNode Previous { get; set; }
        public DestinationNode Next { get; set; }

        public DestinationNode(string name)
        {
            Name = name;
            Previous = null;
        }

        public DestinationNode(string name, DestinationNode previous)
        {
            Name = name;
            Previous = previous;
        }
    }
}
