
namespace GeneticAlgorithm.Core
{
    public class GenomeModel
    {
        public short Value;
        
        public GenomeModel(short value)
        {
            Value = value;
        }

        public void AddValue()
        {
            Value *= 1000;
        }
    }
}
