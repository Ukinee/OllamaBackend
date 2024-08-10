
namespace Persona.Models.Personas
{
    public class PostPersonaRequest
    {
        public required string Name { get; set; }
        public required Guid UserId { get; set; }
    }
}
