namespace WebApi.Domain.DTOs
{
    public class InfoApiDTO
    {
        // Filtrar o que apresentar para o usuário
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Photo {  get; set; }
    }
}
