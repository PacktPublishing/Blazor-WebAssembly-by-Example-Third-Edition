namespace SmartComponents.Models
{
    public class EmbeddingItem
    {
        public string? Category { get; set; }
        public string? Sentence { get; set; }
        public double[] Embeddings { get; set; } = Array.Empty<double>();
    }
}
